using System;
using System.Collections.Generic;
using TodoList.Contracts.DI;
using Unity;
using Unity.Lifetime;

namespace TodoList.DI
{
    internal class TodoListContainer : ITodoListContainer
    {
        internal readonly IUnityContainer Container;

        public TodoListContainer()
            => Container = new UnityContainer();

        private TodoListContainer(IUnityContainer container)
            => Container = container;

        public ITodoListContainer RegisterType<TTypeFrom, TTypeTo>(ContainerLifetimeEnum lifetimeEnum)
            where TTypeTo : TTypeFrom
        {
            var lifetimeManager = GetLifetimeManager(lifetimeEnum);
            Container.RegisterType<TTypeFrom, TTypeTo>(lifetimeManager);
            return this;
        }

        public object Resolve(Type type)
            => Container.Resolve(type);

        public IEnumerable<object> ResolveAll(Type type)
            => Container.ResolveAll(type);

        public ITodoListContainer CreateChildContainer()
        {
            var newChildContainer = Container.CreateChildContainer();
            return new TodoListContainer(newChildContainer);
        }

        public void Dispose() => Container.Dispose();

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
                    // TODO: is it OK this default? This is the same behavior as has without parametric constructor of the Unity Container.
                    return new TransientLifetimeManager(); 
            }
        }
    }
}