using System;
using System.Threading.Tasks;

namespace IShopify.Core.Security
{
    public interface IAccessKeyService
    {
        Task<AccessCard> GetAccessCardAsync(string key);

        Task<string> GenerateAccessKeyAsync(int userId, DateTime expirationUtc, params AccessScope[] scopes);

        Task<string> GenerateAccessKeyAsync(CreateAccessKeyModel createAccessKeyModel);

        Task RevokeAccessKeysAsync(int userId, params AccessScope[] scopes);
    }
}
