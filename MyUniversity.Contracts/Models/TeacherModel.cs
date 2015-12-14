using System;
using System.Collections.Generic;

namespace MyUniversity.Contracts.Models
{
    public class TeacherModel : PersonBaseModel
    {
        public DateTime HireDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DepartmentModel Department { get; set; }
        public IEnumerable<EnrollmentModel> Enrollments { get; set; }
    }
}
