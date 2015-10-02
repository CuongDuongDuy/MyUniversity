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
            var instance = Activator.CreateInstance(assemblyType);
            var builder = method.Invoke(instance, new object[] { dbFrameworkUse }) as ContainerBuilder;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            if (builder != null) config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());

            assemblyType = attachedAssembly.GetType("MyUniversity.ApiStart.DatabaseInitialization");
            method = assemblyType.GetMethod("Run");
            instance = Activator.CreateInstance(assemblyType);
            method.Invoke(instance, new object[] { dbFrameworkUse });

            // Config for Auto Mapper
            assemblyType = attachedAssembly.GetType("MyUniversity.ApiStart.AutoMapperConfig");
            method = assemblyType.GetMethod("Configure");
            instance = Activator.CreateInstance(assemblyType);
            method.Invoke(instance, null);
        }
    }
}