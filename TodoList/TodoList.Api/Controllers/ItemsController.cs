using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TodoList.Api.Models;

namespace TodoList.Api.Controllers
{
    public class ItemsController : ApiController
    {
        Item[] items = new Item[]
        {
            new Item {Id = 1, Text = "Dog"},
            new Item {Id = 2, Text = "Cat"},
            new Item {Id = 3, Text = "Elephant"},
        };

        [Route("api/v1/Items")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(items);
        }

        [Route("api/v1/Items/{id}", Name = "GetItem")]
        [HttpGet]
        public IHttpActionResult GetItem(int id)
        {
            var item = items.FirstOrDefault((p) => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [Route("api/v1/Items")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] Item item)
        {
            return CreatedAtRoute("GetItem", new { id = item.Id}, item);
        }

        [Route("api/v1/Items")]
        [HttpPut]
        public IHttpActionResult Put(int id, Item item)
        {
            if (FoundItem(id) == null)
            {
                return StatusCode(HttpStatusCode.Created);
            }
            return StatusCode(HttpStatusCode.OK);
        }

        [Route("api/v1/Items")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var item = items.FirstOrDefault((p) => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        private Item FoundItem(int id)
        {
            var itemFound = items.FirstOrDefault((p) => p.Id == id);
            return itemFound;
        }

    }
}
