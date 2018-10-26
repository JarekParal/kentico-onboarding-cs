using System;
using System.Collections.Generic;
using TodoList.Contracts;
using Unity;
using Unity.Exceptions;

namespace TodoList.DI
{
    public class TodoListContainer : ITodoListContainer
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
            try
            {
                return _container.Resolve(type);
            }
            catch (global::Unity.Exceptions.ResolutionFailedException)
            {
                throw new ResolutionFailedException();
            }
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            try
            {
                return _container.ResolveAll(type);
            }
            catch (global::Unity.Exceptions.ResolutionFailedException)
            {
                throw new ResolutionFailedException();
            }
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

        public class ResolutionFailedException : Exception
        {
            public ResolutionFailedException()
            {
            }

            public ResolutionFailedException(string message) : base(message)
            {
            }

            public ResolutionFailedException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }
    }
}