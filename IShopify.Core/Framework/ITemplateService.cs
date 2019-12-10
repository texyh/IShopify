using System.Collections.Generic;

namespace IShopify.Core.Framework {
    public interface ITemplateService {
        bool IsTemplateCached (string templateKey);

        string Execute (string templateKey, object model, IDictionary<string, object> viewBag = null);

        string Execute (string templateKey, object model, string template, IDictionary<string, object> viewBag = null);

        string Execute (string templateKey, object model, string template, string layoutKey, IDictionary<string, object> viewBag = null);

        string Execute (string templateKey, object model, string template, string layoutKey,
            string layout, IDictionary<string, object> viewBag = null);
    }
}