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
    }
}