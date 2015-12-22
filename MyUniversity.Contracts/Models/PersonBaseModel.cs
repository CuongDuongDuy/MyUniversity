using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace MyUniversity.Contracts.Models
{
    public class PersonBaseModel : BaseModel
    {
        [Display(Name = "Identity Number")]
        public string IdentityNumber { get; set; }
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Display(Name="Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
        public byte[] PersonRowVersion { get; set; }
    }
}
