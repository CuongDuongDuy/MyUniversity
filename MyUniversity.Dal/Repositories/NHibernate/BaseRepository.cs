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
    public class RepositoryBase<TEntity, TPrimaryKey> : IBaseNhRepository<TEntity, TPrimaryKey> where TEntity : EntityBase
    {
        private readonly ISession session;

        public RepositoryBase(ISession session)
        {
            this.session = session;
        }

        public TEntity GetById(TPrimaryKey id)
        {
            return session.Get<TEntity>(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return session.Query<TEntity>();
        }

        public IQueryable<TEntity> GetItems(Expression<Func<TEntity, bool>> predicate)
        {
            return Find(predicate);
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
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate == null ? session.Query<TEntity>() : session.Query<TEntity>().Where(predicate);
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

        public IQueryable<TEntity> Entity()
        {
            return session.Query<TEntity>();
        }

        public IEnumerable<TEntity> Factory()
        {
            throw new NotImplementedException();
        }

        public ISession Session()
        {
            return session;
        }
    }
}