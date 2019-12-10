using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IShopify.Core.Emails;

namespace IShopify.Core.Common
{
    public interface ITemplateLoader
    {
        Stream LoadTemplate(Assembly excutingAssembly, string manifestSearchText);

        Task<string> LoadTemplateAsStringAsync(Assembly excutingAssembly, string manifestSearchText);

        Task<String> LoadEmailTemplateAsync(EmailTemplateType templateType);
    }
}
