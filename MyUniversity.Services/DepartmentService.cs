using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Services
{
    public class DepartmentService : BaseService<DepartmentModel, Department, Guid, IDepartmentRepository>,
        IDepartmentService
    {

        public DepartmentService(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
            : base(departmentRepository, unitOfWork)
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

    }
}