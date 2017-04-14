using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Our.Umbraco.GridSettings.ValueResolvers
{
    public interface IGridSettingsAttributesResolver
    {
        IEnumerable<IGrouping<string, JProperty>> ResolveSettingsAttributes(IEnumerable<JProperty> properties);
    }
}
