namespace MyUniversity.Contracts.Constants
{
    public class AppSettingConstant
    {
        public enum DbFrameworkType
        {
            Nhibernate,
            EntityFramework
        }

        public static readonly string DbFramework = "DbFrameworkType";

    }
}
