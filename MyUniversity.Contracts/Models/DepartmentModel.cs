using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyUniversity.Contracts.Models
{
    [DataContract(IsReference = true)]
    public class DepartmentModel : BaseModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public IEnumerable<CourseModel> Courses { get; set; } 

    }
}
