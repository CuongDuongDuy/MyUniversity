using System;
using System.Collections.Generic;
using System.Linq;
using MyUniversity.Contracts.Models;

namespace MyUniversity.Contracts.Services
{
    public interface IStudentService : IBaseService<StudentModel, Guid>
    {
        IEnumerable<StudentModel> GetStudents(IEnumerable<string> includes);
        StudentModel GetById(Guid key);
        IQueryable<StudentModel> GetAsQueryable();

    }
}