using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace TodoList.Api.Tests.Extensions
{
    public static class ControllerExtensions
    {
        public static async Task<HttpResponseMessage> ExecuteAction<TController>(
            this TController controller,
            Func<TController, Task<IHttpActionResult>> action) where TController : ApiController
        {
            var actionResult = await action(controller);

            return actionResult.ExecuteAsync(CancellationToken.None).Result;
        }
    }
}