using System;
using System.Collections.Generic;

namespace TodoList.Contracts.DI
{
    public interface ITodoListContainer : IDisposable
    {
        ITodoListContainer RegisterType<TTypeFrom, TTypeTo>(ContainerLifetimeEnum lifetimeEnum)
            where TTypeTo : TTypeFrom;

        ITodoListContainer RegisterType<TTypeTo>(ContainerLifetimeEnum lifetimeEnum, Func<object> factoryFunc);

        object Resolve(Type type);

        IEnumerable<object> ResolveAll(Type type);

        ITodoListContainer CreateChildContainer();
    }
}