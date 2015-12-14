using MyUniversity.Contracts.Services;

namespace MyUniversity.Api.Controllers
{
    public class StudentsController : BaseController
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }
    }


}
