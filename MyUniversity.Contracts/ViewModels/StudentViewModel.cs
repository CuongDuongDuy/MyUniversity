using MyUniversity.Contracts.Models;

namespace MyUniversity.Contracts.ViewModels
{
    public class StudentViewModel
    {
        public StudentModel Student { get; set; }

        public StudentProfileModel StudentProfile { get; set; }
    }
}