using System;

namespace TodoList.Contracts.Api.Services
{
    public interface ITodoListUrlHelper
    {
        Uri Link(Guid id);
    }
}