using System.Threading.Tasks;

namespace IShopify.Core.Common
{
    public interface IUrlService
    {
        string GetPasswordResetUrl(string accesskey); 
    }
}