using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Services
{
    public class EnrollmentService : BaseService<EnrollmentModel, Enrollment, Guid, IBaseRepository<Enrollment, Guid>>, IEnrollmentService
    {
        public EnrollmentService(IBaseRepository<Enrollment, Guid> repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public IEnumerable<EnrollmentModel> GetEnrollments()
        {
            var enrollments = Repository.GetAll(new[] { "Course", "InstructorProfile", "InstructorProfile.Person", "StudentProfile", "StudentProfile.Person" });
            var result = TranferToModels(enrollments);
            return result;
        }

        public EnrollmentModel GetById(Guid id)
        {
            var enrollment = Repository.GetItems(x=>x.Id == id, new[] { "Course", "InstructorProfile", "InstructorProfile.Person", "StudentProfile", "StudentProfile.Person" }).FirstOrDefault();
            var result = TranferToModel(enrollment);
            return result;
        }

        public IEnumerable<EnrollmentModel> GetByStudentId(Guid studentId)
        {
            var enrollments = Repository.GetItems(x => x.StudentProfileId == studentId,
                new[]
                {"Course", "InstructorProfile", "InstructorProfile.Person", "StudentProfile", "StudentProfile.Person"});
            var result = TranferToModels(enrollments);
            return result;
        }

        public ModificationServiceResult Update(Guid id, EnrollmentModel departmentModel)
        {
            var result = new ModificationServiceResult(id);
            try
            {
                var enrollmentToUpdate = Repository.GetById(id);
                enrollmentToUpdate.Mark = departmentModel.Mark;
                enrollmentToUpdate.RowVersion = departmentModel.RowVersion;
                Repository.Update(enrollmentToUpdate);
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

        public Guid Create(EnrollmentModel enrollmentModel)
        {
            var enrollment = TranferToEntity(enrollmentModel);
            Repository.Insert(enrollment);
            UnitOfWork.Commit();
            var result = enrollment.Id;
            return result;
        }
    }
}
