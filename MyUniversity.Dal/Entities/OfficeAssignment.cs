using System;
using Newtonsoft.Json;

namespace MyUniversity.Dal.Entities
{
    public class OfficeAssignment : EntityBase
    {
        public virtual string Location { get; set; }
        public virtual string WorkingHours { get; set; }
        public virtual string Phone { get; set; }
        public virtual Guid DepartmentId { get; set; }
        [JsonIgnore]
        public virtual Department Department { get; set; }

    }
}
