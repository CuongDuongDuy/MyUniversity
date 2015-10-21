using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;

namespace MyUniversity.Dal.Repositories.Contracts
{
    public interface IBaseRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        TEntity GetById(TPrimaryKey id);
        
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetItems(Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(TPrimaryKey id);

        IQueryable<TEntity> ABC();
    }

    public interface IBaseEfRepository<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        IDbSet<TEntity> DbSet();
    }

    public interface IBaseNhRepository<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        ISession Session();
    }

    public interface IMyData
    {
        
    }
}