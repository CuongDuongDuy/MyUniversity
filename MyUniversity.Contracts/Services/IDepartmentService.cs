using System;
using System.Collections.Generic;
using MyUniversity.Contracts.Models;

namespace MyUniversity.Contracts.Services
{
    public interface IDepartmentService : IBaseService<DepartmentModel, Guid>
    {
        IEnumerable<DepartmentModel> GetAllDepartments(IEnumerable<string> includes = null);
    }
}