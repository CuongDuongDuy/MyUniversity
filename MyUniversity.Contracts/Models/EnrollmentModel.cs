namespace MyUniversity.Contracts.Models
{
    public class EnrollmentModel : BaseModel
    {
        public CourseModel Course { get; set; }
        public StudentModel Student{ get; set; }
        public TeacherModel Teacher { get; set; }
        public double? Mark { get; set; }
    }
}
