using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MyUniversity.Contracts.Constants;
using MyUniversity.Dal;
using MyUniversity.Dal.Mappings.NHibernate;
using NHibernate.Event;

namespace MyUniversity.ApiStart
{
    public class DatabaseInitialization
    {
        public static FluentConfiguration GetConfig()
        {
            return Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2012.ShowSql()
                        .ConnectionString(c => c.FromConnectionStringWithKey("MyUniversityDb")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CourseMapping>()
                    .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
                .ExposeConfiguration(c => c.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[] { new NHibernateCustomPreEventListener() })
                .ExposeConfiguration(c => c.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[] { new NHibernateCustomPreEventListener() })
                .ExposeConfiguration(c => c.EventListeners.PreDeleteEventListeners = new IPreDeleteEventListener[] { new NHibernateCustomPreEventListener() });
        }

        public static void Run(AppSettingConstant.DbFrameworkType dbFrameworkUse)
        {
            switch (dbFrameworkUse)
            {
                case AppSettingConstant.DbFrameworkType.Nhibernate:
                    GetConfig().BuildConfiguration();
                    break;

                case AppSettingConstant.DbFrameworkType.EntityFramework:
                    // Database.SetInitializer(new MyUniversityDbInitializer());
                    break;
            }
        }
    }
}