using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using MyUniversity.Contracts.Constants;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Services
{
    public class DepartmentService : BaseService<DepartmentModel, Department, Guid, IBaseRepository<Department, Guid>>,
        IDepartmentService
    {

        public DepartmentService(IBaseRepository<Department, Guid> repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public IEnumerable<DepartmentModel> GetAllDepartments(IEnumerable<string> includes)
        {
            var departments = Repository.GetAll(includes);
            var result = TranferToModels(departments).ToList();
            return result;
        }

        public Guid Create(DepartmentModel departmentModel)
        {
            var department = TranferToEntity(departmentModel);
            Repository.Insert(department);
            UnitOfWork.Commit();
            var result = department.Id;
            return result;
        }

        public DepartmentModel GetById(Guid id, IEnumerable<string> includes = null)
        {
            var department = includes == null ? Repository.GetById(id) : Repository.GetItems(x => x.Id == id, includes).FirstOrDefault();
            var result = TranferToModel(department);
            return result;
        }

        public ModificationServiceResult Update(Guid id, DepartmentModel departmentModel)
        {
            var result = new ModificationServiceResult(id);
            try
            {
                var departmentToUpdate = Repository.GetById(id);
                departmentToUpdate.Name = departmentModel.Name;
                departmentToUpdate.StartDate = departmentModel.StartDate;
                departmentToUpdate.DeanId = departmentModel.DeanId;
                departmentToUpdate.RowVersion = departmentModel.RowVersion;
                Repository.Update(departmentToUpdate);
                UnitOfWork.Commit();
                result.Value = id;
            }
            catch (DbUpdateConcurrencyException)
            {
                result.Type = ResultType.DbUpdateConcurrencyException;
            }
            catch (DataException)
            {
                result.Type = ResultType.DataException;
            }
            return result;
        }

        public ModificationServiceResult Deactivate(Guid id, byte[] rowVersion)
        {
            var result = new ModificationServiceResult(id);
            try
            {
                var departmentToDeactivate = Repository.GetById(id);
                departmentToDeactivate.Deactive = true;
                departmentToDeactivate.RowVersion = rowVersion;
                Repository.Update(departmentToDeactivate);
                UnitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                result.Type = ResultType.DbUpdateConcurrencyException;
            }
            catch (DataException)
            {
                result.Type = ResultType.DataException;
            }
            return result;
        }

        public ModificationServiceResult Activate(Guid id, byte[] rowVersion)
        {
            var result = new ModificationServiceResult(id);
            try
            {
                var departmentToDeactivate = Repository.GetById(id);
                departmentToDeactivate.Deactive = false;
                departmentToDeactivate.RowVersion = rowVersion;
                Repository.Update(departmentToDeactivate);
                UnitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                result.Type = ResultType.DbUpdateConcurrencyException;
            }
            catch (DataException)
            {
                result.Type = ResultType.DataException;
            }
            return result;
        }
    }
}