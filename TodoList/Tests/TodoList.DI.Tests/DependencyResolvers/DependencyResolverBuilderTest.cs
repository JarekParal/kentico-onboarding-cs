using NSubstitute;
using NUnit.Framework;
using TodoList.Contracts.DI;
using TodoList.DI.DependencyResolvers;

namespace TodoList.DI.Tests.DependencyResolvers
{
    [TestFixture]
    public class DependencyResolverBuilderTest
    {
        private class FakeBootstrapper : IBootstrapper
        {
            public static int RegisterCallCounter { get; private set; }

            public ITodoListContainer Register(ITodoListContainer container)
            {
                ++RegisterCallCounter;
                return container;
            }

        }

        [Test]
        public void Bootsrap_CheckCallBootsrapperRegisterMethod()
        {
            var container = Substitute.For<ITodoListContainer>();
            var builder = new DependencyResolverBuilder(container);

            builder.Bootstrap<FakeBootstrapper>();

            Assert.That(FakeBootstrapper.RegisterCallCounter, Is.EqualTo(1));
        }
    }
}