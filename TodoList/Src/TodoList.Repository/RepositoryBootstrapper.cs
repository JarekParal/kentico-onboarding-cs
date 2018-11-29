using TodoList.Contracts.DI;
using TodoList.Contracts.Repository;
using TodoList.Repository.Repository;

namespace TodoList.Repository
{
    public class RepositoryBootstrapper : IBootstrapper
    {
        public ITodoListContainer Register(ITodoListContainer container)
            => container
                .RegisterType<IItemRepository, ItemRepository>(ContainerLifetimeEnum.HierarchicalLifetimeManager);
    }
}