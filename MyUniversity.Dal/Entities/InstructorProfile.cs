using System;
using System.Collections.Generic;

namespace MyUniversity.Dal.Entities
{
    [Serializable]
    public class InstructorProfile : Profile
    {
        public DateTime HireDate { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
