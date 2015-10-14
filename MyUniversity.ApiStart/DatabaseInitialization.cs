using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MyUniversity.Contracts.Constants;
using MyUniversity.Dal.Mappings.NHibernate;

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
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<StudentProfileMapping>());
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