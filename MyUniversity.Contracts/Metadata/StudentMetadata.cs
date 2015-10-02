using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyUniversity.Contracts.Metadata
{
    public class StudentMetadata
    {
        [DisplayName("Latest Changed")]
        public virtual DateTime? UpdatedOn { get; set; }

        [DisplayName("First Name")]
        [Required]
        public virtual string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public virtual string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", NullDisplayText = "Type your birthday", ApplyFormatInEditMode = true)]
        [Required]
        public virtual DateTime DateOfBirth { get; set; }
    }
}