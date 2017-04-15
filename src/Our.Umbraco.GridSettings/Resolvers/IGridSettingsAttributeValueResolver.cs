using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Our.Umbraco.GridSettings.Resolvers
{
    public interface IGridSettingsAttributeValueResolver
    {
        string ResolveAttributeValue(KeyValuePair<string, IEnumerable<JProperty>> property);
    }
}
