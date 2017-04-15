using System.Collections.Generic;

namespace Our.Umbraco.GridSettings.Resolvers
{
    public interface IGridSettingsAttributeValueResolver
    {
        string ResolveAttributeValue(KeyValuePair<string, IDictionary<string, string>> property);
    }
}
