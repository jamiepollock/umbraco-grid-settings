using System.Collections.Generic;

namespace Our.Umbraco.GridSettings.Resolvers
{
    public interface IGridSettingsAttributesResolver
    {
        IDictionary<string, IDictionary<string, string>> ResolveSettingsAttributes(IDictionary<string, string> properties);
    }
}
