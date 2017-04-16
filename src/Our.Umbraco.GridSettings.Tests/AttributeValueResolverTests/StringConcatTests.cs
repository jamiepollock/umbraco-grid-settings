using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Our.Umbraco.GridSettings.Resolvers;

namespace Our.Umbraco.GridSettings.Tests.AttributeValueResolverTests
{
    [TestFixture]
    public class StringConcatTests
    {
        [TestCase(null, "foobarhelloworld")]
        [TestCase("", "foobarhelloworld")]
        [TestCase(" ", "foo bar hello world")]
        [TestCase("_", "foo_bar_hello_world")]
        [TestCase(",", "foo,bar,hello,world")]
        public void GivenAToken_ExpectCorrectOutput(string token, string outcome)
        {
            var attributeValues = new Dictionary<string, string>()
            {
                { "attr_1", "foo" },
                { "attr_2", "bar" },
                { "attr_3", "hello" },
                { "attr_4", "world" }
            };

            var attribute = new KeyValuePair<string, IDictionary<string, string>>("attr", attributeValues);
            var valueResolver = new StringConcatGridSettingValueResolver(token);

            var value = valueResolver.ResolveAttributeValue(attribute);

            Assert.AreEqual(outcome, value);
        }
        [Test]
        public void GivenAnAttributeWithEmptyStringValues_ExpectWhiteSpaceValuesAreIgnoredByDefault()
        {
            var attributeValues = new Dictionary<string, string>()
            {
                { "attr_1", "foo" },
                { "attr_2", "" },
                { "attr_3", "   " },
                { "attr_4", "bar" }
            };

            var attribute = new KeyValuePair<string, IDictionary<string, string>>("attr", attributeValues);
            var valueResolver = new StringConcatGridSettingValueResolver();

            var value = valueResolver.ResolveAttributeValue(attribute);

            Assert.AreEqual("foo bar", value);
        }


        [Test]
        public void GivenAnAttributeWithEmptyOrNullStringValues_And_StringConcatBehaviorRespectNullOrWhiteSpace_ExpectWhiteSpaceValuesAreRespected()
        {
            var attributeValues = new Dictionary<string, string>()
            {
                { "attr_1", "foo" },
                { "attr_2", "" },
                { "attr_3", null },
                { "attr_4", "   " },
                { "attr_5", "bar" }
            };

            var attribute = new KeyValuePair<string, IDictionary<string, string>>("attr", attributeValues);
            var valueResolver = new StringConcatGridSettingValueResolver(",", StringConcatBehavior.RespectNullOrWhiteSpace);

            var value = valueResolver.ResolveAttributeValue(attribute);

            Assert.AreEqual("foo,,,   ,bar", value);
        }
    }
}
