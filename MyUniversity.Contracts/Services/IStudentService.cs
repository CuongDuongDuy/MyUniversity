using System;
using System.Collections.Generic;
using MyUniversity.Contracts.Models;

namespace MyUniversity.Contracts.Services
{
    public interface IStudentService : IBaseService<StudentModel, Guid>
    {
        IEnumerable<StudentModel> GetStudents(IEnumerable<string> includes);
    }
}