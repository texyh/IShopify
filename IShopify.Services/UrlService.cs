using IShopify.Core.Common;
using IShopify.Core.Config;
using IShopify.Core.Security;

namespace IShopify.Services
{
    public class UrlService : IUrlService
    {
        private AppSettings _appSettings;

        public UrlService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        
        public string GetPasswordResetUrl(string accesskey)
        {
            return $"{_appSettings.WebUrl}/reset-password?{SecurityConstants.AccessTokenQueryKey}={accesskey}";
        }
    }
}