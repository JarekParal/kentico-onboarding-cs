using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Contracts.Models;

namespace TodoList.Contracts.Repository
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();
        Task<Item> GetAsync(Guid id);
        Task<Item> AddAsync(Item item);
        Task<Item> EditAsync(Item item);
        Task DeleteAsync(Guid id);
    }
}