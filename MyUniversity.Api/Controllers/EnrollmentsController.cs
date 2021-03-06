﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using Newtonsoft.Json;

namespace MyUniversity.Api.Controllers
{
    [RoutePrefix("api/enrollments")]
    public class EnrollmentsController : BaseController
    {
        private readonly IEnrollmentService enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            this.enrollmentService = enrollmentService;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<EnrollmentModel> GetAll()
        {
            var result = enrollmentService.GetEnrollments();
            return result;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public EnrollmentModel GetById(Guid id)
        {
            var result = enrollmentService.GetById(id);
            return result;
        }

        [HttpPut]
        [Route("{id:guid}")]
        public HttpResponseMessage Edit(Guid id, EnrollmentModel departmentModel)
        {
            var result = new HttpResponseMessage();
            if (ModelState.IsValid)
            {
                var updated = enrollmentService.Update(id, departmentModel);
                result.StatusCode = HttpStatusCode.OK;
                result.Content = new StringContent(JsonConvert.SerializeObject(updated));
            }
            else
            {
                result.StatusCode = HttpStatusCode.BadRequest;
            }
            return result;
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Create(EnrollmentModel enrollmentModel)
        {
            var result = new HttpResponseMessage();
            if (ModelState.IsValid)
            {
                try
                {
                    var newGuid = enrollmentService.Create(enrollmentModel);
                    result.StatusCode = HttpStatusCode.Created;
                    result.Content = new StringContent(newGuid.ToString());
                }
                catch(Exception ex)
                {
                    result.StatusCode = HttpStatusCode.BadRequest;
                    InternalServerError(ex);
                }

            }
            return result;
        }
    }
}
