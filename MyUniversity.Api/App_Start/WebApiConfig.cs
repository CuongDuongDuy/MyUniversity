﻿using System;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using MyUniversity.Contracts.Models;

namespace MyUniversity.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<StudentModel>("Students");
            builder.EntitySet<DepartmentModel>("Departments");
            builder.EntitySet<EnrollmentModel>("Enrollments");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

        }
    }
}
