using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyUniversity.Dal.Entities
{
    public class Course : EntityBase
    {
        public string Title { get; set; }
        public double Credits { get; set; }
        public Guid DepartmentId { get; set; }
        [JsonIgnore]
        public virtual Department Department { get; set; }
        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
