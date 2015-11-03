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
            var departments = Repository.Entity();
            if (includes != null)
            {
                departments = includes.Aggregate(departments, (current, include) => current.Include(include));
            }
            var result = TranferToModels(departments).ToList();
            return result;
        }
    }
}