using System;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;
using NHibernate;

namespace MyUniversity.Dal.Repositories.NHibernate
{
    public class StudentProfileRepository : RepositoryBase<StudentProfile, Guid>, IStudentProfileRepository
    {
        public StudentProfileRepository(ISession session)
            : base(session)
        {
        }
    }
}
