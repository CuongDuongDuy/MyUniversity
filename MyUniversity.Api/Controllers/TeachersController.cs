using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;

namespace MyUniversity.Api.Controllers
{
    [RoutePrefix("api/teachers")]
    public class TeachersController : BaseController
    {
        private readonly ITeacherService teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            this.teacherService = teacherService;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<TeacherModel> GetAll()
        {
            var result = teacherService.GetAllTeachers(QueryExpand());
            return result;
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Index(TeacherModel teacherModel)
        {
            var result = new HttpResponseMessage();
            if (ModelState.IsValid)
            {
                try
                {
                    var newGuid = teacherService.Create(teacherModel);
                    result.StatusCode = HttpStatusCode.Created;
                    result.Content = new StringContent(newGuid.ToString());
                }
                catch
                {
                    result.StatusCode = HttpStatusCode.BadRequest;
                }

            }
            return result;
        }
    }
}
