using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;
using NHibernate;

namespace MyUniversity.Dal.Repositories.EntityFramework
{
    public class BaseRepository<TEntity, TPrimaryKey> : IBaseEfRepository<TEntity, TPrimaryKey> where TEntity : EntityBase
    {
        private readonly MyUniversityDbContext databaseContext;

        private readonly IDbSet<TEntity> dbSet; 

        public BaseRepository()
        {
            databaseContext = new MyUniversityDbContext();
            dbSet = databaseContext.Set<TEntity>();
        }

        public IDbSet<TEntity> DbSet()
        {
            return dbSet;
        }

        public TEntity GetById(TPrimaryKey id)
        {
            var entity = dbSet.Find(id);
            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            var entities = dbSet.AsQueryable();
            return entities;
        }

        public IQueryable<TEntity> GetItems(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = dbSet.Where(predicate);
            return entities;
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                dbSet.Add(entity);
            }
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = dbSet.Where(predicate);
            return entities;
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            databaseContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            dbSet.Attach(entity);
            databaseContext.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(TPrimaryKey id)
        {
            var entity = dbSet.Find(id);
            if (entity == null) return;
            databaseContext.Entry(entity).State = EntityState.Deleted;
        }

        public IQueryable<TEntity> ABC()
        {
            return dbSet;
        }
    }

    
}
