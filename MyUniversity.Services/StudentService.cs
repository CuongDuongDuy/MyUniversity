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

        public IEnumerable<StudentModel> GetStudents(IEnumerable<string> includes)
        {
            var students = Repository.GetAll(includes);
            var result = TranferToModels(students).ToList();
            return result;
        }

    }
}
