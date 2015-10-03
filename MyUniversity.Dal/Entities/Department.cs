using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyUniversity.Dal.Entities
{
    public class Department: EntityBase
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public Guid? DeanId { get; set; }
        [JsonIgnore]
        public virtual InstructorProfile Dean { get; set; }
        [JsonIgnore]
        public virtual ICollection<Course> Courses { get; set; }
        [JsonIgnore]
        public virtual ICollection<OfficeAssignment> OfficeAssignments { get; set; }
        [JsonIgnore]
        public virtual ICollection<InstructorProfile> InstructorProfiles { get; set; }
        [JsonIgnore]
        public virtual ICollection<StudentProfile> StudentProfiles { get; set; } 
        
    }
}
