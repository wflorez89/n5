using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WilmerFlorez.Utilities.Interfaces.Repository;
using WilmerFlorez.Utilities.Interfaces.UnitOfWorks;

namespace WilmerFlorez.Utilities.Implementation.Repositories
{
    public class Repository<TEntity> : IRepositoryAsync<TEntity> where TEntity : class
    {
        #region Private Fields

        private readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWorkAsync _unitOfWork;
        #endregion Private Fields

        public Repository(DbContext context, IUnitOfWorkAsync unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            if (_context != null)
            {
                _dbSet = _context.Set<TEntity>();
            }
        }

        public virtual TEntity Find(params object[] keyValues) => _dbSet.Find(keyValues);

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefaultAsync(predicate);
        }

        public TEntity Find<TKey>(Expression<Func<TEntity, TKey>> sortExpression, bool isDesc, Expression<Func<TEntity, bool>> predicate) => isDesc ? _dbSet.OrderBy(sortExpression).FirstOrDefault(predicate) : _dbSet.OrderByDescending(sortExpression).FirstOrDefault(predicate);


        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            var entityDb = await _dbSet.AddAsync(entity);
            _unitOfWork.SyncObjectState(entity);
            return entityDb.Entity;
        }

        public virtual void Insert(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _dbSet.Add(entity);
            _unitOfWork.SyncObjectState(entity);
        }

       
        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _unitOfWork.SyncObjectState(entity);
            return Task.FromResult(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _unitOfWork.SyncObjectState(entity);
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
          _context.RemoveRange(_dbSet.Where(predicate));  
        }

        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {  
            _context.Entry(entity).State = EntityState.Deleted;
            _unitOfWork.SyncObjectState(entity);   
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = _dbSet;

            return query;
        }

        public virtual DbSet<TEntity> Get() => _dbSet;

        public IQueryable<TEntity> Queryable() => _dbSet;

        public IRepository<T> GetRepository<T>() where T : class => _unitOfWork.Repository<T>();

        public virtual async Task<TEntity> FindAsync(params object[] keyValues) => await _dbSet.FindAsync(keyValues);

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues) => await _dbSet.FindAsync(keyValues);

        public virtual async Task<bool> DeleteAsync(params object[] keyValues) => await DeleteAsync(CancellationToken.None, keyValues);

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues);

            if (entity == null)
            {
                return false;
            }

            _context.Entry(entity).State = EntityState.Deleted;
            //_context.Set<TEntity>().Attach(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public Task<int> Commit()
        {
            return _unitOfWork.SaveChangesAsync();
        }

        
        

    }
}
