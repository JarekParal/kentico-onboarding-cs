using System;
using System.IO;
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

            var apiVersionName = GetApiVersionName(apiVersion);
            var extractedControllerName = ExtractControllerName(controllerName);

            return Combine(_apiRoot, apiVersionName, extractedControllerName);
        }

        private static string Combine(params string[] parts)
            => Path.Combine(parts).Replace("\\", "/");

        private static string GetApiVersionName(ApiVersion apiVersion)
            => Enum.GetName(typeof(ApiVersion), apiVersion);

        private static string ExtractControllerName(string controllerName)
            => controllerName.Replace("Controller", string.Empty);
    }
}