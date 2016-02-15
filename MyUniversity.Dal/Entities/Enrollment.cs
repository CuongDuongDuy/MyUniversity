using System;
using Newtonsoft.Json;

namespace MyUniversity.Dal.Entities
{
    public class Enrollment : EntityBase
    {
        public Guid CourseId { get; set; }
        public Guid StudentProfileId { get; set; }
        public Guid InstructorProfileId { get; set; }
        public double? Mark { get; set; }
        [JsonIgnore]
        public virtual Course Course { get; set; }
        [JsonIgnore]
        public virtual StudentProfile StudentProfile { get; set; }
        [JsonIgnore]
        public virtual InstructorProfile InstructorProfile { get; set; }
    }
}
