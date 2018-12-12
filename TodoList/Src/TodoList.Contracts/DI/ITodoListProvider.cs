using System;
using System.Collections.Generic;

namespace TodoList.Contracts.DI
{
    public interface ITodoListProvider : IDisposable
    {
        object Resolve(Type type);

        IEnumerable<object> ResolveAll(Type type);

        ITodoListProvider CreateChildContainer();
    }
}