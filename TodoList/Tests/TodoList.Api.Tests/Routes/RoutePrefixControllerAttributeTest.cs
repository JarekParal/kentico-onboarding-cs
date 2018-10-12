using NUnit.Framework;
using TodoList.Api.Controllers;
using TodoList.Api.Routes;

namespace TodoList.Api.Tests.Routes
{
    [TestFixture]
    public class RoutePrefixControllerAttributeTest
    {
        [Test]
        public void Prefix_WithAnyAndVersionV1_ReturnValidString()
        {
            var attribute = new RoutePrefixControllerAttribute(ApiVersion.V1, "Any");

            Assert.That(attribute.Prefix, Is.EqualTo("api/V1/Any"));
        }

        [Test]
        public void Prefix_WithAnyControllerAndVersionV1_ReturnValidString()
        {
            var attribute = new RoutePrefixControllerAttribute(ApiVersion.V1, "AnyController");

            Assert.That(attribute.Prefix, Is.EqualTo("api/V1/Any"));
        }

        [Test]
        public void Prefix_WithAnyControllerControllerAndVersionV1_ReturnValidString()
        {
            var attribute = new RoutePrefixControllerAttribute(ApiVersion.V1, "AnyControllerController");

            Assert.That(attribute.Prefix, Is.EqualTo("api/V1/Any"));
        }

        [Test]
        public void Prefix_WithAnyControllerControllerWithSpaceAndVersionV1_ReturnValidString()
        {
            var attribute = new RoutePrefixControllerAttribute(ApiVersion.V1, "An yControllerController");

            Assert.That(attribute.Prefix, Is.EqualTo("api/V1/An y"));
        }

        [Test]
        public void Prefix_WithMistakeInStringController_ReturnValidString()
        {
            var attribute = new RoutePrefixControllerAttribute(ApiVersion.V1, "AnyControl");

            Assert.That(attribute.Prefix, Is.EqualTo("api/V1/AnyControl"));
        }

        [Test]
        public void Prefix_WithEmptyControllerAndVersionV1_ReturnValidString()
        {
            var attribute = new RoutePrefixControllerAttribute(ApiVersion.V1, string.Empty);

            Assert.That(attribute.Prefix, Is.EqualTo("api/V1"));
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