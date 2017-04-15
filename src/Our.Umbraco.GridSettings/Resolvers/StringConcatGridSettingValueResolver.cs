using System.Collections.Generic;
using System.Linq;

namespace Our.Umbraco.GridSettings.Resolvers
{
    public class StringConcatGridSettingValueResolver : IGridSettingsAttributeValueResolver
    {
        private readonly string _separator;

        public StringConcatGridSettingValueResolver(string separator = " ")
        {
            _separator = separator;
        }

        public string ResolveAttributeValue(KeyValuePair<string, IDictionary<string, string>> property)
        {
            var values = property.Value.Select(x => x.Value.ToString());

            return string.Join(_separator, values);
        }
    }
}
