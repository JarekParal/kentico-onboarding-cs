using System.Web.Http.Dependencies;
using TodoList.Contracts.DI;
using TodoList.DI.Containers;
using TodoList.DI.Providers;

namespace TodoList.DI.DependencyResolvers
{
    public class DependencyResolverBuilder
    {
        private readonly TodoListContainer _container;

        public DependencyResolverBuilder() : this(new TodoListContainer())
        {
        }

        internal DependencyResolverBuilder(TodoListContainer container)
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
            var unityContainer = _container.ReleaseUnityContainer();

            return new DependencyResolver(new TodoListProvider(unityContainer));
        }
    }
}