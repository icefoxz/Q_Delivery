
using Delivery.Commons.Config;
using Delivery.Commons.Cookie;
using Delivery.Commons.JWTHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Delivery.WebApi.Filter
{
    public class ApiAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IMemoryCache _memoryCache;

        public ApiAuthorizeFilter(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //1.判断是否需要校验
            var isDefined = false;
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                isDefined = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                   .Any(a => a.GetType().Equals(typeof(IgnoreAttributeFilter)));
            }

            if (isDefined)
            {
                return;
            }

            var token = context.HttpContext.Request.Headers["Authorization"].ToString();    //ajax请求传过来
            string pattern = "^Bearer (.*?)$";
          
            if (!Regex.IsMatch(token, pattern))
            {
                context.Result = new ContentResult { StatusCode = 401, Content = "token不能为空或格式不对!格式为:Bearer {token}" };
                return;
            }
            token = Regex.Match(token, pattern).Groups[1]?.ToString();
            var res = _memoryCache.Get(token);
            if (res != null)
            {
                var obj = JsonConvert.DeserializeObject<BaseQuery>(res.ToString());
                UserInfoCookie.limit_Id = obj.limit_Id;
                UserInfoCookie.user_Id = obj.user_Id;
                UserInfoCookie.user_Name = obj.user_Name;
                UserInfoCookie.login_Name = obj.login_Name;
                UserInfoCookie.dept_Id = obj.dept_Id;
                UserInfoCookie.dept_Name = obj.dept_Name;
            }
            if (token == "null" || string.IsNullOrEmpty(token))
            {
                context.Result = new ContentResult { StatusCode = 401, Content = "token不能为空" };
                return;
            }
            //校验auth的正确性
            var result = JWTHelp.JWTJieM(token, ConfigHelp.GetString("AccessTokenKey"));

            if (result == "expired")
            {
                context.Result = new ContentResult { StatusCode = 401, Content = "expired" };
                return;
            }
            else if (result == "invalid")
            {
                context.Result = new ContentResult { StatusCode = 401, Content = "invalid" };
                return;
            }
            else if (result == "error")
            {
                context.Result = new ContentResult { StatusCode = 401, Content = "error" };
                return;
            }
            else
            {
                //UserCookie.msg = result;
                //表示校验通过,用于向控制器中传值
                context.RouteData.Values.Add("auth", result);
            }
        }

        /// <summary>
        /// 判断该请求是否是ajax请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool IsAjaxRequest(HttpRequest request)
        {
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }
    }
}
