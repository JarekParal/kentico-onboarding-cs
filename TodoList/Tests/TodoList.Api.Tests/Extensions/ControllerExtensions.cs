using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace TodoList.Api.Tests.Extensions
{
    public static class ControllerExtensions
    {
        public static async Task<HttpResponseMessage> ExecuteAction<T>(
            this T controller,
            Func<T, Task<IHttpActionResult>> action)
        {
            var actionResult = await action(controller);

            return actionResult.ExecuteAsync(CancellationToken.None).Result;
        }
    }
}