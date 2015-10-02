using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyUniversity.Contracts.Constants;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Services
{
    public class StudentProfileService : BaseService<StudentProfileModel, AccountProfile, Guid, IStudentProfileRepository>, IStudentProfileService
    {
        public StudentProfileService(IStudentProfileRepository studentProfileRepository, IUnitOfWork unitOfWork)
            : base(studentProfileRepository, unitOfWork)
        {
        }
        public IEnumerable<StudentProfileModel> GetItems(Expression<Func<StudentProfileModel, bool>> predicate = null)
        {
            var result = TranferToModels(Repository.GetItems(TranferToEntityExpFunc(predicate)));
            return result;
        }

        public IEnumerable<StudentProfileModel> GetAll()
        {
            var result = TranferToModels(Repository.GetAll());
            return result;
        }

        public Guid Create(StudentProfileModel model)
        {
            model.CreatedBy = EntityConstant.CreatedBy;
            model.CreatedOn = DateTime.Now;
            var entity = TranferToEntity(model);
            Repository.Insert(entity);
            UnitOfWork.Commit();
            return entity.Guid;
        }

        public virtual StudentProfileModel GetItem(Guid id)
        {
            var result = TranferToModel(Repository.GetById(id));
            return result;
        }

        public void Update(StudentProfileModel model)
        {
            var entity = Repository.GetById(model.Id);
            entity.AccountLogin = model.AccountLogin;
            entity.AccountPassword = model.AccountPassword;
            entity.Deactive = model.Deactive;
            entity.UpdatedBy = EntityConstant.UpdatedBy;
            entity.UpdatedOn = DateTime.Now;
            Repository.Update(entity);
            UnitOfWork.Commit();
        }

        public void Delete(Guid id)
        {
            var entity = Repository.GetById(id);
            entity.UpdatedBy = EntityConstant.UpdatedBy;
            entity.UpdatedOn = DateTime.Now;
            Repository.Delete(entity);
            UnitOfWork.Commit();
        }
    }
}
