using TodoList.Contracts.DI;
using Unity.Lifetime;

namespace TodoList.DI.Extensions
{
    internal static class LifetimeExtension
    {
        internal static LifetimeManager GetUnityLifetimeManager(this Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.PerApplication:
                    return new ContainerControlledLifetimeManager();

                case Lifetime.PerRequest:
                    return new HierarchicalLifetimeManager();

                default:
                    return new TransientLifetimeManager(); // default in UnityContainer.RegisterType()
            }
        }
    }
}