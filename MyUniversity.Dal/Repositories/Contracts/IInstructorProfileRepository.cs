using System;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Repositories.Contracts
{
    public interface IInstructorProfileRepository : IBaseRepository<InstructorProfile, Guid>
    {
    }
}
