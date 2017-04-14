using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Our.Umbraco.GridSettings.Services
{
    public class GridSettingsAttributesService
    {
        public IDictionary<string, string> GetAllAttributes(JObject contentItem)
        {
            var attributes = new Dictionary<string, string>();

            //get style attribute
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
                    attributes.Add("style", string.Join(" ", cssValues));
                }
            }

            //get settings attributes
            var settingsConfig = contentItem.Value<JObject>("config");
            if (settingsConfig != null)
            {
                foreach (var property in settingsConfig.Properties())
                {
                    attributes.Add(property.Name, property.Value.ToString());
                }
            }

            return attributes;
        }
    }
}