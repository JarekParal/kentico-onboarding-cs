using System;
using System.Collections.Generic;
using TodoList.Contracts.DI;
using Unity;
using Unity.Lifetime;

namespace TodoList.DI
{
    internal class TodoListContainer : ITodoListContainer
    {
        private readonly IUnityContainer _container;

        public TodoListContainer()
        {
            _container = new UnityContainer();
        }

        private TodoListContainer(IUnityContainer container)
        {
            _container = container;
        }

        public ITodoListContainer RegisterType<TTypeFrom, TTypeTo>(ContainerLifetimeEnum lifetimeEnum)
            where TTypeTo : TTypeFrom
        {
            var lifetimeManager = GetLifetimeManager(lifetimeEnum);
            _container.RegisterType<TTypeFrom, TTypeTo>(lifetimeManager);
            return this;
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            return _container.ResolveAll(type);
        }

        public ITodoListContainer CreateChildContainer()
        {
            var newChildContainer = _container.CreateChildContainer();
            return new TodoListContainer(newChildContainer);
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        private static LifetimeManager GetLifetimeManager(ContainerLifetimeEnum lifetimeEnum)
        {
            switch (lifetimeEnum)
            {
                case ContainerLifetimeEnum.TransientLifetimeManager:
                    return new TransientLifetimeManager();
                case ContainerLifetimeEnum.ContainerControlledLifetimeManager:
                    return new ContainerControlledLifetimeManager();
                case ContainerLifetimeEnum.HierarchicalLifetimeManager:
                    return new HierarchicalLifetimeManager();
                case ContainerLifetimeEnum.PerResolveLifetimeManager:
                    return new PerResolveLifetimeManager();
                case ContainerLifetimeEnum.PerThreadLifetimeManager:
                    return new PerThreadLifetimeManager();
                case ContainerLifetimeEnum.ExternallyControlledLifetimeManager:
                    return new ExternallyControlledLifetimeManager();
                default:
                    return new TransientLifetimeManager();
            }
        }
    }
}