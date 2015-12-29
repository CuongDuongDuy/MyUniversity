using System;
using System.Collections.Generic;

namespace MyUniversity.Contracts.Models
{
    public class TeacherModel : BaseModel
    {
        public DateTime HireDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public Guid? DepartmentId { get; set; }
        public DepartmentModel Department { get; set; }
        public PersonModel Person { get; set; }
        public IEnumerable<EnrollmentModel> Enrollments { get; set; }
    }
}
