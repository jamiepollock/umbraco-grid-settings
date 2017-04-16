using System.Collections.Generic;
using System.Linq;

namespace Our.Umbraco.GridSettings.Resolvers
{
    /// <summary>
    /// A <see cref="IGridSettingsAttributeValueResolver"/> which concatenates found attributes together with a given token.
    /// </summary>
    public class StringConcatGridSettingValueResolver : IGridSettingsAttributeValueResolver
    {
        private readonly string _separator;
        private readonly StringConcatBehavior _concatBehavior;

        public StringConcatGridSettingValueResolver(string separator = " ", StringConcatBehavior concatBehavior = StringConcatBehavior.IgnoreNullOrWhiteSpace)
        {
            _separator = separator;
            _concatBehavior = concatBehavior;
        }

        public string ResolveAttributeValue(KeyValuePair<string, IDictionary<string, string>> property)
        {
            var values = property.Value.Select(x => x.Value);

            if (_concatBehavior == StringConcatBehavior.IgnoreNullOrWhiteSpace)
            {
                return string.Join(_separator, values.Where(x => string.IsNullOrWhiteSpace(x) == false));
            }

            return string.Join(_separator, values);
        }
    }

    public enum StringConcatBehavior
    {
        IgnoreNullOrWhiteSpace,
        RespectNullOrWhiteSpace
    }
}