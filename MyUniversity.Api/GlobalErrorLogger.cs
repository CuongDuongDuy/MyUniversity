using System.Web.Http.ExceptionHandling;
using log4net;

namespace MyUniversity.Api
{
    public class GlobalErrorLogger : ExceptionLogger
    {
        readonly ILog log = LogManager.GetLogger("API");

        public override void Log(ExceptionLoggerContext context)
        {
            var exception = context.Exception;
            log.Fatal(exception.Message, exception);
        }
    }
}