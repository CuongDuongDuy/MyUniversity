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
        private readonly IStudentProfileRepository studentRepository;

        public StudentService(IStudentProfileRepository studentRepository,
            IStudentProfileRepository studentProfileRepository,
            IUnitOfWork unitOfWork)
            : base(studentRepository, unitOfWork)
        {
            this.studentRepository = studentProfileRepository;
        }

        public IEnumerable<StudentModel> GetStudentModel(IEnumerable<string> includes)
        {
            var students = studentRepository.Entity();
            if (includes != null)
            {
                students = includes.Aggregate(students, (current, include) => current.Include(include));
            }
            var result = TranferToModels(students).ToList();
            return result;
        }
    }
}
