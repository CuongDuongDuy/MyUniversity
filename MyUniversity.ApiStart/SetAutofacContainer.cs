using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using MyUniversity.Contracts.Constants;
using MyUniversity.Dal;
using MyUniversity.Dal.Repositories.Contracts;
using MyUniversity.Dal.Repositories.EntityFramework;
using NHibernate;
using NHStudentProfileRepository = MyUniversity.Dal.Repositories.NHibernate.StudentProfileRepository;
using NHStudentRepository = MyUniversity.Dal.Repositories.NHibernate.StudentProfileRepository;
using NHUnitOfWork = MyUniversity.Dal.Repositories.NHibernate.UnitOfWork;
using EFStudentRepository = MyUniversity.Dal.Repositories.EntityFramework.StudentProfileRepository;
using EFStudentProfileRepository = MyUniversity.Dal.Repositories.EntityFramework.StudentProfileRepository;
using EFUnitOfWork = MyUniversity.Dal.Repositories.EntityFramework.UnitOfWork;

namespace MyUniversity.ApiStart
{
    public class SetAutofacContainer
    {
        public static ContainerBuilder GetBuilder(AppSettingConstant.DbFrameworkType dbFrameworkUse)
        {
            var builder = new ContainerBuilder();

            var serviceAssembly = Assembly.Load("MyUniversity.Services");
            builder.RegisterAssemblyTypes(serviceAssembly)
                   .Where(x => x.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            // Register for Repositories, UnitOfWork
            switch (dbFrameworkUse)
            {
                case AppSettingConstant.DbFrameworkType.Nhibernate:
                    builder.RegisterType<NHStudentProfileRepository>()
                        .As<IStudentProfileRepository>()
                        .InstancePerLifetimeScope();
                    builder.RegisterType<NHUnitOfWork>()
                        .As<IUnitOfWork>()
                        .InstancePerLifetimeScope();

                    builder.Register(x => DatabaseInitialization.GetConfig()
                        .BuildSessionFactory()).As<ISessionFactory>().SingleInstance();
                    builder.Register(x => x.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope();
                    break;

                case AppSettingConstant.DbFrameworkType.EntityFramework:
                    builder.RegisterType<EFStudentProfileRepository>()
                        .As<IStudentProfileRepository>()
                        .InstancePerLifetimeScope();
                    builder.RegisterType<EFUnitOfWork>()
                        .As<IUnitOfWork>()
                        .InstancePerLifetimeScope();
                    builder.RegisterType<CourseRepository>()
                        .As<ICourseRepository>()
                        .InstancePerLifetimeScope();
                    builder.RegisterType<DepartmentRepository>()
                       .As<IDepartmentRepository>()
                       .InstancePerLifetimeScope();
                    builder.RegisterType<MyUniversityDbContext>().InstancePerLifetimeScope();
                    break;
            }
            return builder;
        }

        private static string GetFileBinPath(string fileFileName)
        {
            var binPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            var result = Directory.GetFiles(binPath, fileFileName, SearchOption.AllDirectories).FirstOrDefault();
            return result;
        }
    }
}
