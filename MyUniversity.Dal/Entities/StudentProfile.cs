using System;
using Newtonsoft.Json;

namespace MyUniversity.Dal.Entities
{
    [Serializable]
    public class StudentProfile : Profile
    {
        public DateTime EnrollmentDate { get; set; }
        public Guid DepartmentId { get; set; }
        [JsonIgnore]
        public virtual Department Department { get; set; }

    }
}