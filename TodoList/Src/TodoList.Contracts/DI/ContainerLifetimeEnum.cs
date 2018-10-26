namespace TodoList.Contracts.DI
{
    public enum ContainerLifetimeEnum
    {
        TransientLifetimeManager,
        ContainerControlledLifetimeManager,
        HierarchicalLifetimeManager,
        PerResolveLifetimeManager,
        PerThreadLifetimeManager,
        ExternallyControlledLifetimeManager
    }
}