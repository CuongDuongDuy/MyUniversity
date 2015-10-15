namespace MyUniversity.Contracts.Models
{
    public class OfficeAssignmentModel : BaseModel
    {
        public string Location { get; set; }
        public string WorkingHours { get; set; }
        public string Phone { get; set; }
        public DepartmentModel Department { get; set; }
    }
}
