using System;

namespace TodoList.Contracts.Api.Services
{
    public interface ITodoListUrlHelper
    {
        Uri GetItemLink(Guid id);
    }
}