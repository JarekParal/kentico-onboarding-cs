using TodoList.Contracts.Routes;

namespace TodoList.Api.Routes
{
    public class RouteNames : IRouteNames
    {
        public const string GetItemName = "GetItem";
        public string GetItem => GetItemName;
    }
}