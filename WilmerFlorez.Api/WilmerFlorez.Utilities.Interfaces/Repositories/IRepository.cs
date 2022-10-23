using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WilmerFlorez.Utilities.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Find(params object[] keyValues);
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Find<TKey>(Expression<Func<TEntity, TKey>> sortExpression, bool isDesc, Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);

        IQueryable<TEntity> GetAll();

        DbSet<TEntity> Get();
        IQueryable<TEntity> Queryable();
        IRepository<T> GetRepository<T>() where T : class;

    }
}
