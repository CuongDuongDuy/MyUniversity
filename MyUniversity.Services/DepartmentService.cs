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

        public DepartmentModel GetById(Guid id)
        {
            var department = Repository.GetById(id);
            var result = TranferToModel(department);
            return result;
        }
    }
}