using System.Collections.Generic;
using System.Linq;

namespace Our.Umbraco.GridSettings.Resolvers
{
    /// <summary>
    /// An <see cref="IGridSettingsAttributesResolver"/> which groups attributes by their Key. All keys are unique however. 
    /// 
    /// This <see cref="IGridSettingsAttributesResolver" /> is here to mimic typical Umbraco Grid Setting behavior.
    /// </summary>
    public sealed class GroupByKeyGridSettingsAttributesResolver : GroupByGridSettingsAttributesResolver
    {
        protected override IEnumerable<IGrouping<string, KeyValuePair<string, string>>> GroupProperties(IDictionary<string, string> properties)
        {
            return properties.GroupBy(x => x.Key);
        }
    }
}
