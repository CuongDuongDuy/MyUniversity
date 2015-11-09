using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;

namespace MyUniversity.Api.Controllers
{
    [RoutePrefix("api/courses")]
    public class CoursesController : ApiController
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
            var result = courseService.GetAllCourses();
            return result;
        }

        [HttpGet]
        [Route("{includes:regex([A-Za-z0-9\\-]+)}")]
        public IEnumerable<CourseModel> GetAll(string includes)
        {
            var result = courseService.GetAllCourses(includes != null ? includes.Split('-') : null);
            return result;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public CourseModel GetById(Guid id)
        {
            var result = courseService.GetById(id);
            return result;
        }

        [HttpGet]
        [Route("{id:guid}/{includes:regex([A-Za-z0-9\\-]+)}")]
        public CourseModel GetById(Guid id, string includes)
        {
            var result = courseService.GetById(id, includes != null ? includes.Split('-') : null);
            return result;
        }

        [HttpGet]
        [Route("searchname/{nameSearch:regex([A-Za-z0-9]+)}")]
        public IEnumerable<CourseModel> GetBySearchName(string nameSearch)
        {
            var result = courseService.GetCoursesByName(nameSearch);
            return result;
        }

        [HttpGet]
        [Route("searchname/{nameSearch:regex([A-Za-z0-9]+)}/{includes:regex([A-Za-z0-9\\-])}")]
        public IEnumerable<CourseModel> GetBySearchName(string nameSearch, string includes)
        {
            var result = courseService.GetCoursesByName(nameSearch, includes != null ? includes.Split('-') : null);
            return result;
        }

    }
}
