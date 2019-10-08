using IShopify.Core.Common;
using IShopify.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.DomainServices.Common
{
    public class TemplateLoader : ITemplateLoader
    {
        public Stream LoadTemplate(Assembly excutingAssembly, string manifestSearchText)
        {
            var allManifests = excutingAssembly.GetManifestResourceNames();
            var targetManifestName = allManifests.SingleOrDefault(m => m.Contains(manifestSearchText));

            if (targetManifestName.IsNull())
            {
                throw new FileNotFoundException($"No template found for search text '{manifestSearchText}'");
            }

            return excutingAssembly.GetManifestResourceStream(targetManifestName);
        }

        public async Task<string> LoadTemplateAsStringAsync(Assembly excutingAssembly,  string manifestSearchText)
        {
            var templateStream = LoadTemplate(excutingAssembly, manifestSearchText);

            return await ToString(templateStream);
        }

        private Task<string> ToString(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(stream);

            return reader.ReadToEndAsync();
        }
    }
}
