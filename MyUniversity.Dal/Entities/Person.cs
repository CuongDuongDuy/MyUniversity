using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyUniversity.Dal.Entities
{
    public class Person : EntityBase
    {
        public virtual string IdentityNumber { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual DateTime DateOfBirth { get; set; }

        public virtual string Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<Profile> Profiles { get; set; }  

    }
}
