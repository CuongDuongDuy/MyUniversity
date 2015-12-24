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

        public StudentModel GetById(Guid id, IEnumerable<string> includes = null)
        {
            var incluesList = includes == null ? new List<string>() : includes.ToList();
            if (!incluesList.Contains("Person"))
            {
                incluesList.Add("Person");
            }
            var department = includes == null ? Repository.GetById(id) : Repository.GetItems(x => x.Id == id, incluesList).FirstOrDefault();
            var result = TranferToModel(department);
            return result;
        }

        public IQueryable<StudentModel> GetAsQueryable()
        {
            var result = Repository.GetAll(null).Project().To<StudentModel>();
            return result;
        }

        public Guid Create(StudentModel studentModel)
        {
            var student = TranferToEntity(studentModel);
            Repository.Insert(student);
            UnitOfWork.Commit();
            var result = student.Id;
            return result;
        }
    }
}
