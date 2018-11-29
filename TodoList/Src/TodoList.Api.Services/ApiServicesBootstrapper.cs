using System.Net.Http;
using System.Web;
using TodoList.Api.Services.UrlHelpers;
using TodoList.Contracts.Api.Services;
using TodoList.Contracts.DI;

namespace TodoList.Api.Services
{
    public class ApiServicesBootstrapper : IBootstrapper
    {
        public ITodoListContainer Register(ITodoListContainer container)
            => container
                .RegisterType<ITodoListUrlHelper, ItemUrlHelper>(Lifetime.PerRequest)
                .RegisterType<HttpRequestMessage>(Lifetime.PerRequest, GetMessage);

        private static HttpRequestMessage GetMessage()
            => HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
    }
}