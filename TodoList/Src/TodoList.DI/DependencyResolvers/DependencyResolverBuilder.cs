using System.Web.Http.Dependencies;
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

        public DependencyResolverBuilder Bootstrap<TBootstrapper>()
            where TBootstrapper : IBootstrapper, new()
        {
            IBootstrapper bootstrapper = new TBootstrapper();
            bootstrapper.Register(_container);
            return this;
        }

        public IDependencyResolver Build()
        {
            return new DependencyResolver(_container.GetProvider());
        }
    }
}