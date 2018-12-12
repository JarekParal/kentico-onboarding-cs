using System;
using TodoList.Contracts.DI;
using TodoList.DI.Extensions;
using Unity;
using Unity.Injection;

namespace TodoList.DI.Containers
{
    internal sealed class TodoListContainer : ITodoListContainer
    {
        private bool _disposed;
        internal readonly IUnityContainer Container;

        public TodoListContainer()
            => Container = new UnityContainer();

        internal TodoListContainer(IUnityContainer container)
            => Container = container;

        public ITodoListContainer RegisterType<TContract, TImplementation>(Lifetime lifetime)
            where TImplementation : TContract
        {
            Container.RegisterType<TContract, TImplementation>(lifetime.GetUnityLifetimeManager());

            return this;
        }

        public ITodoListContainer RegisterType<TContract>(Lifetime lifetime,
            Func<object> factoryFunc)
        {
            Container.RegisterType<TContract>(
                lifetime.GetUnityLifetimeManager(),
                new InjectionFactory(_ => factoryFunc())
            );

            return this;
        }

        internal IUnityContainer ReleaseUnityContainer()
        {
            _disposed = true; // TODO: is that save?

            return Container;
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