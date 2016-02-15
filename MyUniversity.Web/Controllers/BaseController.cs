using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using log4net;
using MyUniversity.Contracts.Services;
using Newtonsoft.Json;

namespace MyUniversity.Web.Controllers
{
    public class BaseController : Controller
    {
        protected HttpClient Client { get; set; }
        private static readonly ILog Log = LogManager.GetLogger("Web");

        public BaseController()
        {
            Client = new HttpClient { BaseAddress = new Uri(WebConfigurationManager.AppSettings.Get("ApiBaseAddress")) };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Add("contentType", "application/json; charset=utf-8");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TResult> GetHttpResponMessageResultAsyc<TResult>(string requestUri, params string[] includes)
        {
            var response = await Client.GetAsync(GetFullRequestUri(requestUri, includes));
            var result = response.IsSuccessStatusCode
                ? await response.Content.ReadAsAsync<TResult>()
                : default(TResult);
            return result;
        }

        public async Task<string> PostJsonAsyc(string requestUri, object value)
        {
            var response = await Client.PostAsJsonAsync(requestUri, value);
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result.Replace("\"", string.Empty);
            }
            throw new HttpException((int)response.StatusCode, "HttpException from API");
        }

        public async Task<ModificationServiceResult> PutJsonAsyc(string requestUri, object value)
        {
            var response = await Client.PutAsJsonAsync(requestUri, value);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ModificationServiceResult>(response.Content.ReadAsStringAsync().Result);
            }
            throw new HttpException((int)response.StatusCode, "HttpException from API");
        }

        public async Task<ModificationServiceResult> DeleteJsonAsyc(string requestUri)
        {
            var response = await Client.DeleteAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ModificationServiceResult>(response.Content.ReadAsStringAsync().Result);
            }
            throw new HttpException((int)response.StatusCode, "HttpException from API");
        }

        private string GetFullRequestUri(string requestUri, params string[] includes)
        {
            var result = requestUri;
            if (!includes.Any()) return result;
            var includesString = includes.Aggregate(string.Empty, (current, include) => current + (include + ','));
            result = result + "?$expand=" + includesString.Substring(0, includesString.Length - 1);
            return result;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Log.Error(filterContext.Exception.Message, filterContext.Exception);
            filterContext.ExceptionHandled = true;
            if (filterContext.Exception is HttpException)
            {
                var httpExceoption = filterContext.Exception as HttpException;
                switch (httpExceoption.GetHttpCode())
                {
                    case 404:
                        filterContext.Result = new RedirectResult("~/Error/NotFound");
                        break;
                    case 400:
                        filterContext.Result = new RedirectResult("~/Error/BadRequest");
                        break;
                    default:
                        filterContext.Result = new RedirectResult("~/Error/Index");
                        break;
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Error/Index");
            }
        }
    }
}