namespace Panel.UnitOfWork;

public interface IUnitOfWork
{
    Task CommitAsync();
    
    void Commit();
}