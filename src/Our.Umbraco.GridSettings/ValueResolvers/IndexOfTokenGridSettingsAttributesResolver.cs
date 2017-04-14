using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System;

namespace Our.Umbraco.GridSettings.ValueResolvers
{
    /// <summary>
    /// An <see cref="IGridSettingsAttributesResolver"/> which looks for a specific token in an Attribute name. If found it will group all found <see cref="JProperty"/> together.
    /// </summary>
    public class IndexOfTokenGridSettingsAttributesResolver : IGridSettingsAttributesResolver
    {
        private readonly string _token;

        public IndexOfTokenGridSettingsAttributesResolver(string token)
        {
            _token = token;
        }

        public IEnumerable<IGrouping<string, JProperty>> ResolveSettingsAttributes(IEnumerable<JProperty> properties)
        {
            return properties.GroupBy(x => SplitByTokenIfItExists(x.Name));
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
