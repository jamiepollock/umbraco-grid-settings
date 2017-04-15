using System.Collections.Generic;
using System.Linq;

namespace Our.Umbraco.GridSettings.Resolvers
{
    /// <summary>
    /// An <see cref="IGridSettingsAttributesResolver"/> which looks for a specific token in an Attribute name. If found it will group all found attributes together under this name.
    /// </summary>
    public sealed class GroupByPrefixTokenGridSettingsAttributesResolver : GroupByGridSettingsAttributesResolver
    {
        private readonly string _token;
        /// <summary>
        /// Constructs a new <see cref="GroupByPrefixTokenGridSettingsAttributesResolver"/> with a given token.
        /// </summary>
        /// <param name="token">The <see cref="string"/> token of which to look for.</param>
        public GroupByPrefixTokenGridSettingsAttributesResolver(string token)
        {
            _token = token;
        }
        protected override IEnumerable<IGrouping<string, KeyValuePair<string, string>>> GroupProperties(IDictionary<string, string> properties)
        {
            return properties.GroupBy(x => SplitByTokenIfItExists(x.Key));
        }

        private string SplitByTokenIfItExists(string name)
        {
            if (name.IndexOf(_token) > -1)
            {
                return name.Substring(0, name.IndexOf(_token));
            }
            return name;
        }
    }
}
