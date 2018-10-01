using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoList.Api.Models;

namespace TodoList.Api.Controllers
{
    [RoutePrefix("api/v1/Items")]
    [Route("")]
    public class ItemsController : ApiController
    {
        private static readonly Item[] s_items =
        {
            new Item {Id = new Guid("1BBA61A3-9DA6-4A28-8A12-F543BB5EA737"), Text = "Dog"},
            new Item {Id = new Guid("BFA20109-F15E-4F5C-B395-2879E02BC422"), Text = "Cat"},
            new Item {Id = new Guid("4BAF698C-AF41-4AA1-8465-85C00073BD13"), Text = "Elephant"}
        };

        public async Task<IHttpActionResult> GetAsync()
            => Ok(await Task.FromResult(s_items));

        [Route("{id}", Name = "GetItem")]
        public async Task<IHttpActionResult> GetAsync(Guid id)
            => Ok(await Task.FromResult(s_items[0]));

        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
            => Ok(await Task.FromResult(new Item {Id = item.Id, Text = item.Text}));

        [Route("{id}")]
        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] Item item)
            => Created("http://localhost/api/v1/items/1", item);

        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(Guid id)
            => StatusCode(await Task.FromResult(HttpStatusCode.NoContent));
    }
}