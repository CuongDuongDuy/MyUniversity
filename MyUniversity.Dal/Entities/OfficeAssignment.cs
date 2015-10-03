using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MyUniversity.Dal.Entities
{
    public class OfficeAssignment : EntityBase
    {
        public string Location { get; set; }
        public string WorkingHours { get; set; }
        public string Phone { get; set; }
        public Guid DepartmentId { get; set; }
        [JsonIgnore]
        public virtual Department Department { get; set; }

    }
}
