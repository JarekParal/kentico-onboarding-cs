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
            var dependencyResolverBuilder = DependencyResolverBuilder.GetInstance();
            config.DependencyResolver = dependencyResolverBuilder
                .Bootstrap(new ApiBootstrapper())
                .Bootstrap(new RepositoryBootstrapper())
                .Bootstrap(new ApiServicesBootstrapper())
                .Build();
        }
    }
}