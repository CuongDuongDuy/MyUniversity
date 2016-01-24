using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;
using NHibernate;

namespace MyUniversity.Services
{
    public class CourseService : BaseService<CourseModel, Course, Guid, IBaseRepository<Course, Guid>>, ICourseService
    {
        public CourseService(IBaseRepository<Course, Guid> repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public IEnumerable<CourseModel> GetAllCourses(IEnumerable<string> includes)
        {
            var courses = Repository.GetAll(includes);
            var result = TranferToModels(courses).ToList();
            return result;
        }

        public CourseModel GetById(Guid id, IEnumerable<string> includes)
        {
            CourseModel result;
            if (includes == null)
            {
                var course = Repository.GetById(id);
                result = TranferToModel(course);
            }
            else
            {
                var course = Repository.GetItems(x => x.Id == id, includes).First();
                result = TranferToModel(course);
            }
            return result;
        }

        public IEnumerable<CourseModel> GetCoursesByName(string nameSearch, IEnumerable<string> includes)
        {
            var courses = Repository.GetItems(x => x.Title.Contains(nameSearch), includes);
            var result = TranferToModels(courses);
            return result;
        }

        public Guid Create(CourseModel courseModel)
        {
            var course = TranferToEntity(courseModel);
            Repository.Insert(course);
            UnitOfWork.Commit();
            var result = course.Id;
            return result;
        }

        public ModificationServiceResult Update(Guid id, CourseModel coureModel)
        {
            var result = new ModificationServiceResult(id);
            try
            {
                var courseToUpdate = Repository.GetById(id);
                courseToUpdate.Title = coureModel.Title;
                courseToUpdate.Credits = coureModel.Credits;
                courseToUpdate.DepartmentId = coureModel.DepartmentId;
                courseToUpdate.RowVersion = coureModel.RowVersion;
                Repository.Update(courseToUpdate);
                UnitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                result.Type = ResultType.DbUpdateConcurrencyException;
            }
            catch (DataException)
            {
                result.Type = ResultType.DataException;
            }
            catch (StaleObjectStateException)
            {
                result.Type = ResultType.DbUpdateConcurrencyException;
            }
            return result;
        }
    }
}