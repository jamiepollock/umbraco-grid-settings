using System.Collections.Generic;
using System.Linq;

namespace Our.Umbraco.GridSettings.Resolvers
{
    public class GroupByKeyGridSettingsAttributesResolver : GroupByGridSettingsAttributesResolver
    {
        protected override IEnumerable<IGrouping<string, KeyValuePair<string, string>>> GroupProperties(IDictionary<string, string> properties)
        {
            return properties.GroupBy(x => x.Key);
        }
    }
}
