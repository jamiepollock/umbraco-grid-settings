using Newtonsoft.Json.Linq;
using Our.Umbraco.GridSettings.Resolvers;
using Our.Umbraco.GridSettings.Services;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Our.Umbraco.GridSettings.Web
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders the Settings &amp; Styles associated with the current <see cref="JObject"/>.
        /// </summary>
        /// <param name="helper">The <see cref="HtmlHelper"/> instance.</param>
        /// <param name="contentItem">The <see cref="JObject"/> item which holds the configuration for Settings & Styles. This is typically an area or row in the Umbraco Grid.</param>
        /// <param name="attributesResolver">Optional Attributes Resolver: this provides bespoke logic for grouping attributes together.</param>
        /// <param name="attributeValueResolvers">Optional Attribute Value Resolver: Once attributes are grouped Attribute Value Resolvers can tailor the output of a given grouping.</param>
        /// <param name="defaultAttributeValueResolver">Optional Default Attribute Value Resolver: If an <see cref="IGridSettingsAttributeValueResolver"/> can not be found for a specific property this <see cref="IGridSettingsAttributeValueResolver"/> is used insteads.</param>
        /// <returns>A <see cref="MvcHtmlString" /> containing all the resolved attributes with their values.</returns>
        public static MvcHtmlString RenderGridSettingAttributes(this HtmlHelper helper, JObject contentItem, IGridSettingsAttributesResolver attributesResolver = null, IDictionary<string, IGridSettingsAttributeValueResolver> attributeValueResolvers = null, IGridSettingsAttributeValueResolver defaultAttributeValueResolver = null)
        {
            var attributesService = new GridSettingsAttributesService(attributesResolver, defaultAttributeValueResolver, attributeValueResolvers);

            return helper.RenderGridSettingAttributes(contentItem, attributesService);
        }
        /// <summary>
        /// Renders the Settings &amp; Styles associated with the <paramref name="contentItem"/>.
        /// </summary>
        /// <typeparam name="TContentItem">The type used for the content item</typeparam>
        /// <param name="helper">The <see cref="HtmlHelper"/> instance.</param>
        /// <param name="contentItem">The item which holds the configuration for Settings & Styles. This is typically an area or row in the Umbraco Grid.</param>
        /// <param name="attributesService">An <see cref="IGridSettingsAttributesService" /> which handles the resolution of attributes.</param>
        /// <returns>A <see cref="MvcHtmlString" /> containing all the resolved attributes with their values.</returns>
        public static MvcHtmlString RenderGridSettingAttributes<TContentItem>(this HtmlHelper helper, TContentItem contentItem, IGridSettingsAttributesService<TContentItem> attributesService)
        {
            var attributes = attributesService.GetAllAttributes(contentItem);

            var output = new StringBuilder();
            foreach (var attribute in attributes)
            {
                output.AppendFormat(" {0}=\"{1}\"", attribute.Key, helper.AttributeEncode(attribute.Value));
            }

            return new MvcHtmlString(output.ToString());
        }
    }
}
