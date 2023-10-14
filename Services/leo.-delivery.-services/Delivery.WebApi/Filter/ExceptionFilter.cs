using Delivery.Commons.Cookie;
using Delivery.Commons.Log4Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Delivery.WebApi.Filter
{
    public class ExceptionFilter : ActionFilterAttribute, IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter>  _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext actionContext)
        {
            var result = JsonConvert.SerializeObject(
              actionContext?.Result?
              .GetType()?
            .GetProperties()?
              .FirstOrDefault(m => m.Name == "Value")?
            .GetValue(actionContext?.Result)
            );

            var parameters = ParmsUtil.GetParams(actionContext.HttpContext);
            //var remoteIpAddress = actionContext.HttpContext.Connection.RemoteIpAddress.ToString();
            var userName = UserInfoCookie.user_Name;

            //特殊处理JSON数据格式请求
            var error = new Log4Error
            {
                Method = actionContext.HttpContext.Request.Method,
                RequestUri = actionContext.HttpContext.Request.Path,
                //Referrer = ParmsUtil.GetRequestHeader("referer"),
                //ClientIp = remoteIpAddress,
                ActionParameters = parameters,
                UserName = userName,
                ResultMsg = result
            };

            _logger.LogError(JsonConvert.SerializeObject(error)+"\n"+ actionContext.Exception.ToString());

        }
    }
}
