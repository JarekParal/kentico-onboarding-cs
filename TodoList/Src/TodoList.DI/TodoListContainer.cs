using System;
using System.Collections.Generic;
using TodoList.Contracts;
using Unity;

namespace TodoList.DI
{
    public class TodoListContainer : ITodoListContainer
    {
        private readonly IUnityContainer _container = new UnityContainer();

        public ITodoListContainer RegisterType<TTypeFrom, TTypeTo>()
            where TTypeTo : TTypeFrom
        {
            _container.RegisterType<TTypeFrom, TTypeTo>();
            return this;
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            return _container.ResolveAll(type);
        }

        public ITodoListContainer CreateChildContainer()
        {
            return new TodoListContainer();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}