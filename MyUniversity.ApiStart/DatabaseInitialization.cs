using System;
using System.Diagnostics.Eventing.Reader;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MyUniversity.Contracts.Constants;
using MyUniversity.Dal;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Mappings.NHibernate;
using NHibernate.Event;
using NHibernate.Persister.Entity;

namespace MyUniversity.ApiStart
{
    public class DatabaseInitialization
    {
        public static FluentConfiguration GetNHibernateDbConfig()
        {
            return Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2012.ShowSql()
                        .ConnectionString(c => c.FromConnectionStringWithKey("MyUniversityDb")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CourseMapping>()
                    .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Always()))
            .ExposeConfiguration(
                c =>
                    c.EventListeners.SaveEventListeners =
                        new ISaveOrUpdateEventListener[] { new NHibernateEventListener.CustomSaveOrUpdateEventListener(), })
            .ExposeConfiguration(
                c =>
                    c.EventListeners.DeleteEventListeners =
                        new IDeleteEventListener[] { new NHibernateEventListener.CustomDeleteEventListener(), })
            .ExposeConfiguration(
                c =>
                    c.EventListeners.PostLoadEventListeners =
                        new IPostLoadEventListener[] { new NHibernateEventListener.CustomPostLoadEventListener(), });
        }

        public static void Run(AppSettingConstant.DbFrameworkType dbFrameworkUse)
        {
            switch (dbFrameworkUse)
            {
                case AppSettingConstant.DbFrameworkType.Nhibernate:
                    GetNHibernateDbConfig().BuildConfiguration();
                    break;

                case AppSettingConstant.DbFrameworkType.EntityFramework:
                    // Database.SetInitializer(new MyUniversityDbInitializer());
                    break;
            }
        }
    }
}