using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Our.Umbraco.GridSettings.Services;
using Our.Umbraco.GridSettings.ValueResolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Our.Umbraco.GridSettings.Tests.GridSettingsAttributesServiceTests
{
    [TestFixture]
    public class GetSettingsAttributesTests
    {
        private const string validJson = @"{
          ""config"": {
            ""class"": ""color-scheme-red"",
            ""data-title"": ""My Configuration""
          }
        }";

        private const string jsonWithBlankValuesAndKeys = @"{
          ""config"": {
            ""class"": ""color-scheme-red"",
            ""data-title"": ""My Configuration"",
            ""foo"": """",
            """": ""bar""
          }
        }";

        private const string validJsonWithRelatedKeys = @"{
          ""config"": {
            ""class_color-scheme"": ""color-scheme-red"",
            ""class_padding"": ""padding-medium"",
            ""data-title"": ""My Configuration""
          }
        }";

        [Test]
        public void GivenValidConfiguration_ReturnValidAttribute()
        {
            // Arrange
            var contentItem = JObject.Parse(jsonWithBlankValuesAndKeys);
            var service = new GridSettingsAttributesService();

            // Act
            var attributes = service.GetSettingsAttributes(contentItem);

            // Assert
            Assert.AreEqual(2, attributes.Count);
        }


        [Test]
        public void GivenConfigurationWithBlankKeyOrValues_ReturnOnlyValidAttributes()
        {
            // Arrange
            var contentItem = JObject.Parse(validJson);
            var service = new GridSettingsAttributesService();

            // Act
            var attributes = service.GetSettingsAttributes(contentItem);

            // Assert
            Assert.AreEqual(2, attributes.Count);
        }

        [Test]
        public void GivenConfigurationWithResolvableKeys_and_AnAttributeResolver_ReturnAttributesWithConcatanatedValues()
        {
            // Arrange
            var contentItem = JObject.Parse(validJsonWithRelatedKeys);

            var attributesResolver = new IndexOfTokenGridSettingsAttributesResolver("_");
            var service = new GridSettingsAttributesService(attributesResolver);

            // Act
            var attributes = service.GetSettingsAttributes(contentItem);

            // Assert
            Assert.AreEqual(2, attributes.Count);
        }

        public void GivenConfigurationWithoutResolvableKeys_and_AnAttributeResolver_ReturnAttributes()
        {
            // Arrange
            var contentItem = JObject.Parse(validJson);

            var attributesResolver = new IndexOfTokenGridSettingsAttributesResolver("_");
            var service = new GridSettingsAttributesService(attributesResolver);

            // Act
            var attributes = service.GetSettingsAttributes(contentItem);

            // Assert
            Assert.AreEqual(2, attributes.Count);
        }

        [Test]
        public void GivenConfigurationWithResolvableKeys_and_NoAttributeResolver_ReturnAttributesWithoutConcatanatedValues()
        {
            // Arrange
            var contentItem = JObject.Parse(validJsonWithRelatedKeys);
            
            var service = new GridSettingsAttributesService();

            // Act
            var attributes = service.GetSettingsAttributes(contentItem);

            // Assert
            Assert.AreEqual(3, attributes.Count);
        }
    }
}
