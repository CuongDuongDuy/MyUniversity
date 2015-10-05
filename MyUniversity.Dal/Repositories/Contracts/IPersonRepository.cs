using System;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Dal.Repositories.Contracts
{
    public interface IPersonRepository : IBaseRepository<Person, Guid>
    {
        
    }
}