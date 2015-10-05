using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Dal.Repositories.EntityFramework
{
    public class PersonRepository : BaseRepository<Person, Guid>, IPersonRepository
    {

    }
}
