using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Our.Umbraco.GridSettings.Services;

namespace Our.Umbraco.GridSettings.Tests
{
    [TestFixture]
    public class GridSettingsAttributesServiceTests
    {
        private const string validJsonWithStylesAndConfig = @"{
          ""styles"": {
            ""background-position"": ""right top"",
            ""background-repeat"": ""no-repeat""
          },
          ""config"": {
            ""class"": ""color-scheme-red"",
            ""data-title"": ""My Configuration""
          }
        }";

        private const string validJsonWithOnlyStyles = @"{
          ""styles"": {
            ""background-position"": ""right top"",
            ""background-repeat"": ""no-repeat""
          }
        }";

        [Test]
        public void GivenNoConfiguration_ReturnNoAttributes()
        {
            // Arrange
            var contentItem = new JObject();
            var service = new GridSettingsAttributesService();

            // Act
            var attributes = service.GetAllAttributes(contentItem);
            
            // Assert
            Assert.IsEmpty(attributes);
        }

        [Test]
        public void GivenValidConfigurationWithStylesAndConfig_ReturnAttributes()
        {
            // Arrange
            var contentItem = JObject.Parse(validJsonWithStylesAndConfig);
            var service = new GridSettingsAttributesService();

            // Act
            var attributes = service.GetAllAttributes(contentItem);

            // Assert
            Assert.AreEqual(3, attributes.Count);
        }


        [Test]
        public void GivenValidConfigurationWithOnlyStyles_ReturnOnlyStyleAttribute()
        {
            // Arrange
            var contentItem = JObject.Parse(validJsonWithOnlyStyles);
            var service = new GridSettingsAttributesService();

            // Act
            var attributes = service.GetAllAttributes(contentItem);

            // Assert
            Assert.AreEqual(1, attributes.Count);
            Assert.IsTrue(attributes.ContainsKey("style"));
        }
    }
}
