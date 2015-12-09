using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MyUniversity.Api.Controllers
{
    public class BaseController : ApiController
    {

        protected Dictionary<string, string> QueryDictionary;

        protected IEnumerable<string> QueryExpand()
        {
            return GetQueryValue("$expand");
        }

        private IEnumerable<string> GetQueryValue(string key)
        {
            string value;
            var found = QueryDictionary.TryGetValue(key, out value);
            return found ? (string.IsNullOrEmpty(value) ? null : value.Split(',')) : null;
        }

        public BaseController()
        {
            QueryDictionary = HttpContext.Current.Request.QueryString.Keys.Cast<string>()
                .ToDictionary(key => key, value => HttpContext.Current.Request.QueryString[value]);
        }
    }

}
