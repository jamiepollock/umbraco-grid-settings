using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Our.Umbraco.GridSettings.Services;

namespace Our.Umbraco.GridSettings.Tests.GridSettingsAttributesServiceTests
{
    public class GetStyleAttributeTests
    {
        private const string validJsonWithStylesAndConfig = @"{
          ""styles"": {
            ""background-position"": ""right top"",
            ""background-repeat"": ""no-repeat""
          }
        }";

        [Test]
        public void GivenValidConfiguration_ReturnValidAttribute()
        {
            // Arrange
            var contentItem = JObject.Parse(validJsonWithStylesAndConfig);
            var service = new GridSettingsAttributesService();

            // Act
            var styleAttribute = service.GetStyleAttribute(contentItem);

            // Assert
            Assert.AreEqual("background-position:right top; background-repeat:no-repeat;", styleAttribute.Value);
        }

        [Test]
        public void GivenNoConfiguration_ReturnInvalidAttribute()
        {
            // Arrange
            var contentItem = new JObject();
            var service = new GridSettingsAttributesService();

            // Act
            var styleAttribute = service.GetStyleAttribute(contentItem);

            // Assert
            Assert.IsFalse(styleAttribute.IsValid());
        }
    }
}
