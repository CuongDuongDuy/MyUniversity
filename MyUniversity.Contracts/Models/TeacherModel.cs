using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyUniversity.Contracts.Models
{
    public class TeacherModel : BaseModel
    {
        [Display(Name = "Hire Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        [Display(Name = "Effective Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime EffectiveDate { get; set; }
        [Display(Name = "Expiry Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }
        public Guid? DepartmentId { get; set; }
        public DepartmentModel Department { get; set; }
        public PersonModel Person { get; set; }
        public IEnumerable<EnrollmentModel> Enrollments { get; set; }
    }
}
