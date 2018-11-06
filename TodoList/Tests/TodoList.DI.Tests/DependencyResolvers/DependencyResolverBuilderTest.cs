using NSubstitute;
using NUnit.Framework;
using TodoList.Contracts.DI;
using TodoList.DI.DependencyResolvers;

namespace TodoList.DI.Tests.DependencyResolvers
{
    [TestFixture]
    public class DependencyResolverBuilderTest
    {
        [Test]
        public void Bootsrap()
        {
            var container = Substitute.For<ITodoListContainer>();
            var bootsrapper = Substitute.For<IBootstrapper>();
            var builder = new DependencyResolverBuilder(container);

            builder.Bootstrap(bootsrapper);

            bootsrapper.Received(1).Register(container);
        }
    }
}