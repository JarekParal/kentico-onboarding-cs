using System;
using System.Collections.Generic;
using TodoList.Contracts;
using Unity;

namespace TodoList.DI
{
    internal class TodoListContainer : ITodoListContainer
    {
        private readonly IUnityContainer _container;

        public TodoListContainer()
        {
            _container = new UnityContainer();
        }

        private TodoListContainer(IUnityContainer container)
        {
            _container = container;
        }

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
            var newChildContainer = _container.CreateChildContainer();
            return new TodoListContainer(newChildContainer);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}