using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyUniversity.Dal.Entities
{
    public class StudentProfile : Profile
    {
        public DateTime EnrollmentDate { get; set; }
        public Guid? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}