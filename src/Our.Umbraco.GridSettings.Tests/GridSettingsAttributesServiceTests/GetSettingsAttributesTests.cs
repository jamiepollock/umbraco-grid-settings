using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Our.Umbraco.GridSettings.Services;
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
        public void GivenConfigurationWithBlankKeyOrValues_ReturnOnlyValidAttribute()
        {
            // Arrange
            var contentItem = JObject.Parse(validJson);
            var service = new GridSettingsAttributesService();

            // Act
            var attributes = service.GetSettingsAttributes(contentItem);

            // Assert
            Assert.AreEqual(2, attributes.Count);
        }
    }
}
