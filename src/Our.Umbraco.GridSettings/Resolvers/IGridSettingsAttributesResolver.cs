using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Our.Umbraco.GridSettings.Resolvers
{
    public interface IGridSettingsAttributesResolver
    {
        IDictionary<string, IEnumerable<JProperty>> ResolveSettingsAttributes(IEnumerable<JProperty> properties);
    }
}
