using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyUniversity.Dal.Entities
{
    [Serializable]
    public class InstructorProfile : Profile
    {
        public virtual DateTime HireDate { get; set; }
        public virtual Guid? DepartmentId { get; set; }
        [JsonIgnore]
        public virtual Department Department { get; set; }
        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
