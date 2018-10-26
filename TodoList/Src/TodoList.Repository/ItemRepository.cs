using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Contracts.Models;
using TodoList.Contracts.Repository;

namespace TodoList.Repository
{
    internal class ItemRepository : IItemRepository
    {
        private static readonly Item[] s_items =
        {
            new Item {Id = new Guid("1BBA61A3-9DA6-4A28-8A12-F543BB5EA737"), Text = "Dog"},
            new Item {Id = new Guid("BFA20109-F15E-4F5C-B395-2879E02BC422"), Text = "Cat"},
            new Item {Id = new Guid("4BAF698C-AF41-4AA1-8465-85C00073BD13"), Text = "Elephant"}
        };

        public async Task<IEnumerable<Item>> GetAllAsync() 
            => await Task.FromResult(s_items);

        public async Task<Item> GetAsync(Guid id) 
            => await Task.FromResult(s_items[0]);

        public async Task<Item> AddAsync(Item item) 
            => await Task.FromResult(s_items[0]);

        public async Task<Item> EditAsync(Item item)
            => await Task.FromResult(s_items[0]);

        public async Task DeleteAsync(Guid id) 
            => await Task.CompletedTask;
    }
}