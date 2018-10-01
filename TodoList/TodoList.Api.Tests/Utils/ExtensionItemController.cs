using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TodoList.Api.Controllers;

namespace TodoList.Api.Tests.Utils
{
    public static class ExtensionItemController
    {
        public static async Task<HttpResponseMessage> ExecuteAction(
            this ItemsController controller,
            Func<ItemsController, Task<IHttpActionResult>> action)
        {
            var actionResult = await action(controller);

            return actionResult.ExecuteAsync(CancellationToken.None).Result;
        }
    }
}