using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MyUniversity.Contracts.Constants;
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
            var entityType = entity.GetType();
            var createdByProp = entityType.GetProperty("CreatedBy");
            if (createdByProp != null)
            {
                createdByProp.SetValue(entity, EntityConstant.CreatedBy, null);
            }
            var createdOnProp = entityType.GetProperty("CreatedOn");
            if (createdOnProp != null)
            {
                createdOnProp.SetValue(entity, DateTime.UtcNow, null);
            }
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
                var entityType = entity.GetType();
                var createdByProp = entityType.GetProperty("CreatedBy", entityType);
                if (createdByProp != null)
                {
                    createdByProp.SetValue(entity, EntityConstant.CreatedBy, null);
                }
                var createdOnProp = entityType.GetProperty("CreatedOn", entityType);
                if (createdOnProp != null)
                {
                    createdOnProp.SetValue(entity, DateTime.UtcNow, null);
                }
                session.Save(entity);
            }
        }

        public void Update(TEntity entity)
        {
            var entityType = entity.GetType();
            var createdByProp = entityType.GetProperty("UpdatedBy", entityType);
            if (createdByProp != null)
            {
                createdByProp.SetValue(entity, EntityConstant.UpdatedBy, null);
            }
            var createdOnProp = entityType.GetProperty("UpdatedOn", entityType);
            if (createdOnProp != null)
            {
                createdOnProp.SetValue(entity, DateTime.UtcNow, null);
            }
            session.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            var entityType = entity.GetType();
            var createdByProp = entityType.GetProperty("UpdatedBy", entityType);
            if (createdByProp != null)
            {
                createdByProp.SetValue(entity, EntityConstant.UpdatedBy, null);
            }
            var createdOnProp = entityType.GetProperty("UpdatedOn", entityType);
            if (createdOnProp != null)
            {
                createdOnProp.SetValue(entity, DateTime.UtcNow, null);
            }
            session.Delete(entity);
        }

        public void Delete(TPrimaryKey id)
        {
            var entity = session.Load<TEntity>(id);
            var entityType = entity.GetType();
            var createdByProp = entityType.GetProperty("UpdatedBy", entityType);
            if (createdByProp != null)
            {
                createdByProp.SetValue(entity, EntityConstant.UpdatedBy, null);
            }
            var createdOnProp = entityType.GetProperty("UpdatedOn", entityType);
            if (createdOnProp != null)
            {
                createdOnProp.SetValue(entity, DateTime.UtcNow, null);
            }
            session.Delete(entity);
        }
    }
}