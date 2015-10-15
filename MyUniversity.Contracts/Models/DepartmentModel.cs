using System;
using System.Collections.Generic;

namespace MyUniversity.Contracts.Models
{
    public class DepartmentModel : BaseModel
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public IEnumerable<CourseModel> Courses { get; set; } 

    }
}
