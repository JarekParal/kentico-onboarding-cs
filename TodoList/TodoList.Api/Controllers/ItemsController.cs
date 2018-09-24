using System.Net;
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
        public IHttpActionResult Get()
        {
            return Ok(s_items);
        }

        [Route("{id}", Name = "GetItem")]
        public IHttpActionResult Get(int id)
        {
            return Ok(s_items[0]);
        }

        [Route("")]
        public IHttpActionResult Post([FromBody] Item item)
        {
            return CreatedAtRoute("GetItem", new { id = item.Id }, item);
        }

        [Route("{id}")]
        public IHttpActionResult Put(int id, [FromBody] Item item)
        {
            return StatusCode(HttpStatusCode.OK);
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
