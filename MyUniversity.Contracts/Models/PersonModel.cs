using System;
using System.ComponentModel.DataAnnotations;

namespace MyUniversity.Contracts.Models
{
    public class PersonModel : BaseModel
    {
        [Display(Name = "Identity Number")]
        public string IdentityNumber { get; set; }
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Display(Name="Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
    }
}
