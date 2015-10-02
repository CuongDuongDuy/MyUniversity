using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyUniversity.Dal.Entities
{
    public class Enrollment : EntityBase
    {
        public Guid CourseId { get; set; }
        public Guid StudentProfileId { get; set; }
        public double Mark { get; set; }

        public virtual Course Course { get; set; }
        public virtual StudentProfile StudentProfile { get; set; }
    }
}
