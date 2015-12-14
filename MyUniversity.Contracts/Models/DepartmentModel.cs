using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataMember]
        public IEnumerable<CourseModel> Courses { get; set; }

        [DataMember]
        public Guid? DeanId { get; set; }

        [DataMember]
        public TeacherModel Dean { get; set; }

        [DataMember]
        public IEnumerable<OfficeAssignmentModel> OfficeAssignments { get; set; }

        [DataMember]
        public IEnumerable<TeacherModel> Instructors { get; set; }

        [DataMember]
        public IEnumerable<StudentModel> Students { get; set; }

    }
}
