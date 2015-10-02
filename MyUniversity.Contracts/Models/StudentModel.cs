using System;
using System.ComponentModel.DataAnnotations;
using MyUniversity.Contracts.Metadata;

namespace MyUniversity.Contracts.Models
{
    [MetadataType(typeof (StudentMetadata))]
    public class StudentModel : BaseModel
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
    }
}