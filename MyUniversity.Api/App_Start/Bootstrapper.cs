using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;
using MyUniversity.Api.Helpers;
using MyUniversity.ApiStart;

namespace MyUniversity.Api
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            var dbFrameworkUse = AppSettingHelper.GetDbFrameworkType();
            AutoMapperConfig.Configure();
          

            // Register for autofac
            var builder = SetAutofacContainer.GetBuilder(dbFrameworkUse);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            DatabaseInitialization.Run(dbFrameworkUse);

            // Config for Auto Mapper
            AutoMapperConfig.Configure();

        }
    }
}