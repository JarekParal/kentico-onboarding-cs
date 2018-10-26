using System.Web.Http;
using TodoList.Contracts;
using TodoList.DI;
using Unity;
using TodoList.Repository;
using TodoList.DI.Unity;

namespace TodoList.Api
{
    public class DependencyConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new TodoListContainer();
            container.RegisterType<IItemRepository, ItemRepository>();
            config.DependencyResolver = new DependencyResolver(container);
        }
    }
}