using System.Web.Configuration;
using MyUniversity.Contracts.Constants;

namespace MyUniversity.Api.Helpers
{
    public class AppSettingHelper
    {
        public static AppSettingConstant.DbFrameworkType GetDbFrameworkType()
        {
            var value = WebConfigurationManager.AppSettings[AppSettingConstant.DbFramework];
            switch (value)
            {
                case "EntityFramework":
                    return AppSettingConstant.DbFrameworkType.EntityFramework;
                    break;
                case "NHibernate":
                    return AppSettingConstant.DbFrameworkType.Nhibernate;
                    break;
            }
            return AppSettingConstant.DbFrameworkType.EntityFramework;
        }
    }
}