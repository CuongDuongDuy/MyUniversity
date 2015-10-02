using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyUniversity.Dal.Repositories.Contracts
{
    public interface IBaseRepository<TEntity, TPrimaryKey>
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


    }
}