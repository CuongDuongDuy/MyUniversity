using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyUniversity.Dal.Entities
{
    public class StudentProfile : Profile
    {
        public virtual DateTime EnrollmentDate { get; set; }
        public virtual Guid? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}