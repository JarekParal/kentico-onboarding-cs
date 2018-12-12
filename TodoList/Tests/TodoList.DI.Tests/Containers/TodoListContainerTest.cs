using NSubstitute;
using NUnit.Framework;
using TodoList.DI.Containers;
using Unity;

namespace TodoList.DI.Tests.Containers
{
    [TestFixture]
    public class TodoListContainerTest
    {

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