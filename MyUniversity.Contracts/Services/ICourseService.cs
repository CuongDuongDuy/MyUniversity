using System;
using System.Linq;
using MyUniversity.Contracts.Models;

namespace MyUniversity.Contracts.Services
{
    public interface ICourseService: IBaseService<CourseModel, Guid>
    {
    }
}