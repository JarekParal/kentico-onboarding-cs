using System.Web.Http;

namespace TodoList.Api
{
    public static class RoutesConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}