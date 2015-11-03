using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyUniversity.Contracts.Models
{
    public class DepartmentModel : BaseModel
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        [IgnoreDataMember]
        public IEnumerable<CourseModel> Courses { get; set; } 

    }
}
