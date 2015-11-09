using System.Collections.Generic;
using System.Web.Http;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;

namespace MyUniversity.Api.Controllers
{
    [RoutePrefix("api/departments")]
    public class DepartmentsController : ApiController
    {
        private readonly IDepartmentService departmentService;
        public DepartmentsController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<DepartmentModel> GetAll()
        {
            var result = departmentService.GetAllDepartments(null);
            return result;
        }

        [HttpGet]
        [Route("courses")]
        public IEnumerable<DepartmentModel> GetAllPlusCourses()
        {
            var result = departmentService.GetAllDepartments(new[] { "Courses" });
            return result;
        }
    }
}
