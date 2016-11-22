using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyUniversity.Dal.Entities
{
    public class Department: EntityBase
    {
        public virtual string Name { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual Guid? DeanId { get; set; }
        public virtual InstructorProfile Dean { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<OfficeAssignment> OfficeAssignments { get; set; }
        public virtual ICollection<InstructorProfile> InstructorProfiles { get; set; }
        public virtual ICollection<StudentProfile> StudentProfiles { get; set; } 
        
    }
}
