using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;
using NHibernate;
using NHibernate.Linq;

namespace MyUniversity.Dal.Repositories.NHibernate
{
    public class BaseRepository<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey> where TEntity : EntityBase
    {
        private readonly ISession session;

        public BaseRepository(ISession session)
        {
            this.session = session;
        }

        public TEntity GetById(TPrimaryKey id)
        {
            return session.Get<TEntity>(id);
        }


        public IQueryable<TEntity> GetItems(Expression<Func<TEntity, bool>> predicate, IEnumerable<string> includes)
        {
            var result = session.Query<TEntity>().Where(predicate);
            if (includes != null)
            {
                result = includes.Aggregate(result, (current, include) => current.Include(include));
            }
            return result;
        }

        public IQueryable<TEntity> GetAll(IEnumerable<string> includes)
        {
            var result = session.Query<TEntity>();
            if (includes != null)
            {
                result = includes.Aggregate(result, (current, include) => current.Include(include));
            }
            return result;
        }

        public void Insert(TEntity entity)
        {
            session.Save(entity);
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                return;
            }
            foreach (var entity in entities)
            {
                session.Save(entity);
            }
        }

        public void Update(TEntity entity)
        {
            session.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            session.Delete(entity);
        }

        public void Delete(TPrimaryKey id)
        {
            var entity = session.Load<TEntity>(id);
            session.Delete(entity);
        }
    }
}