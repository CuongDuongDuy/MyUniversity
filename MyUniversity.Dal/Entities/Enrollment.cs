using System;
using Newtonsoft.Json;

namespace MyUniversity.Dal.Entities
{
    public class Enrollment : EntityBase
    {
        public virtual Guid CourseId { get; set; }
        public virtual Guid StudentProfileId { get; set; }
        public virtual Guid InstructorProfileId { get; set; }
        public virtual double? Mark { get; set; }
        [JsonIgnore]
        public virtual Course Course { get; set; }
        [JsonIgnore]
        public virtual StudentProfile StudentProfile { get; set; }
        [JsonIgnore]
        public virtual InstructorProfile InstructorProfile { get; set; }
    }
}
