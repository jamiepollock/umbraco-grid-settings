using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Our.Umbraco.GridSettings.Resolvers
{
    public interface IGridSettingsAttributesResolver
    {
        IEnumerable<IGrouping<string, JProperty>> ResolveSettingsAttributes(IEnumerable<JProperty> properties);
    }
}
