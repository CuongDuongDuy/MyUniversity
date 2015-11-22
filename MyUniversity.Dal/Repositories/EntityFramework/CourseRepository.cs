using System;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Dal.Repositories.EntityFramework
{
    public class CourseRepository : BaseRepository<Course, Guid>, ICourseRepository
    {
        public CourseRepository(MyUniversityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
