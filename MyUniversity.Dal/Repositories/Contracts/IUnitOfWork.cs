namespace MyUniversity.Dal.Repositories.Contracts
{
    //ToDo: Do not inherit from IDisposable
    public interface IUnitOfWork
    {
        int Commit();
    }
}