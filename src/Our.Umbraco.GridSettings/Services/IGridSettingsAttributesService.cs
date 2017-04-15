using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Our.Umbraco.GridSettings.Services
{
    public interface IGridSettingsAttributesService
    {
        IDictionary<string, string> GetAllAttributes(JObject contentItem);

        IDictionary<string, string> GetSettingsAttributes(JObject contentItem);

        KeyValuePair<string, string> GetStyleAttribute(JObject contentItem);

        string ResolveSettingValue(KeyValuePair<string, IEnumerable<JProperty>> property);
    }
}