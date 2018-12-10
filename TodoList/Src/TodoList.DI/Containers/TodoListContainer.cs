using System;
using System.Collections.Generic;
using TodoList.Contracts.DI;
using TodoList.DI.Extensions;
using Unity;
using Unity.Exceptions;
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
            Container.RegisterType<TContract>(lifetime.GetUnityLifetimeManager(), new InjectionFactory(_ => factoryFunc()));

            return this;
        }

        public object Resolve(Type type)
            => ResolveTypes(() => Container.Resolve(type));

        public IEnumerable<object> ResolveAll(Type type)
            => ResolveTypes(() => Container.ResolveAll(type));

        public ITodoListContainer CreateChildContainer()
        {
            var newChildContainer = Container.CreateChildContainer();

            return new TodoListContainer(newChildContainer);
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
    }
}