using System;
using System.Collections.Generic;
using MyUniversity.Contracts.Models;

namespace MyUniversity.Contracts.Services
{
    public interface ICourseService: IBaseService<CourseModel, Guid>
    {
        IEnumerable<CourseModel> GetAllCourses(IEnumerable<string> includes = null);
        CourseModel GetById(Guid id, IEnumerable<string> includes = null);
        IEnumerable<CourseModel> GetCoursesByName(string nameSearch, IEnumerable<string> includes = null);
    }
}