using System;
using System.Collections.Generic;

namespace TodoList.Contracts.DI
{
    public interface ITodoListContainer : IDisposable
    {
        ITodoListContainer RegisterType<TContract, TImplementation>(Lifetime lifetime)
            where TImplementation : TContract;

        ITodoListContainer RegisterType<TContract>(Lifetime lifetime, Func<object> factoryFunc);

        object Resolve(Type type);

        IEnumerable<object> ResolveAll(Type type);

        ITodoListContainer CreateChildContainer();
    }
}