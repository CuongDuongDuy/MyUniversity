using System;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Dal.Repositories.EntityFramework
{
    public class StudentProfileRepository : BaseRepository<StudentProfile, Guid>, IStudentProfileRepository
    {
    }
}
