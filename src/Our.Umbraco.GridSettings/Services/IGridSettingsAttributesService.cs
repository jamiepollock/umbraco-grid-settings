using System.Collections.Generic;

namespace Our.Umbraco.GridSettings.Services
{
    public interface IGridSettingsAttributesService<TModel>
    {
        IDictionary<string, string> GetAllAttributes(TModel contentItem);

        IDictionary<string, string> GetSettingsAttributes(TModel contentItem);

        KeyValuePair<string, string> GetStyleAttribute(TModel contentItem);

        string ResolveSettingValue(KeyValuePair<string, IDictionary<string, string>> property);
    }
}