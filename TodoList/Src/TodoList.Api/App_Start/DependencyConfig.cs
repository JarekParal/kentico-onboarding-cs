using System.Web.Http;
using TodoList.Api.Services;
using TodoList.DI.DependencyResolvers;
using TodoList.Repository;

namespace TodoList.Api
{
    public class DependencyConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var configDependencyResolver = new DependencyResolver();

            var todoListContainer = configDependencyResolver.Container;
            new ApiBootstrapper().Register(todoListContainer);
            new RepositoryBootstrapper().Register(todoListContainer);
            new ApiServicesBootstrapper().Register(todoListContainer);

            config.DependencyResolver = configDependencyResolver;
        }
    }
}