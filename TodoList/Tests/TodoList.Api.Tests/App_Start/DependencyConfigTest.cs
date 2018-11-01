using System;
using System.Linq;
using System.Web.Http;
using NSubstitute;
using NUnit.Framework;
using TodoList.Contracts.DI;
using TodoList.Contracts.Models;
using TodoList.DI.Containers;
using TodoList.DI.DependencyResolvers;

namespace TodoList.Api.Tests
{
    [TestFixture]
    public class DependencyConfigTest
    {
        private readonly Type[] _ignoredTypes =
        {
            typeof(IBootstrapper),
            typeof(ITodoListContainer)
        };

        private Type[] _requiredTypes;

        [SetUp]
        public void SetUp()
        {
            _requiredTypes = typeof(Item).Assembly
                .GetExportedTypes()
                .Where(type => type.IsInterface)
                .Except(_ignoredTypes)
                .ToArray();
        }

        [Test]
        public void GetDependencyWithConfig_ReturnValidConfiguration()
        {
            var config = Substitute.For<HttpConfiguration>();
            Type[] containerRegistrations = null;

            DependencyConfig.Register(config);
            if (config.DependencyResolver is DependencyResolver dependencyResolver)
            {
                if (dependencyResolver.Container is TodoListContainer container)
                {
                    var unityContainer = container.Container;
                    containerRegistrations =
                        unityContainer.Registrations
                            .Select(type => type.RegisteredType)
                            .ToArray();
                }
            }

            Assert.That(containerRegistrations, Is.Not.Null);
            Assert.That(containerRegistrations, Is.SupersetOf(_requiredTypes),
                "Not all required dependencies are correctly resolved:");
            Assert.That(containerRegistrations, Is.Not.SubsetOf(_ignoredTypes),
                "Some unassociated dependencies are resolved:");
        }
    }
}