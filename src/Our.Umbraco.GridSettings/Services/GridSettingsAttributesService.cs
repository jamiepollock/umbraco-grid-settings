using Newtonsoft.Json.Linq;
using Our.Umbraco.GridSettings.Resolvers;
using System.Collections.Generic;
using System.Linq;

namespace Our.Umbraco.GridSettings.Services
{
    public sealed class GridSettingsAttributesService : IGridSettingsAttributesService
    {
        private readonly IGridSettingsAttributesResolver _attributesResolver;
        private readonly IGridSettingsAttributeValueResolver _defaultAttributeValueResolver;
        private readonly IDictionary<string, IGridSettingsAttributeValueResolver> _attributeValueResolvers;

        public GridSettingsAttributesService(IGridSettingsAttributesResolver attributesResolver = null, IGridSettingsAttributeValueResolver defaultAttributeValueResolver = null, IDictionary<string, IGridSettingsAttributeValueResolver> attributeValueResolvers = null)
        {
            _attributesResolver = attributesResolver;
            _defaultAttributeValueResolver = defaultAttributeValueResolver ?? new StringConcatGridSettingValueResolver();
            _attributeValueResolvers = attributeValueResolvers ?? new Dictionary<string, IGridSettingsAttributeValueResolver>();
        }

        public IDictionary<string, string> GetAllAttributes(JObject contentItem)
        {
            var attributes = new Dictionary<string, string>();            
            var styleAttribute = GetStyleAttribute(contentItem);
            var settingsAttributes = GetSettingsAttributes(contentItem);

            if (styleAttribute.IsValid())
            {
                attributes.AddKeyValuePair(styleAttribute);
            }

            foreach(var attribute in settingsAttributes)
            {
                attributes.AddKeyValuePair(attribute);
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
                    var value = ResolveSettingValue(property);
                    var attribute = new KeyValuePair<string, string>(property.Key, value);

                    if (attribute.IsValid())
                    {
                        attributes.AddKeyValuePair(attribute);
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


        public string ResolveSettingValue(IGrouping<string, JProperty> property)
        {
            IGridSettingsAttributeValueResolver resolver = null;

            if (_attributeValueResolvers.TryGetValue(property.Key, out resolver) == false)
            {
                resolver = _defaultAttributeValueResolver;
            }

            return resolver.ResolveAttributeValue(property);
        }
    }
}