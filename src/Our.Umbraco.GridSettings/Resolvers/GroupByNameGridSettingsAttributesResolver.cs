using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Our.Umbraco.GridSettings.Resolvers
{
    public class GroupByNameGridSettingsAttributesResolver : GroupByGridSettingsAttributesResolver
    {
        protected override IEnumerable<IGrouping<string, JProperty>> GroupProperties(IEnumerable<JProperty> properties)
        {
            return properties.GroupBy(x => x.Name);
        }
    }
}
