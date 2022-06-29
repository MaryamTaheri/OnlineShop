namespace ONLINE_SHOP.Domain.Framework.Repositories;

public interface IUnitOfWork : IDisposable
{
    void Commit();

    void Rollback();
}