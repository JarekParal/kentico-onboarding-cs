using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoList.Contracts.DI;
using TodoList.DI.DependencyResolvers;
using TodoList.DI.Tests.Utils;

namespace TodoList.DI.Tests.DependencyResolvers
{
    [TestFixture]
    public class DependencyResolverTest
    {
        private readonly Type _inputInterface = typeof(IFake);

        [Test]
        public void GetService_Interface_InstanceOfTheInterface()
        {
            var outputInstance = new Fake();
            var container = Substitute.For<ITodoListContainer>();
            container.Resolve(_inputInterface).Returns(outputInstance);
            var resolver = new DependencyResolver(container);

            var result = resolver.GetService(_inputInterface);

            Assert.That(result, Is.EqualTo(outputInstance));
        }

        [Test]
        public void GetService_UnregisteredInterface_EmptyEnumerable()
        {
            var exceptionWhichWillBeThrow =
                new DependencyResolutionFailedException("Could not resolve the interface.", new Exception());
            var container = Substitute.For<ITodoListContainer>();
            container.Resolve(_inputInterface).Throws(exceptionWhichWillBeThrow);
            var resolver = new DependencyResolver(container);

            var result = resolver.GetService(_inputInterface);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void GetServices_Interface_InstancesOfTheInterface()
        {
            var outputListOfInstances = new List<object> {new Fake()};
            var container = Substitute.For<ITodoListContainer>();
            container.ResolveAll(_inputInterface).Returns(outputListOfInstances);
            var resolver = new DependencyResolver(container);

            var result = resolver.GetServices(_inputInterface);

            Assert.That(result, Is.EqualTo(outputListOfInstances));
        }

        [Test]
        public void GetServices_UnregisteredInterface_EmptyEnumerable()
        {
            var exceptionWhichWillBeThrow =
                new DependencyResolutionFailedException("Could not resolve the interface.", new Exception());
            var container = Substitute.For<ITodoListContainer>();
            container.ResolveAll(_inputInterface).Throws(exceptionWhichWillBeThrow);
            var resolver = new DependencyResolver(container);

            var result = resolver.GetServices(_inputInterface);

            Assert.That(result, Is.EqualTo(Enumerable.Empty<object>()));
        }
    }
}