using System;
using System.Collections.Generic;
using System.Text;

namespace MyUniversity.Contracts.Models
{
    public class PersonBaseModel : BaseModel
    {
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
