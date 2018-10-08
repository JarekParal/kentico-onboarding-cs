using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoList.Api.Models;

namespace TodoList.Api.Controllers
{
    [RoutePrefix(ApiVersions.V1)]
    [Route("Items")]
    public class ItemsController : ApiController
    {
        private const string _getItemRouteName = "GetItem";

        private static readonly Item[] s_items =
        {
            new Item {Id = new Guid("1BBA61A3-9DA6-4A28-8A12-F543BB5EA737"), Text = "Dog"},
            new Item {Id = new Guid("BFA20109-F15E-4F5C-B395-2879E02BC422"), Text = "Cat"},
            new Item {Id = new Guid("4BAF698C-AF41-4AA1-8465-85C00073BD13"), Text = "Elephant"}
        };

        public async Task<IHttpActionResult> GetAsync()
            => Ok(await Task.FromResult(s_items));

        [Route("{id}", Name = _getItemRouteName)]
        public async Task<IHttpActionResult> GetAsync(Guid id)
            => Ok(await Task.FromResult(s_items[0]));

        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
            => await Task.FromResult(CreatedAtRoute(_getItemRouteName, new { id = item.Id }, item));

        [Route("{id}")]
        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] Item item)
            => await Task.FromResult(Ok(s_items[0]));


        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(Guid id)
            => StatusCode(await Task.FromResult(HttpStatusCode.NoContent));
    }
}