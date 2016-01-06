using System;
using System.Collections.Generic;
using System.Linq;
using MyUniversity.Contracts.Models;

namespace MyUniversity.Contracts.Services
{
    public interface ITeacherService : IBaseService<TeacherModel, Guid>
    {
        IEnumerable<TeacherModel> GetTeachers(IEnumerable<string> includes);
        TeacherModel GetById(Guid id, IEnumerable<string> includes = null);
        IQueryable<TeacherModel> GetAsQueryable();
        Guid Create(TeacherModel studentModel);
        ModificationServiceResult Update(Guid id, TeacherModel teacherModel);
    }
}