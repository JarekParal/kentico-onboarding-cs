using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using TodoList.Contracts.DI;
using TodoList.DI.Containers;
using Unity.Exceptions;

namespace TodoList.DI.DependencyResolvers
{
    public class DependencyResolver : IDependencyResolver
    {
        public ITodoListContainer Container { get; }

        public DependencyResolver()
            => Container = new TodoListContainer();

        private DependencyResolver(ITodoListContainer todoListContainer)
            => Container = todoListContainer;

        public object GetService(Type serviceType)
        {
            try
            {
                return Container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return Container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = Container.CreateChildContainer();
            return new DependencyResolver(child);
        }

        public void Dispose()
            => Dispose(true);

        protected virtual void Dispose(bool disposing)
            => Container.Dispose();
    }
}