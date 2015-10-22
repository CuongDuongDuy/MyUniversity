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
            var attachedAssembly = Assembly.Load("MyUniversity.ApiStart");

            // Register for autofac
            var assemblyType = attachedAssembly.GetType("MyUniversity.ApiStart.SetAutofacContainer");
            var method = assemblyType.GetMethod("GetBuilder");
            var autofacInstance = Activator.CreateInstance(assemblyType);
            var builder = method.Invoke(autofacInstance, new object[] { dbFrameworkUse }) as ContainerBuilder;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            if (builder != null) config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            assemblyType = attachedAssembly.GetType("MyUniversity.ApiStart.DatabaseInitialization");
            method = assemblyType.GetMethod("Run");
            var dbInstance = Activator.CreateInstance(assemblyType);
            method.Invoke(dbInstance, new object[] { dbFrameworkUse });

            // Config for Auto Mapper
            assemblyType = attachedAssembly.GetType("MyUniversity.ApiStart.AutoMapperConfig");
            method = assemblyType.GetMethod("Configure");
            var autoMapperInstance = Activator.CreateInstance(assemblyType);
            method.Invoke(autoMapperInstance, null);

        }
    }
}