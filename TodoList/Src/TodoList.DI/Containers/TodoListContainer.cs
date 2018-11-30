using System;
using System.Collections.Generic;
using TodoList.Contracts.DI;
using Unity;
using Unity.Exceptions;
using Unity.Injection;
using Unity.Lifetime;

namespace TodoList.DI.Containers
{
    internal class TodoListContainer : ITodoListContainer
    {
        private bool _disposed;
        internal readonly IUnityContainer Container;

        public TodoListContainer()
            => Container = new UnityContainer();

        internal TodoListContainer(IUnityContainer container)
            => Container = container;

        public ITodoListContainer RegisterType<TTypeFrom, TTypeTo>(Lifetime lifetime)
            where TTypeTo : TTypeFrom
        {
            var lifetimeManager = GetLifetimeManager(lifetime);
            Container.RegisterType<TTypeFrom, TTypeTo>(lifetimeManager);

            return this;
        }

        public ITodoListContainer RegisterType<TTypeTo>(Lifetime lifetime,
            Func<object> factoryFunc)
        {
            var lifetimeManager = GetLifetimeManager(lifetime);
            Container.RegisterType<TTypeTo>(lifetimeManager, new InjectionFactory(_ => factoryFunc()));

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
                    "Problem with the resolving types.",
                    exception
                );
            }
        }

        private static LifetimeManager GetLifetimeManager(Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.PerApplication:
                    return new ContainerControlledLifetimeManager();
                case Lifetime.PerRequest:
                    return new HierarchicalLifetimeManager();
                default:
                    return new TransientLifetimeManager(); // default in Unity
            }
        }
    }
}