using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.GridSettings.ValueResolvers;

namespace Our.Umbraco.GridSettings.Services
{
    public class GridSettingsAttributesService
    {
        private readonly IGridSettingsAttributesResolver _attributesResolver;
        
        public GridSettingsAttributesService(IGridSettingsAttributesResolver attributesResolver = null)
        {
            _attributesResolver = attributesResolver;
        }

        public IDictionary<string, string> GetAllAttributes(JObject contentItem)
        {
            var attributes = new Dictionary<string, string>();            
            var styleAttribute = GetStyleAttribute(contentItem);
            var settingsAttributes = GetSettingsAttributes(contentItem);

            if (styleAttribute.IsValid())
            {
                attributes.Add(styleAttribute.Key, styleAttribute.Value);
            }

            foreach(var attribute in settingsAttributes)
            {
                attributes.Add(attribute.Key, attribute.Value);
            }

            return attributes;
        }

        public IDictionary<string, string> GetSettingsAttributes(JObject contentItem)
        {
            var attributes = new Dictionary<string, string>();
            var settingsConfig = contentItem.Value<JObject>("config");

            if (settingsConfig != null)
            {
                foreach (var property in ResolveAttributes(settingsConfig.Properties()))
                {
                    var value = string.Join(" ", property.Select(x => x.Value));
                    var attribute = new KeyValuePair<string, string>(property.Key, value);

                    if (attribute.IsValid())
                    {
                        attributes.Add(attribute.Key, attribute.Value);
                    }
                }
            }

            return attributes;
        }

        private IEnumerable<IGrouping<string, JProperty>> ResolveAttributes(IEnumerable<JProperty> properties)
        {
            if(_attributesResolver != null)
            {
                return _attributesResolver.ResolveSettingsAttributes(properties);
            }

            return properties.GroupBy(x => x.Name);
        }

        public KeyValuePair<string, string> GetStyleAttribute(JObject contentItem)
        {
            var stylesConfig = contentItem.Value<JObject>("styles");
            if (stylesConfig != null)
            {
                var cssValues = new List<string>();

                foreach (var property in stylesConfig.Properties())
                {
                    var propertyValue = property.Value.ToString();
                    if (string.IsNullOrWhiteSpace(propertyValue) == false)
                    {
                        cssValues.Add(property.Name + ":" + propertyValue + ";");
                    }
                }

                if (cssValues.Any())
                {
                    return new KeyValuePair<string, string>("style", string.Join(" ", cssValues));
                }
            }

            return default(KeyValuePair<string, string>);
        }
    }
}