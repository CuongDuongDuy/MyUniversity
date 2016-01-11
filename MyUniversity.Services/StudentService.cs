using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoMapper.QueryableExtensions;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Services
{
    public class StudentService : BaseService<StudentModel, StudentProfile, Guid, IBaseRepository<StudentProfile, Guid>>, IStudentService
    {
        private readonly IBaseRepository<Person, Guid> personRepository;

        public StudentService(IBaseRepository<StudentProfile, Guid> studentProfileRepository, IBaseRepository<Person, Guid> personRepository,
            IUnitOfWork unitOfWork)
            : base(studentProfileRepository, unitOfWork)
        {
            this.personRepository = personRepository;
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
            var department = Repository.GetItems(x => x.Id == id, incluesList).FirstOrDefault();
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

        public ModificationServiceResult Update(Guid id, StudentModel studentModel)
        {
            var result = new ModificationServiceResult(id);
            try
            {
                var teacherToUpdate = Repository.GetById(id);
                teacherToUpdate.Person.IdentityNumber = studentModel.Person.IdentityNumber;
                teacherToUpdate.Person.FirstName = studentModel.Person.FirstName;
                teacherToUpdate.Person.LastName = studentModel.Person.LastName;
                teacherToUpdate.Person.DateOfBirth = studentModel.Person.DateOfBirth;
                teacherToUpdate.Person.Address = studentModel.Person.Address;
                teacherToUpdate.Person.RowVersion = studentModel.Person.RowVersion;

                teacherToUpdate.EnrollmentDate = studentModel.EnrollmentDate;
                teacherToUpdate.EffectiveDate = studentModel.EffectiveDate;
                teacherToUpdate.ExpiryDate = studentModel.ExpiryDate;
                teacherToUpdate.RowVersion = studentModel.RowVersion;
                Repository.Update(teacherToUpdate);
                personRepository.Update(teacherToUpdate.Person);
                UnitOfWork.Commit();
                result.Value = id;
            }
            catch (DbUpdateConcurrencyException)
            {
                result.Type = ResultType.DbUpdateConcurrencyException;
            }
            catch (DataException)
            {
                result.Type = ResultType.DataException;
            }
            return result;
        }
    }
}
