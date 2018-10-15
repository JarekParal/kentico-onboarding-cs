using System;
using System.Collections.Generic;
using TodoList.Contracts.Models;

namespace TodoList.Repository
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAll();
        Item Get(Guid id);
        void Add(Item item);
        void Edit(Item item);
        void Delete(Guid id);
    }
}