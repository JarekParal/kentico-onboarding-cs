using NSubstitute;
using NUnit.Framework;
using TodoList.Contracts.DI;
using TodoList.DI.Containers;
using TodoList.DI.DependencyResolvers;

namespace TodoList.DI.Tests.DependencyResolvers
{
    [TestFixture]
    public class DependencyResolverBuilderTest
    {
        private class FakeFirstBootstrapper : IBootstrapper
        {
            public static int RegisterCallCounter { get; private set; }

            public static void ResetRegisterCallCounter()
            {
                RegisterCallCounter = 0;
            }

            public ITodoListContainer Register(ITodoListContainer container)
            {
                ++RegisterCallCounter;
                return container;
            }
        }

        private class FakeSecondBootstrapper : IBootstrapper
        {
            public static int RegisterCallCounter { get; private set; }

            public static void ResetRegisterCallCounter()
            {
                RegisterCallCounter = 0;
            }

            public ITodoListContainer Register(ITodoListContainer container)
            {
                ++RegisterCallCounter;
                return container;
            }
        }

        [SetUp]
        public void InitFakeBootstreppers()
        {
            FakeFirstBootstrapper.ResetRegisterCallCounter();
            FakeSecondBootstrapper.ResetRegisterCallCounter();
        }

        [Test]
        public void Bootsrap_CheckCallForOneBootsrapperRegisterMethod()
        {
            var container = Substitute.For<TodoListContainer>();
            var builder = new DependencyResolverBuilder(container);

            builder.Bootstrap<FakeFirstBootstrapper>();

            Assert.That(FakeFirstBootstrapper.RegisterCallCounter, Is.EqualTo(1));
        }

        [Test]
        public void Bootsrap_CheckCallForTwoBootsrapperRegisterMethod()
        {
            var container = Substitute.For<TodoListContainer>();
            var builder = new DependencyResolverBuilder(container);

            builder.Bootstrap<FakeFirstBootstrapper>();
            builder.Bootstrap<FakeSecondBootstrapper>();

            Assert.Multiple(() =>
            {
                Assert.That(FakeFirstBootstrapper.RegisterCallCounter, Is.EqualTo(1));
                Assert.That(FakeSecondBootstrapper.RegisterCallCounter, Is.EqualTo(1));
            });
        }

        [Test]
        public void Build_FakeBootstrapper_NotNull()
        {
            var builder = new DependencyResolverBuilder();
            builder.Bootstrap<FakeFirstBootstrapper>();

            var result = builder.Build();

            Assert.That(result, Is.Not.Null);
        }
    }
}