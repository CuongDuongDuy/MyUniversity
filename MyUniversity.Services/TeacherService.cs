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
    public class TeacherService : BaseService<TeacherModel, InstructorProfile, Guid, IBaseRepository<InstructorProfile, Guid>>, ITeacherService
    {
        private readonly IPersonRepository personRepository;

        public TeacherService(IBaseRepository<InstructorProfile, Guid> teacherProfileRepository, IPersonRepository personRepository,
            IUnitOfWork unitOfWork)
            : base(teacherProfileRepository, unitOfWork)
        {
            this.personRepository = personRepository;
        }

        public IEnumerable<TeacherModel> GetTeachers(IEnumerable<string> includes)
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

        public TeacherModel GetById(Guid id, IEnumerable<string> includes = null)
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

        public IQueryable<TeacherModel> GetAsQueryable()
        {
            var result = Repository.GetAll(null).Project().To<TeacherModel>();
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

        public ModificationServiceResult Update(Guid id, TeacherModel teacherModel)
        {
            var result = new ModificationServiceResult(id);
            try
            {
                var teacherToUpdate = Repository.GetById(id);
                teacherToUpdate.Person.IdentityNumber = teacherModel.Person.IdentityNumber;
                teacherToUpdate.Person.FirstName = teacherModel.Person.FirstName;
                teacherToUpdate.Person.LastName = teacherModel.Person.LastName;
                teacherToUpdate.Person.DateOfBirth = teacherModel.Person.DateOfBirth;
                teacherToUpdate.Person.Address = teacherModel.Person.Address;
                teacherToUpdate.Person.RowVersion = teacherModel.Person.RowVersion;

                teacherToUpdate.HireDate = teacherModel.HireDate;
                teacherToUpdate.EffectiveDate = teacherModel.EffectiveDate;
                teacherToUpdate.ExpiryDate = teacherModel.ExpiryDate;
                teacherToUpdate.RowVersion = teacherModel.RowVersion;
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
