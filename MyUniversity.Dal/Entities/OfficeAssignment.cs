using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyUniversity.Dal.Entities
{
    public class OfficeAssignment : EntityBase
    {
        public string Location { get; set; }
        public string OpeningTime { get; set; }
        public string ClosedTime { get; set; }
        public string Phone { get; set; }
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }

    }
}
