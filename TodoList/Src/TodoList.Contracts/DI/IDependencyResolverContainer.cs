namespace TodoList.Contracts
{
    public interface IDependencyResolverContainer
    {
        ITodoListContainer Container { get; }
    }
}