using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoList.Api.Controllers;

namespace TodoList.Api.Routes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RoutePrefixControllerAttribute : RoutePrefixAttribute
    {
        private const string _apiRoot = "api";

        public RoutePrefixControllerAttribute(ApiVersion apiVersion, string controllerName)
            : base(CreatePrefix(apiVersion, controllerName))
        {
        }

        private static string CreatePrefix(ApiVersion apiVersion, string controllerName)
        {
            if (controllerName == null)
            {
                throw new ArgumentNullException(nameof(controllerName));
            }

            return CombinePrefix(_apiRoot, apiVersion, controllerName);
        }

        private static string CombinePrefix(string apiRoot, ApiVersion apiVersion, string controllerName)
            => Path.Combine(apiRoot, ConvertApiVersion(apiVersion), ExtractControllerName(controllerName))
                .Replace("\\", "/");

        private static string ConvertApiVersion(ApiVersion apiVersion)
            => Enum.GetName(typeof(ApiVersion), apiVersion);

        private static string ExtractControllerName(string controllerName)
            => controllerName.Replace("Controller", "");
    }
}