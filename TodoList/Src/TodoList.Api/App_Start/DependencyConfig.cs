using System.Web.Http;
using TodoList.Contracts.Unity;
using Unity;
using TodoList.Repository;

namespace TodoList.Api
{
    public class DependencyConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IItemRepository, ItemRepository>();
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}