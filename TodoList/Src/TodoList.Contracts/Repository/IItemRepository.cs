using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Contracts.Models;

namespace TodoList.Contracts.Repository
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> GetItemAsync(Guid id);
        Task<Item> AddItemAsync(Item item);
        Task<Item> EditItemAsync(Item item);
        Task DeleteItemAsync(Guid id);
    }
}