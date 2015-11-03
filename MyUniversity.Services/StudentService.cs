using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Services
{
    public class StudentService : BaseService<StudentModel, StudentProfile, Guid, IStudentProfileRepository>, IStudentService
    {

        public StudentService(IStudentProfileRepository studentProfileRepository,
            IUnitOfWork unitOfWork)
            : base(studentProfileRepository, unitOfWork)
        {
        }

        public IEnumerable<StudentModel> GetStudentModel(IEnumerable<string> includes)
        {
            var students = Repository.Entity();
            if (includes != null)
            {
                students = includes.Aggregate(students, (current, include) => current.Include(include));
            }
            var result = TranferToModels(students).ToList();
            return result;
        }
    }
}
