using System;
using System.Web.Http.Routing;
using TodoList.Contracts.Api.Services;
using TodoList.Contracts.Routes;

namespace TodoList.Api.Services.UrlHelpers
{
    internal class ItemUrlHelper : ITodoListUrlHelper
    {
        private readonly UrlHelper _urlHelper;
        private readonly IRouteNames _routeNames;

        public ItemUrlHelper(UrlHelper urlHelper, IRouteNames routeNames)
        {
            _urlHelper = urlHelper;
            _routeNames = routeNames;
        }

        public Uri Link(Guid id)
            => new Uri(_urlHelper.Link(_routeNames.GetItem, new {id}));
    }
}