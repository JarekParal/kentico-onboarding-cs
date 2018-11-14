using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using TodoList.Contracts.DI;
using TodoList.DI.Containers;

namespace TodoList.DI.DependencyResolvers
{
    public class DependencyResolver : ITodoListDependencyResolver
    {
        private bool _disposed;
        internal readonly ITodoListContainer Container;
        
        internal DependencyResolver(ITodoListContainer todoListContainer)
            => Container = todoListContainer;

        public object GetService(Type serviceType)
            => GetService(() => Container.Resolve(serviceType));

        public IEnumerable<object> GetServices(Type serviceType)
            => GetService(() => Container.ResolveAll(serviceType)) ?? Enumerable.Empty<object>();

        private static T GetService<T>(Func<T> resolveFunc) where T : class 
        {
            try
            {
                return resolveFunc();
            }
            catch (DependencyResolutionFailedException)
            {
                return null;
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
        {
            if (_disposed)
            {
                return;
            }

            Container.Dispose();
            _disposed = true;
        }
    }
}