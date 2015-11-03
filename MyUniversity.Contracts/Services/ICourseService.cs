using System;
using System.Collections.Generic;
using MyUniversity.Contracts.Models;

namespace MyUniversity.Contracts.Services
{
    public interface ICourseService: IBaseService<CourseModel, Guid>
    {
        IEnumerable<CourseModel> GetAllCourses(IEnumerable<string> includes);
    }
}