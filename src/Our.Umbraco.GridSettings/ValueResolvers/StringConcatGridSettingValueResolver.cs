using Newtonsoft.Json.Linq;
using System.Linq;

namespace Our.Umbraco.GridSettings.ValueResolvers
{
    public class StringConcatGridSettingValueResolver : IGridSettingsAttributeValueResolver
    {
        private readonly string _separator;

        public StringConcatGridSettingValueResolver(string separator = " ")
        {
            _separator = separator;
        }

        public string ResolveAttributeValue(IGrouping<string, JProperty> property)
        {
            var values = property.Select(x => x.Value.ToString());

            return string.Join(_separator, values);
        }
    }
}
