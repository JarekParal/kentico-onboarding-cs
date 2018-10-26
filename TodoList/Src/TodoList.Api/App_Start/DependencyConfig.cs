using System.Web.Http;
using TodoList.DI;
using TodoList.Repository;

namespace TodoList.Api
{
    public class DependencyConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var configDependencyResolver = new DependencyResolver();
            configDependencyResolver.Container.RegisterType<IItemRepository, ItemRepository>();
            config.DependencyResolver = configDependencyResolver;
        }
    }
}