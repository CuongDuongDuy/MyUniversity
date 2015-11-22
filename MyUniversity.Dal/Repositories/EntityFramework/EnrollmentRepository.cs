using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Dal.Repositories.EntityFramework
{
    public class EnrollmentRepository : BaseRepository<Enrollment, Guid>, IEnrollmentRepository
    {
        public EnrollmentRepository(MyUniversityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
