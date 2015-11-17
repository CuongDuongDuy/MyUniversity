using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using MyUniversity.Api.Helpers;

namespace MyUniversity.Api
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            var dbFrameworkUse = AppSettingHelper.GetDbFrameworkType();
            ApiStart.AutoMapperConfig.Configure();
          

            // Register for autofac
            var builder = ApiStart.SetAutofacContainer.GetBuilder(dbFrameworkUse);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            ApiStart.DatabaseInitialization.Run(dbFrameworkUse);

            // Config for Auto Mapper
            ApiStart.AutoMapperConfig.Configure();

        }
    }
}