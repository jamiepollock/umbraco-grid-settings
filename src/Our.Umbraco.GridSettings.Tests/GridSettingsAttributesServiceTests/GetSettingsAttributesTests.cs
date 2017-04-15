using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Our.Umbraco.GridSettings.Services;
using Our.Umbraco.GridSettings.ValueResolvers;
using System.Collections.Generic;


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

        private const string validJsonWithRelatedKeysAndCsv = @"{
          ""config"": {
            ""data-csv_first"": ""first"",
            ""data-csv_second"": ""second"",
            ""data-csv_third"": ""third"",
          }
        }";
        private const string validJsonWithMultipleRelatedKeys = @"{
          ""config"": {
            ""data-csv_first"": ""first"",
            ""data-csv_second"": ""second"",
            ""data-csv_third"": ""third"",
            ""data-hyphenated_first"": ""first"",
            ""data-hyphenated_second"": ""second"",
            ""data-hyphenated_third"": ""third"",
            ""data-default_first"": ""first"",
            ""data-default_second"": ""second"",
            ""data-default_third"": ""third""
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


        [Test]
        public void GivenConfigurationWithResolvableKeys_and_CsvValueResolver_ReturnAttributesWithCsvConcatanatedValues()
        {
            // Arrange
            var contentItem = JObject.Parse(validJsonWithRelatedKeysAndCsv);

            var attributesResolver = new IndexOfTokenGridSettingsAttributesResolver("_");
            var defaultValueResolver = new StringConcatGridSettingValueResolver(",");
            var service = new GridSettingsAttributesService(attributesResolver, defaultValueResolver);

            // Act
            var attributes = service.GetSettingsAttributes(contentItem);

            // Assert
            Assert.IsTrue(attributes.ContainsKey("data-csv"));
            Assert.AreEqual("first,second,third", attributes["data-csv"]);
        }

        [Test]
        public void GivenConfigurationWithResolvableKeys_and_MultipleValueResolvers_ReturnAttributesWithConcatanatedValues()
        {
            // Arrange
            var contentItem = JObject.Parse(validJsonWithMultipleRelatedKeys);

            var attributesResolver = new IndexOfTokenGridSettingsAttributesResolver("_");
            var defaultValueResolver = new StringConcatGridSettingValueResolver(" ");
            var valueResolvers = new Dictionary<string, IGridSettingsAttributeValueResolver>()
            {
                { "data-csv", new StringConcatGridSettingValueResolver(",") },
                { "data-hyphenated", new StringConcatGridSettingValueResolver("-") }
            };

            var service = new GridSettingsAttributesService(attributesResolver, defaultValueResolver, valueResolvers);

            // Act
            var attributes = service.GetSettingsAttributes(contentItem);

            // Assert
            Assert.IsTrue(attributes.ContainsKey("data-csv"));
            Assert.IsTrue(attributes.ContainsKey("data-hyphenated"));
            Assert.IsTrue(attributes.ContainsKey("data-default"));
            Assert.AreEqual("first,second,third", attributes["data-csv"]);
            Assert.AreEqual("first-second-third", attributes["data-hyphenated"]);
            Assert.AreEqual("first second third", attributes["data-default"]);
        }
    }
}
