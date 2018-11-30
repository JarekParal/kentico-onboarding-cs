using TodoList.Contracts.DI;
using TodoList.DI.Containers;

namespace TodoList.DI.DependencyResolvers
{
    public class DependencyResolverBuilder
    {
        private readonly ITodoListContainer _container;

        public DependencyResolverBuilder() : this(new TodoListContainer())
        {
        }

        internal DependencyResolverBuilder(ITodoListContainer container)
        {
            _container = container;
        }

        public DependencyResolverBuilder Bootstrap(IBootstrapper bootstrapper)
        {
            bootstrapper.Register(_container);
            return this;
        }

        public ITodoListDependencyResolver Build()
        {
            return new DependencyResolver(_container);
        }
    }
}