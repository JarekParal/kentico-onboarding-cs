using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoList.Api.Models;

namespace TodoList.Api.Controllers
{
    [RoutePrefix("api/v1/Items")]
    public class ItemsController : ApiController
    {
        private static readonly Item[] s_items = {
            new Item {Id = 1, Text = "Dog"},
            new Item {Id = 2, Text = "Cat"},
            new Item {Id = 3, Text = "Elephant"}
        };

        [Route("")]
        public async Task<IHttpActionResult> GetAsync()
        {
            return Ok(await Task.FromResult(s_items));
        }

        [Route("{id}", Name = "GetItem")]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            return Ok(await Task.FromResult(s_items[0]));
        }

        [Route("")]
        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
        {
            return Ok(await Task.FromResult(new Item {Id = item.Id, Text = item.Text}));
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> PutAsync(int id, [FromBody] Item item)
        {
            return StatusCode(await Task.FromResult(HttpStatusCode.OK));
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            return StatusCode(await Task.FromResult(HttpStatusCode.NoContent));
        }
    }
}
