using System.Web.Http;
using MyUniversity.Contracts.Services;

namespace MyUniversity.Api.Controllers
{
    public class StudentsController : ApiController
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

    }
}