using System;
using System.Linq;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Services
{
    public class CourseService:BaseService<CourseModel, Course, Guid, ICourseRepository>, ICourseService
    {

    }
}