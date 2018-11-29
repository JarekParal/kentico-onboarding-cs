using System;

namespace TodoList.Contracts.DI
{
    public interface ITodoListContainer : IDisposable
    {
        ITodoListContainer RegisterType<TContract, TImplementation>(Lifetime lifetime)
            where TImplementation : TContract;

        ITodoListContainer RegisterType<TContract>(Lifetime lifetime, Func<TContract> factoryMethod);

        ITodoListProvider GetProvider();
    }
}