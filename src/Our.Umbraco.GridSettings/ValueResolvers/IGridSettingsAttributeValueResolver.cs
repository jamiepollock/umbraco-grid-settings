using Newtonsoft.Json.Linq;
using System.Linq;

namespace Our.Umbraco.GridSettings.ValueResolvers
{
    public interface IGridSettingsAttributeValueResolver
    {
        string ResolveAttributeValue(IGrouping<string, JProperty> property);
    }
}
