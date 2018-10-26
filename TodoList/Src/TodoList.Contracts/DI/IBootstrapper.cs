namespace TodoList.Contracts.DI
{
    public interface IBootstrapper
    {
        ITodoListContainer Register(ITodoListContainer container);
    }
}
