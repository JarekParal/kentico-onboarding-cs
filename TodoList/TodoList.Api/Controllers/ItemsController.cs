using System.Linq;
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
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(s_items);
        }

        [Route("{id}", Name = "GetItem")]
        [HttpGet]
        public IHttpActionResult GetItem(int id)
        {
            var item = FoundItem(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] Item item)
        {
            return CreatedAtRoute("GetItem", new { id = item.Id}, item);
        }

        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Item item)
        {
            if (FoundItem(id) == null)
            {
                return CreatedAtRoute("GetItem", new { id = item.Id }, item);
            }
            return StatusCode(HttpStatusCode.OK);
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (FoundItem(id) == null)
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        private static Item FoundItem(int id)
        {
            var itemFound = s_items.FirstOrDefault((p) => p.Id == id);
            return itemFound;
        }
    }
}
