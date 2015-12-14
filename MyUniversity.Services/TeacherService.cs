using System;
using System.Collections.Generic;
using System.Linq;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Services
{
    public class TeacherService : BaseService<TeacherModel, InstructorProfile, Guid, IInstructorProfileRepository>,
        ITeacherService
    {

        public TeacherService(IInstructorProfileRepository instructorProfileRepository,
            IUnitOfWork unitOfWork)
            : base(instructorProfileRepository, unitOfWork)
        {
        }

        public IEnumerable<TeacherModel> GetAllTeachers(IEnumerable<string> includes = null)
        {
            var incluesList = includes == null ? new List<string>() : includes.ToList();
            if (!incluesList.Contains("Person"))
            {
                incluesList.Add("Person");
            }
            var teachers = Repository.GetAll(incluesList);
            var result = TranferToModels(teachers).ToList();
            return result;
        }

        public Guid Create(TeacherModel teacherModel)
        {
            var teacher = TranferToEntity(teacherModel);
            Repository.Insert(teacher);
            UnitOfWork.Commit();
            var result = teacher.Id;
            return result;
        }
    }
}
