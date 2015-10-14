using System;
using System.ComponentModel.DataAnnotations;
using MyUniversity.Contracts.Metadata;

namespace MyUniversity.Contracts.Models
{
    [MetadataType(typeof (StudentProfileMetadata))]
    public class StudentModel : BaseModel
    {
        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
