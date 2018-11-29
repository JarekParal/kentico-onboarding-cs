using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TodoList.Contracts.DI;
using TodoList.Contracts.Models;
using TodoList.DI.Containers;
using Unity;

namespace TodoList.DI.Tests.Containers
{
    [TestFixture]
    public class TodoListContainerTest
    {
        private readonly Type _serviceTypeIn = typeof(Type);

        [Test]
        public void Resolve_CheckCallContainerResolveMethod()
        {
            var serviceTypeOut = typeof(Item);
            var unityContainer = Substitute.For<IUnityContainer>();
            var container = new TodoListContainer(unityContainer);
            unityContainer.Resolve(_serviceTypeIn).Returns(serviceTypeOut);

            var result = container.Resolve(_serviceTypeIn);

            Assert.That(result, Is.EqualTo(serviceTypeOut));
        }

        [Test]
        public void Resolve_NotRegisteredType_CheckThrowsException()
        {
            var container = new TodoListContainer();

            Assert.Throws<DependencyResolutionFailedException>(
                () => container.Resolve(_serviceTypeIn)
            );
        }

        [Test]
        public void ResolveAll_CheckCallContainerResolveAllMethod()
        {
            var serviceTypesOut = new List<object> {typeof(Item)};
            var unityContainer = Substitute.For<IUnityContainer>();
            var container = new TodoListContainer(unityContainer);
            unityContainer.Resolve(_serviceTypeIn.MakeArrayType()).Returns(serviceTypesOut);

            var result = container.ResolveAll(_serviceTypeIn);

            Assert.That(result, Is.EqualTo(serviceTypesOut));
        }
    }
}