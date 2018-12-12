using System;
using System.Collections.Generic;
using TodoList.Contracts.DI;
using TodoList.DI.Extensions;
using Unity;
using Unity.Exceptions;
using Unity.Injection;

namespace TodoList.DI.Containers
{
    internal sealed class TodoListContainer : ITodoListContainer, ITodoListProvider
    {
        private bool _disposed;
        internal readonly IUnityContainer Container;

        internal TodoListContainer() : this(new UnityContainer())
        {
        }

        internal TodoListContainer(IUnityContainer container)
            => Container = container;

        public ITodoListContainer RegisterType<TContract, TImplementation>(Lifetime lifetime)
            where TImplementation : TContract
        {
            Container.RegisterType<TContract, TImplementation>(lifetime.GetUnityLifetimeManager());

            return this;
        }

        public ITodoListContainer RegisterType<TContract>(
            Lifetime lifetime,
            Func<object> factoryMethod
        )
        {
            Container.RegisterType<TContract>(
                lifetime.GetUnityLifetimeManager(),
                new InjectionFactory(_ => factoryMethod())
            );

            return this;
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

        public ITodoListProvider GetProvider()
            => this;

        public object Resolve(Type type)
            => ResolveTypes(() => Container.Resolve(type));

        public IEnumerable<object> ResolveAll(Type type)
            => ResolveTypes(() => Container.ResolveAll(type));

        public ITodoListProvider CreateChildContainer()
        {
            var newChildContainer = Container.CreateChildContainer();

            return new TodoListContainer(newChildContainer).GetProvider();
        }

        private static T ResolveTypes<T>(Func<T> resolveMethod)
        {
            try
            {
                return resolveMethod();
            }
            catch (ResolutionFailedException exception)
            {
                throw new DependencyResolutionFailedException(
                    exception.Message,
                    exception
                );
            }
        }
    }
}