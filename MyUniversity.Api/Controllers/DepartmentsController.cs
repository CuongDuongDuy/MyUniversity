﻿using System.Collections.Generic;
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
            var result = departmentService.GetAllDepartments();
            return result;
        }

        [HttpGet]
        [Route("{includes:regex([A-Za-z0-9\\-]+)}")]
        public IEnumerable<DepartmentModel> GetAll(string includes)
        {
            var result = departmentService.GetAllDepartments(includes != null ? includes.Split('-') : null);
            return result;
        }
    }
}
