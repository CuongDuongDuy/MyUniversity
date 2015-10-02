using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyUniversity.Dal.Entities
{
    public class Person : EntityBase
    {
        public string IdentityNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<Profile> Profiles { get; set; }  

    }
}
