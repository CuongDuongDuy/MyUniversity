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
    public class CourseService : BaseService<CourseModel, Course, Guid, ICourseRepository>, ICourseService
    {

        public CourseService(ICourseRepository courseRepository, IUnitOfWork unitOfWork) : base(courseRepository, unitOfWork)
        {
        }


        public IEnumerable<CourseModel> GetAllCourses(IEnumerable<string> includes)
        {
            var courses = Repository.Entity();
            if (includes != null)
            {
                courses = includes.Aggregate(courses, (current, include) => current.Include(include));
            }
            var result = TranferToModels(courses).ToList();
            return result;
        }
    }
}