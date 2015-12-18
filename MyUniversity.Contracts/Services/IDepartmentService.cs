using System;
using System.Collections.Generic;
using MyUniversity.Contracts.Models;

namespace MyUniversity.Contracts.Services
{
    public interface IDepartmentService : IBaseService<DepartmentModel, Guid>
    {
        IEnumerable<DepartmentModel> GetAllDepartments(IEnumerable<string> includes = null);
        Guid Create(DepartmentModel departmentModel);
        DepartmentModel GetById(Guid id, IEnumerable<string> includes = null);
        ModificationServiceResult Update(Guid id, DepartmentModel departmentModel);
        ModificationServiceResult Deactivate(Guid id, byte[] rowVersion);
        ModificationServiceResult Activate(Guid id, byte[] rowVersion);
    }
}