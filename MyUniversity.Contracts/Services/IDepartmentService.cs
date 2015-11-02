using System;
using MyUniversity.Contracts.Models;

namespace MyUniversity.Contracts.Services
{
    public interface IDepartmentService : IBaseService<DepartmentModel, Guid>
    {
         
    }
}