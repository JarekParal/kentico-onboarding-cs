using System;
using System.Collections.Generic;

namespace TodoList.Contracts
{
    public interface ITodoListContainer : IDisposable
    {
        ITodoListContainer RegisterType<TTypeFrom, TTypeTo>() 
            where TTypeTo : TTypeFrom;

        object Resolve(Type type);

        IEnumerable<object> ResolveAll(Type type);

        ITodoListContainer CreateChildContainer();
    }
}