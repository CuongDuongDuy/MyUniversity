using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            var incluesList = includes == null ? new List<string>() : includes.ToList();
            if (!incluesList.Contains("Person"))
            {
                incluesList.Add("Person");
            }
            var students = Repository.GetAll(incluesList);
            var result = TranferToModels(students).ToList();
            return result;
        }

        public StudentModel GetById(Guid key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StudentModel> GetAsQueryable()
        {
            var result = Repository.GetAll(null).Project().To<StudentModel>();
            return result;
        }
    }
}
