using TodoList.Contracts.DI;
using TodoList.Contracts.Repository;

namespace TodoList.Repository
{
    public class RepositoryBootstrapper : IBootstrapper
    {
        public ITodoListContainer Register(ITodoListContainer container)
            => container.RegisterType<IItemRepository, ItemRepository>();
    }
}