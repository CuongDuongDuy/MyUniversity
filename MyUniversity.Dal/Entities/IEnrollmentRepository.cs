using System;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Dal.Entities
{
    public interface IEnrollmentRepository : IBaseRepository<Enrollment, Guid>
    {

    }
}