using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoList.Api.Models;

namespace TodoList.Api.Controllers
{
    public class ItemsController : ApiController
    {
        Item[] items = new Item[]
        {
            new Item {id = 1, name = "Dog"},
            new Item {id = 2, name = "Cat"},
            new Item {id = 3, name = "Elephant"},
        };

        // GET: api/Items
        public IEnumerable<Item> Get()
        {
            return items;
        }

        // GET: api/Items/5
        public Item Get(int id)
        {
            return items[id];
        }

        // POST: api/Items
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Items/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Items/5
        public void Delete(int id)
        {
        }
    }
}
