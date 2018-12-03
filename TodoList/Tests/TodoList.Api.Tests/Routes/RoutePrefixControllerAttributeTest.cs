using NUnit.Framework;
using TodoList.Api.Controllers;
using TodoList.Api.Routes;

namespace TodoList.Api.Tests.Routes
{
    [TestFixture]
    public class RoutePrefixControllerAttributeTest
    {
        [TestCase("Any")]
        [TestCase("AnyController")]
        [TestCase("AnyControllerController")]
        public void Prefix_WithAllPossibleNames_ReturnValidString(string input)
        {
            var attribute = new RoutePrefixControllerAttribute(ApiVersion.V1, input);

            Assert.That(attribute.Prefix, Is.EqualTo("api/V1/Any"));
        }

        [TestCase("An yControllerController", "api/V1/An y")]
        [TestCase("AnyControl", "api/V1/AnyControl")]
        [TestCase("", "api/V1")]
        public void Prefix_WithInvalidNames_ReturnWrongString(string input, string expectedOutput)
        {
            var attribute = new RoutePrefixControllerAttribute(ApiVersion.V1, input);

            Assert.That(attribute.Prefix, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void Prefix_WithNullAsControllerAndVersionV1_ReturnException()
        {
            Assert.That(
                () => new RoutePrefixControllerAttribute(ApiVersion.V1, null),
                Throws.ArgumentNullException
            );
        }
    }
}