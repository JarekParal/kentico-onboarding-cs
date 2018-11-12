using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using TodoList.Contracts.DI;
using TodoList.Contracts.Models;
using TodoList.DI.DependencyResolvers;

namespace TodoList.DI.Tests.DependencyResolvers
{
    [TestFixture]
    public class DependencyResolverTest
    {
        private readonly Type _serviceTypeIn = typeof(Type);

        [Test]
        public void GetService_CheckCallContainerMethod()
        {
            var serviceTypeOut = typeof(Item);
            var container = Substitute.For<ITodoListContainer>();
            var resolver = new DependencyResolver(container);
            container.Resolve(_serviceTypeIn).Returns(serviceTypeOut);

            var result = resolver.GetService(_serviceTypeIn);

            Assert.That(result, Is.EqualTo(serviceTypeOut));
        }

        [Test]
        public void GetService_CheckCorrectBehaviorWhenResolveThrowsException()
        {
            var container = Substitute.For<ITodoListContainer>();
            var resolver = new DependencyResolver(container);
            var exceptionIn = new DependencyResolutionFailedException();
            container.Resolve(_serviceTypeIn).Throws(exceptionIn);

            var result = resolver.GetService(_serviceTypeIn);

            Assert.That(result, Is.EqualTo(null));
        }
        
        [Test]
        public void GetServices_CheckCallContainerMethod()
        {
            var serviceTypesOut = new List<object> {typeof(Item)};
            var container = Substitute.For<ITodoListContainer>();
            var resolver = new DependencyResolver(container);
            container.ResolveAll(_serviceTypeIn).Returns(serviceTypesOut);
            
            var result = resolver.GetServices(_serviceTypeIn);

            Assert.That(result, Is.EqualTo(serviceTypesOut));
        }

        [Test]
        public void GetServices_CheckCorrectBehaviorWhenResolveAllThrowsException()
        {
            var container = Substitute.For<ITodoListContainer>();
            var resolver = new DependencyResolver(container);
            var exceptionIn = new DependencyResolutionFailedException();
            container.ResolveAll(_serviceTypeIn).Throws(exceptionIn);

            var result = resolver.GetServices(_serviceTypeIn);

            Assert.That(result, Is.EqualTo(Enumerable.Empty<object>()));
        }

    }
}