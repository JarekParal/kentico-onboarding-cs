using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TodoList.Contracts.DI;
using TodoList.DI.Containers;
using TodoList.DI.Tests.Utils;
using Unity;

namespace TodoList.DI.Tests.Containers
{
    [TestFixture]
    public class TodoListContainerTest
    {
        private readonly Type _inputInterface = typeof(IFake);

        [Test]
        public void Resolve_Interface_InstanceOfTheInterface()
        {
            var outputInstance = new Fake();
            var unityContainer = Substitute.For<IUnityContainer>();
            unityContainer.Resolve(_inputInterface).Returns(outputInstance);
            var container = new TodoListContainer(unityContainer);

            var result = container.Resolve(_inputInterface);

            Assert.That(result, Is.EqualTo(outputInstance));
        }

        [Test]
        public void Resolve_NotRegisteredInterface_CheckThrowsException()
        {
            var container = new TodoListContainer();

            Assert.Throws<DependencyResolutionFailedException>(
                () => container.Resolve(_inputInterface)
            );
        }

        [Test]
        public void ResolveAll_Interface_InstancesOfTheInterface()
        {
            var outputListOfInstances = new List<object> {new Fake()};
            var unityContainer = Substitute.For<IUnityContainer>();
            unityContainer.Resolve(_inputInterface.MakeArrayType()).Returns(outputListOfInstances);
            var container = new TodoListContainer(unityContainer);

            var result = container.ResolveAll(_inputInterface);

            Assert.That(result, Is.EqualTo(outputListOfInstances));
        }

        [Test]
        public void ResolveAll_NotRegisteredInterface_EmptyCollection()
        {
            var container = new TodoListContainer();

            var result = container.ResolveAll(_inputInterface); // TODO: check if the test is correct

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void CreateChildContainer_NotNull()
        {
            var container = new TodoListContainer();

            var result = container.CreateChildContainer();

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Dispose_CheckIfIsCallOnUnityContainer()
        {
            var unityContainer = Substitute.For<IUnityContainer>();
            var container = new TodoListContainer(unityContainer);

            container.Dispose();

            unityContainer.Received(1).Dispose();
        }

        [Test]
        public void Dispose_CheckIfIsCallJustOneTimeOnUnityContainer()
        {
            var unityContainer = Substitute.For<IUnityContainer>();
            var container = new TodoListContainer(unityContainer);

            container.Dispose();
            container.Dispose();

            unityContainer.Received(1).Dispose();
        }
    }
}