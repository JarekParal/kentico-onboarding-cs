namespace TodoList.Contracts
{
    public interface IBootstrapper
    {
        ITodoListContainer Register(ITodoListContainer container);
    }
}