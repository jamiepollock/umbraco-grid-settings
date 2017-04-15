using System.Collections.Generic;
using System.Linq;

namespace Our.Umbraco.GridSettings.Resolvers
{
    public abstract class GroupByGridSettingsAttributesResolver : IGridSettingsAttributesResolver
    {
        public IDictionary<string, IDictionary<string, string>> ResolveSettingsAttributes(IDictionary<string, string> properties)
        {
            var groupedProperties = GroupProperties(properties);

            var resolvedProperties = new Dictionary<string, IDictionary<string, string>>();


            foreach (var groupedProperty in groupedProperties)
            {
                var values = groupedProperty.ToDictionary(x => x.Key, x => x.Value);
                resolvedProperties.Add(groupedProperty.Key, values);
            }

            return resolvedProperties;
        }

        protected abstract IEnumerable<IGrouping<string, KeyValuePair<string, string>>> GroupProperties(IDictionary<string, string> properties);
    }
}