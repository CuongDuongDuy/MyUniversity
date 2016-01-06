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
            var result = teacherService.GetTeachers(QueryExpand());
            return result;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(TeacherModel teacherModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newGuid = teacherService.Create(teacherModel);
                    return Created("api/teachers/" + newGuid, newGuid);
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
        public TeacherModel GetById(Guid id)
        {
            var result = teacherService.GetById(id, QueryExpand());
            return result;
        }

        [HttpPut]
        [Route("{id:guid}")]
        public HttpResponseMessage Edit(Guid id, TeacherModel teacherModel)
        {
            var result = new HttpResponseMessage();
            if (ModelState.IsValid)
            {
                var updated = teacherService.Update(id, teacherModel);
                result.StatusCode = HttpStatusCode.OK;
                result.Content = new StringContent(JsonConvert.SerializeObject(updated));
            }
            else
            {
                result.StatusCode = HttpStatusCode.BadRequest;
            }
            return result;
        }
    }
}
