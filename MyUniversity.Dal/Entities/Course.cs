using System;
using System.Collections.Generic;

namespace MyUniversity.Dal.Entities
{
    public class Course : EntityBase
    {
        public string Title { get; set; }
        public double Credits { get; set; }
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<InstructorProfile> InstructorProfiles { get; set; } 
    }
}
