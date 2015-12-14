using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;

namespace MyUniversity.Api.Controllers
{
    [RoutePrefix("api/departments")]
    public class DepartmentsController : BaseController
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
            var result = departmentService.GetAllDepartments();
            return result;
        }

        //[HttpGet]
        //[Route("{includes:regex([A-Za-z0-9\\-]+)}")]
        //public IEnumerable<DepartmentModel> GetAll(string includes)
        //{
        //    var result = departmentService.GetAllDepartments(includes != null ? includes.Split('-') : null);
        //    return result;
        //}

        [HttpGet]
        [Route("{id:guid}")]
        public DepartmentModel GetById(Guid id)
        {
            var result = departmentService.GetById(id, QueryExpand());
            return result;
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Index(DepartmentModel departmentModel)
        {
            var result = new HttpResponseMessage();
            if (ModelState.IsValid)
            {
                try
                {
                    var newGuid = departmentService.Create(departmentModel);
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
