using System;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Dal.Repositories.EntityFramework
{
    public class PersonRepository : BaseRepository<Person, Guid>, IPersonRepository
    {

    }
}
