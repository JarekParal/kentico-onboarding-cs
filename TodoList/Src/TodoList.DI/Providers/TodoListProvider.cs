using System;
using System.Collections.Generic;
using TodoList.Contracts.DI;
using Unity;
using Unity.Exceptions;

namespace TodoList.DI.Providers
{
    public sealed class TodoListProvider : ITodoListProvider
    {
        private bool _disposed;
        internal readonly IUnityContainer Container;

        internal TodoListProvider()
            => Container = new UnityContainer();

        internal TodoListProvider(IUnityContainer container)
            => Container = container;

        public object Resolve(Type type)
            => ResolveTypes(() => Container.Resolve(type));

        public IEnumerable<object> ResolveAll(Type type)
            => ResolveTypes(() => Container.ResolveAll(type));

        public ITodoListProvider CreateChildContainer()
        {
            var newChildContainer = Container.CreateChildContainer();

            return new TodoListProvider(newChildContainer);
        }

        private static T ResolveTypes<T>(Func<T> resolveFunc)
        {
            try
            {
                return resolveFunc();
            }
            catch (ResolutionFailedException exception)
            {
                throw new DependencyResolutionFailedException(
                    exception.Message,
                    exception
                );
            }
        }
        public void Dispose()
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