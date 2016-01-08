using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Services
{
    public abstract class BaseService<TModel, TEntity, TPrimaryKey, TRepository> where TRepository : IBaseRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        protected TRepository Repository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        protected BaseService(TRepository repository, IUnitOfWork unitOfWork)
        {
            Repository = repository;
            UnitOfWork = unitOfWork;
        }
        protected BaseService()
        {
        }
        
        public static TModel TranferToModel(TEntity entity)
        {
            var result = Mapper.Map<TModel>(entity);
            return result;
        }
        public static IEnumerable<TModel> TranferToModels(IEnumerable<TEntity> entities)
        {
            var result = entities.Select(Mapper.Map<TModel>);
            return result;
        }
        public static Expression<Func<TEntity, bool>> TranferToEntityExpFunc(Expression<Func<TModel, bool>> predicate = null)
        {
            var mappedExp = Mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            return mappedExp;
        }
        public static TEntity TranferToEntity(TModel model)
        {
            var result = Mapper.Map<TEntity>(model);
            return result;
        }
    }
}