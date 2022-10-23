using System.Threading;
using System.Threading.Tasks;
using WilmerFlorez.Utilities.Interfaces.Repositories;

namespace WilmerFlorez.Utilities.Interfaces.UnitOfWorks
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class;
    }
}
