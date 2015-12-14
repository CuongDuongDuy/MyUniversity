using System;
using System.Collections.Generic;
using MyUniversity.Contracts.Models;

namespace MyUniversity.Contracts.Services
{
    public interface ITeacherService : IBaseService<TeacherModel, Guid>
    {
        IEnumerable<TeacherModel> GetAllTeachers(IEnumerable<string> includes = null);
        Guid Create(TeacherModel teacherModel);
    }
}