﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;

namespace MyUniversity.Api.Controllers
{
    [RoutePrefix("api/courses")]
    public class CoursesController : BaseController
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<CourseModel> GetAll()
        {
            var result = courseService.GetAllCourses(QueryExpand());
            return result;
        }

        //[HttpGet]
        //[Route("{includes:regex([A-Za-z0-9\\-]+)}")]
        //public IEnumerable<CourseModel> GetAll(string includes)
        //{
        //    var result = courseService.GetAllCourses(includes != null ? includes.Split('-') : null);
        //    return result;
        //}

        [HttpGet]
        [Route("{id:guid}")]
        public CourseModel GetById(Guid id)
        {
            var result = courseService.GetById(id, QueryExpand());
            return result;
        }

        [HttpGet]
        [Route("searchname/{nameSearch:regex([A-Za-z0-9]+)}")]
        public IEnumerable<CourseModel> GetBySearchName(string nameSearch)
        {
            var result = courseService.GetCoursesByName(nameSearch, QueryExpand());
            return result;
        }

        //[HttpGet]
        //[Route("searchname/{nameSearch:regex([A-Za-z0-9]+)}/{includes:regex([A-Za-z0-9\\-])}")]
        //public IEnumerable<CourseModel> GetBySearchName(string nameSearch, string includes)
        //{
        //    var result = courseService.GetCoursesByName(nameSearch, includes != null ? includes.Split('-') : null);
        //    return result;
        //}

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Index(CourseModel coureModel)
        {
            var result = new HttpResponseMessage();
            if (ModelState.IsValid)
            {
                try
                {
                    var newGuid = courseService.Create(coureModel);
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

        [HttpPut]
        [Route("{id:guid}")]
        public HttpResponseMessage Edit(Guid id, CourseModel coureModel)
        {
            var result = new HttpResponseMessage();
            if (ModelState.IsValid)
            {
                try
                {
                    courseService.Update(id, coureModel);
                    result.StatusCode = HttpStatusCode.Accepted;
                    result.Content = new StringContent(id.ToString());
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
