
using System;
using System.Linq;
using System.Threading.Tasks;
using IShopify.Core.Framework;
using IShopify.Core.Framework.Logging;
using IShopify.Core.Helpers;
using IShopify.Core.Security;
using IShopify.DomainServices.Validation;

namespace IShopify.DomainServices.Security
{
    public class AccessKeyService : IAccessKeyService
    {
        private readonly IAccessKeyRepository _accessKeyRepository;
        private readonly ICryptoService _cryptoService;
        private readonly ILogger _logger;
        private readonly IValidatorFactory _validatorFactory;

        private const int KeyLength = 48;
        private const int DefaultMaxAllowedAttempts = 1;

        public AccessKeyService(
            IAccessKeyRepository accessKeyRepository, 
            ICryptoService cryptoService,
            ILogger logger,
            IValidatorFactory validatorFactory)
        {
            _accessKeyRepository = accessKeyRepository;
            _cryptoService = cryptoService;
            _logger = logger;
            _validatorFactory = validatorFactory;
        }

        public Task<string> GenerateAccessKeyAsync(int userId, DateTime expirationUtc, params AccessScope[] scopes)
        {
            var model = new CreateAccessKeyModel
            {
                UserId = userId,
                ExpirationUtc = expirationUtc,
                Scopes = scopes,
                MaxAllowedAttempts = DefaultMaxAllowedAttempts
            };

            return GenerateAccessKeyAsync(model);
        }

        public async Task<string> GenerateAccessKeyAsync(CreateAccessKeyModel createAccessKeyModel)
        {
            await _validatorFactory.ValidateAsync(createAccessKeyModel);

            var accessKey = new AccessKeyEntity
            {
                Id = Guid.NewGuid(),
                UserId = createAccessKeyModel.UserId,
                StartTimeUtc = createAccessKeyModel.StartTimeUtc,
                ExpirationUtc = createAccessKeyModel.ExpirationUtc,
                MaxAllowedAttempts = createAccessKeyModel.MaxAllowedAttempts,
                ResourceIds = createAccessKeyModel.ResourceIds,
                CreatedDateUtc = DateTime.UtcNow,
                Scopes = createAccessKeyModel.Scopes,
                Attempts = default(int),
                Key = _cryptoService.CreateUniqueKey(KeyLength)
            };

            await _accessKeyRepository.AddAsync(accessKey);

            return accessKey.Key;
        }

        public async Task<AccessCard> GetAccessCardAsync(string key)
        {
            try
            {
                return await DoGetAccessCardAsync(key);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return await ProcessAccessCard(new AccessKeyEntity(), AccessKeyStatus.Errored);
            }
        }

        public async Task RevokeAccessKeysAsync(int userId, params AccessScope[] scopes)
        {
            ArgumentGuard.NotDefault(userId, nameof(userId));
            ArgumentGuard.NotNullOrEmpty(scopes, nameof(scopes));

            var accessKeys = await _accessKeyRepository.FindAllAsync(k => k.UserId == userId);

            foreach (var accessKey in accessKeys)
            {
                accessKey.Scopes = accessKey.Scopes.Except(scopes).ToArray();

                // delete document if no scope
                if (!accessKey.Scopes.Any())
                {
                    await _accessKeyRepository.DeleteAsync(accessKey.Id, false);
                }
                //update if there are still some scopes
                else       
                {
                    await _accessKeyRepository.UpdateFieldsAsync(accessKey, nameof(AccessKeyEntity.ScopesJson));
                }
            }
        }

        private async Task<AccessCard> DoGetAccessCardAsync(string key)
        {
            ArgumentGuard.NotNullOrEmpty(key, nameof(key));

            var accessKey = (await _accessKeyRepository.FindAllAsync(k => k.Key == key)).SingleOrDefault();
            var now = DateTime.UtcNow;

            if (accessKey.IsNull())
            {
                return await ProcessAccessCard(new AccessKeyEntity(), AccessKeyStatus.NotFound);
            }

            if (now >= accessKey.ExpirationUtc)
            {
                return await ProcessAccessCard(accessKey, AccessKeyStatus.Expired);
            }

            if (accessKey.Attempts >= accessKey.MaxAllowedAttempts)
            {
                return await ProcessAccessCard(accessKey, AccessKeyStatus.MaxAttemptsExceeded);
            }

            // The key is not ready for use. So, this is not considered an attempt. 
            if (accessKey.StartTimeUtc.HasValue && now < accessKey.StartTimeUtc.Value)
            {
                return await ProcessAccessCard(accessKey, AccessKeyStatus.NotReady);
            }

            return await ProcessAccessCard(accessKey, AccessKeyStatus.Valid);
        }

        private async Task<AccessCard> ProcessAccessCard(AccessKeyEntity accessKey, AccessKeyStatus status)
        {
            // increment attempts
            accessKey.Attempts++;

            var update = false;
            var delete = accessKey.Attempts >= accessKey.MaxAllowedAttempts;

            switch (status)
            {
                case AccessKeyStatus.Expired:
                case AccessKeyStatus.MaxAttemptsExceeded:
                    delete = true;
                    update = false;
                    break;

                case AccessKeyStatus.NotReady:
                case AccessKeyStatus.NotFound:
                case AccessKeyStatus.Errored:
                    delete = false;
                    update = false;
                    break;
                
                case AccessKeyStatus.Valid:
                    update = !delete;
                    break;

                default:
                    status = AccessKeyStatus.Errored; // change status to Errored if can't identity the status
                    break;
            }

            if (delete)
            {
                await _accessKeyRepository.DeleteAsync(accessKey.Id, false);
            }
            else if (update)
            {
                await _accessKeyRepository.UpdateFieldsAsync(accessKey, nameof(AccessKeyEntity.Attempts));
            }

            return new AccessCard(accessKey.Key, accessKey.UserId, status, accessKey.Scopes, accessKey.ResourceIds);
        }
    }
}
