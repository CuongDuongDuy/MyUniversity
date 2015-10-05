using System;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Repositories.Contracts
{
    public interface IDepartmentRepository : IBaseRepository<Department, Guid>
    {

    }
}