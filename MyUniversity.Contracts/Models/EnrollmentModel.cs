using System;

namespace MyUniversity.Contracts.Models
{
    public class EnrollmentModel : BaseModel
    {
        public Guid CourseId { get; set; }
        public CourseModel Course { get; set; }
        public Guid StudentId { get; set; }
        public StudentModel Student{ get; set; }
        public Guid TeacherId { get; set; }
        public TeacherModel Teacher { get; set; }
        public double? Mark { get; set; }
    }
}
