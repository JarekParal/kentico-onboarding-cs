using System.Collections.Generic;
using TodoList.Contracts.DI;
using TodoList.DI.Containers;

namespace TodoList.DI.DependencyResolvers
{
    public class DependencyResolverBuilder
    {
        private readonly List<IBootstrapper> _bootstrappers = new List<IBootstrapper>();

        public DependencyResolverBuilder Bootstrap(IBootstrapper bootstrapper)
        {
            _bootstrappers.Add(bootstrapper);
            return this;
        }

        public ITodoListDependencyResolver Build()
        {
            ITodoListContainer container = new TodoListContainer();

            foreach (var bootstrapper in _bootstrappers)
            {
                bootstrapper.Register(container);
            }

            return new DependencyResolver(container);
        }
    }
}