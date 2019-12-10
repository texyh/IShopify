using System;
using System.Collections.Generic;
using IShopify.Core.Framework;
using IShopify.Core.Helpers;
using RazorEngine;
using RazorEngine.Templating;

namespace IShopify.Framework
{
    public class TemplateService : ITemplateService
    {
        public bool IsTemplateCached(string templateKey)
        {
            return Engine.Razor.IsTemplateCached(templateKey, null);
        }

        public string Execute(string templateKey, object model,
            IDictionary<string, object> viewBag = null)
        {
            ArgumentGuard.NotNullOrWhiteSpace(templateKey, nameof(templateKey));
            ArgumentGuard.NotNull(model, nameof(model));

            if (!IsTemplateCached(templateKey))
            {
                throw new ArgumentException("TemplateKey has to be cached");
            }

            var modelType = model.GetType();
            var dynamicViewbag = new DynamicViewBag(viewBag ?? new Dictionary<string, object>());

            return Engine.Razor.Run(templateKey, modelType, model, viewBag: dynamicViewbag);
        }

        public string Execute(string templateKey, object model, string template,
            IDictionary<string, object> viewBag = null)
        {
            ArgumentGuard.NotNullOrWhiteSpace(templateKey, nameof(templateKey));
            ArgumentGuard.NotNull(model, nameof(model));
            ArgumentGuard.NotNullOrWhiteSpace(template, nameof(template));

            var modelType = model.GetType();
            var dynamicViewbag = new DynamicViewBag(viewBag ?? new Dictionary<string, object>());

            if (IsTemplateCached(templateKey))
            {
                return Engine.Razor.Run(templateKey, modelType, model, viewBag: dynamicViewbag);
            }
            
            AddTemplateToCache(templateKey, template);            

            return Engine.Razor.RunCompile(templateKey, modelType, model, viewBag: dynamicViewbag);
        }

        public string Execute(string templateKey, object model, string template, string layoutKey,
            IDictionary<string, object> viewBag = null)
        {
            ArgumentGuard.NotNullOrWhiteSpace(templateKey, nameof(templateKey));
            ArgumentGuard.NotNull(model, nameof(model));
            ArgumentGuard.NotNullOrWhiteSpace(template, nameof(template));
            ArgumentGuard.NotNullOrWhiteSpace(layoutKey, nameof(layoutKey));

            if (!IsTemplateCached(layoutKey))
            {
                throw new ArgumentException("LayoutKey has to be cached");
            }

            var modelType = model.GetType();
            var dynamicViewbag = new DynamicViewBag(viewBag ?? new Dictionary<string, object>());

            if (IsTemplateCached(templateKey))
            {
                return Engine.Razor.Run(templateKey, modelType, model, viewBag: dynamicViewbag);
            }

            AddTemplateToCache(templateKey, template);
                
            return Engine.Razor.RunCompile(templateKey, modelType, model, viewBag: dynamicViewbag);
          
        }

        public string Execute(string templateKey, object model, string template, string layoutKey, 
            string layout, IDictionary<string, object> viewBag = null)
        {
            ArgumentGuard.NotNullOrWhiteSpace(templateKey, nameof(templateKey));
            ArgumentGuard.NotNull(model, nameof(model));
            ArgumentGuard.NotNullOrWhiteSpace(template, nameof(template));
            ArgumentGuard.NotNullOrWhiteSpace(layoutKey, nameof(layoutKey));
            ArgumentGuard.NotNullOrWhiteSpace(layout, nameof(layout));

            var modelType = model.GetType();
            var dynamicViewbag = new DynamicViewBag(viewBag ?? new Dictionary<string, object>());

            if (IsTemplateCached(layoutKey))
            {
                if (IsTemplateCached(templateKey))
                {
                    return Engine.Razor.Run(templateKey, modelType, model, viewBag: dynamicViewbag);
                }

                AddTemplateToCache(templateKey, template);
                
                return Engine.Razor.RunCompile(templateKey, modelType, model, viewBag: dynamicViewbag);
            }

            AddTemplateToCache(layoutKey, layout);

            AddTemplateToCache(templateKey, template);

            return Engine.Razor.RunCompile(templateKey, modelType, model, viewBag: dynamicViewbag);
        }        

        private void AddTemplateToCache(string key, string template)
        {
            Engine.Razor.AddTemplate(key, template);
            Engine.Razor.Compile(key);
        }
    }
}