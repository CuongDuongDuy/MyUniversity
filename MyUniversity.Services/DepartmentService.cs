using System;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Services
{
    public class DepartmentService : BaseService<DepartmentModel, Department, Guid, IDepartmentRepository>,
        IDepartmentService
    {

    }
}