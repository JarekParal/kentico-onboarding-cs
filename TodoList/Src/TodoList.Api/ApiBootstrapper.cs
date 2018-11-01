using TodoList.Api.Routes;
using TodoList.Contracts.DI;
using TodoList.Contracts.Routes;

namespace TodoList.Api
{
    public class ApiBootstrapper
    {
        public ITodoListContainer Register(ITodoListContainer container)
            => container
                .RegisterType<IRouteNames, RouteNames>(ContainerLifetimeEnum.HierarchicalLifetimeManager);
    }
}