using System.Collections.Generic;

namespace MyUniversity.Contracts.Models
{
    public class CourseModel : BaseModel
    {
        public string Title { get; set; }
        public double Credits { get; set; }
        public DepartmentModel Department { get; set; }
        public IEnumerable<EnrollmentModel> Enrollments { get; set; }
    }
}
