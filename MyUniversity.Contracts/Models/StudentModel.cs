using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyUniversity.Contracts.Models
{
    public class StudentModel : BaseModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime EffectiveDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }
        [Required]
        public Guid? DepartmentId { get; set; }
        public DepartmentModel Department { get; set; }
        public PersonModel Person { get; set; }
        public IEnumerable<EnrollmentModel> Enrollments { get; set; }
    }
}
