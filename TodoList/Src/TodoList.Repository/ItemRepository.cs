using System;
using System.Collections.Generic;
using TodoList.Contracts.Models;

namespace TodoList.Repository
{
    public class ItemRepository : IItemRepository
    {
        private static readonly Item[] s_items =
        {
            new Item {Id = new Guid("1BBA61A3-9DA6-4A28-8A12-F543BB5EA737"), Text = "Dog"},
            new Item {Id = new Guid("BFA20109-F15E-4F5C-B395-2879E02BC422"), Text = "Cat"},
            new Item {Id = new Guid("4BAF698C-AF41-4AA1-8465-85C00073BD13"), Text = "Elephant"}
        };

        public IEnumerable<Item> GetAll()
        {
            return s_items;
        }

        public Item Get(Guid id)
        {
            return s_items[0];
        }

        public void Add(Item item) { }

        public void Edit(Item item) { }

        public void Delete(Guid id) { }
    }
}
