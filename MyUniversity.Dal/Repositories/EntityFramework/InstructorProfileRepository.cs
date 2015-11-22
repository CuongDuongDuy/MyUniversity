using System;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Dal.Repositories.EntityFramework
{
    public class InstructorProfileRepository : BaseRepository<InstructorProfile, Guid>, IInstructorProfileRepository
    {
        public InstructorProfileRepository(MyUniversityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
