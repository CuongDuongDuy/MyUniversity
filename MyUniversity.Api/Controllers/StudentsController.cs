using System.Collections.Generic;
using System.Web.Http;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;

namespace MyUniversity.Api.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<StudentModel> GetModel()
        {
            var result = studentService.GetStudentModel();
            return result;
        }

    }
}