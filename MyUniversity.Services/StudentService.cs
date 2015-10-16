using System;
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
        
    }
}
