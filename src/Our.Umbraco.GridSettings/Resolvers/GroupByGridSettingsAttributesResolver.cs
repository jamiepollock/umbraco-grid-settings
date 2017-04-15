using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Our.Umbraco.GridSettings.Resolvers
{
    public abstract class GroupByGridSettingsAttributesResolver : IGridSettingsAttributesResolver
    {
        public IDictionary<string, IEnumerable<JProperty>> ResolveSettingsAttributes(IEnumerable<JProperty> properties)
        {
            var groupedProperties = GroupProperties(properties);

            return groupedProperties.ToDictionary(g => g.Key,
                                                  g => g.Select(x => x));
        }

        protected abstract IEnumerable<IGrouping<string, JProperty>> GroupProperties(IEnumerable<JProperty> properties);
    }
}