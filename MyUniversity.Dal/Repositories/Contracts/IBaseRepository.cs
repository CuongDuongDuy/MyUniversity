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

        IQueryable<TEntity> GetAll(IEnumerable<string> includes);

        IQueryable<TEntity> GetItems(Expression<Func<TEntity, bool>> predicate, IEnumerable<string> includes);

        void Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(TPrimaryKey id);
    }
}