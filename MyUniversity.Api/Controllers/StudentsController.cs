using System.Collections.Generic;
using System.Web.Http;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;

namespace MyUniversity.Api.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsController : BaseController
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<StudentModel> Index()
        {
            var students = studentService.GetStudents(null);
            return students;
        }

    }


}
