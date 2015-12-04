using System;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using MyUniversity.Contracts.Models;
using MyUniversity.Dal.Entities;

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
            builder.EntitySet<StudentProfile>("Students");
            builder.EntitySet<Person>("Persons");
            builder.EntitySet<Department>("Departments");
            builder.EntitySet<Enrollment>("Enrollments");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

        }
    }
}
