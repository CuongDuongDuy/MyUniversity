using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Contracts.ViewModels;

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

        [HttpGet]
        [Route("{id:guid}")]
        public StudentModel GetById(Guid id)
        {
            var result = studentService.GetById(id, QueryExpand());
            return result;
        }
    }


}
