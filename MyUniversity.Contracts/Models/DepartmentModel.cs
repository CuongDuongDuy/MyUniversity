using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MyUniversity.Contracts.Models
{
    [DataContract(IsReference = true)]
    [JsonObject(IsReference = false)]
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
