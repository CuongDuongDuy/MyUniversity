using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Services
{
    public class CourseService : BaseService<CourseModel, Course, Guid, ICourseRepository>, ICourseService
    {

        public CourseService(ICourseRepository courseRepository, IUnitOfWork unitOfWork) : base(courseRepository, unitOfWork)
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
    }
}