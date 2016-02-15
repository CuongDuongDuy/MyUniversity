using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using Newtonsoft.Json;

namespace MyUniversity.Api.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsController : BaseController
    {
        private readonly IStudentService studentService;
        private readonly IEnrollmentService enrollmentService;

        public StudentsController(IStudentService studentService, IEnrollmentService enrollmentService)
        {
            this.studentService = studentService;
            this.enrollmentService = enrollmentService;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<StudentModel> GetAll()
        {
            var students = studentService.GetStudents(QueryExpand());
            return students;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newGuid = studentService.Create(studentModel);
                    return Created("api/students/" + newGuid, newGuid);
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("{id:guid}")]
        public HttpResponseMessage Edit(Guid id, StudentModel studentModel)
        {
            var result = new HttpResponseMessage();
            if (ModelState.IsValid)
            {
                var updated = studentService.Update(id, studentModel);
                result.StatusCode = HttpStatusCode.OK;
                result.Content = new StringContent(JsonConvert.SerializeObject(updated));
            }
            else
            {
                result.StatusCode = HttpStatusCode.BadRequest;
            }
            return result;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public StudentModel GetById(Guid id)
        {
            var result = studentService.GetById(id, QueryExpand());
            return result;
        }

        [HttpGet]
        [Route("{id:guid}/enrollments")]
        public IEnumerable<EnrollmentModel> GetEnrollemntsByStudentId(Guid id)
        {
            var result = enrollmentService.GetByStudentId(id);
            return result;
        }
    }


}
