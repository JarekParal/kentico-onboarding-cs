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

            var todoListContainer = configDependencyResolver.Container;
            new RepositoryBootstrapper().Register(todoListContainer);

            config.DependencyResolver = configDependencyResolver;
        }
    }
}