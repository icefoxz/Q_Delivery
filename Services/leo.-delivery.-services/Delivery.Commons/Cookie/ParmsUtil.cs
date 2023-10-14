
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Delivery.Commons.Cookie
{

    public class ParmsUtil
    {
        /// <summary>
        /// 从cookie中或者参数中获取parmsKey
        /// </summary>
        /// <returns></returns>
        public static string GetRequestParms(string parmsKey, Microsoft.AspNetCore.Http.HttpContext context = null, bool decrypt = false)
        {
            if (context == null) context = HttpContextUtil.Current;
            var parms = string.Empty;

            if (context?.Request == null) return decrypt ? EncryptUtil.DESDecrypt(parms) : parms;

            //获取url
            parms = context?.Request.Query[parmsKey].FirstOrDefault();
            if (!string.IsNullOrEmpty(parms)) return decrypt ? EncryptUtil.DESDecrypt(parms) : parms;

            //获取header
            parms = GetRequestHeader(parmsKey, context);
            if (!string.IsNullOrEmpty(parms)) return decrypt ? EncryptUtil.DESDecrypt(parms) : parms;

            var cookies = context?.Request.Cookies;
            if (cookies.Any() && cookies.ContainsKey(parmsKey)) parms = cookies[parmsKey];

            return decrypt ? EncryptUtil.DESDecrypt(parms) : parms; ;
        }

        /// <summary>
        /// 从header 中获取parmsKey
        /// </summary>
        /// <returns></returns>
        public static bool GetRequestParmsBool(string parmsKey, Microsoft.AspNetCore.Http.HttpContext context = null)
        {
            if (context == null) context = HttpContextUtil.Current;
            var parmBool = false;

            var parms = GetRequestParms(parmsKey, context);

            if (!string.IsNullOrEmpty(parms)) bool.TryParse(parms, out parmBool);

            return parmBool;
        }

        /// <summary>
        /// 从header 中获取parmsKey
        /// </summary>
        /// <returns></returns>
        public static string GetRequestHeader(string headerKey, Microsoft.AspNetCore.Http.HttpContext context = null)
        {
            if (context == null) context = HttpContextUtil.Current;
            var parms = string.Empty;

            var headers = context.Request.Headers;
            if (headers.ContainsKey(headerKey)) parms = headers[headerKey];
            if (!string.IsNullOrEmpty(parms)) return parms;

            return parms;
        }

        /// <summary>
        /// 从header 中获取parmsKey
        /// </summary>
        /// <returns></returns>
        public static bool GetRequestHeaderBool(string headerKey, Microsoft.AspNetCore.Http.HttpContext context = null)
        {
            if (context == null) context = HttpContextUtil.Current;
            var parmBool = false;

            var parms = GetRequestHeader(headerKey, context);

            if (!string.IsNullOrEmpty(parms)) bool.TryParse(parms, out parmBool);

            return parmBool;
        }

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetParams(Microsoft.AspNetCore.Http.HttpContext context = null)
        {
            if (context == null) context = HttpContextUtil.Current;

            try
            {
                var form = HttpUtility.ParseQueryString(context.Request.QueryString.ToString());

                var request = context.Request;

                if (request.Body.CanSeek) request.Body.Position = 0;

                string data = string.Empty;
                var parameters = new Dictionary<string, string>();
                switch (request.Method)
                {
                    case "POST":
                        data = new StreamReader(request.Body).ReadToEnd();
                        //请求参数只读一次，读取后释放，所以需要重新赋值body
                        request.Body = new MemoryStream(Encoding.GetEncoding("GB2312").GetBytes(data));

                        if (request.HasFormContentType && request.Form.Files.Any())
                        {
                            Dictionary<string, object> dict = new Dictionary<string, object>();
                            foreach (var key in request.Form.Keys)
                            {
                                dict.Add(key, request.Form[key]);
                            }
                            data = CommonTools.SerializeObject(dict);
                        }

                        break;
                    case "GET":
                        //第一步：取出所有get参数
                        for (int f = 0; f < form.Count; f++)
                        {
                            string key = form.Keys[f];
                            parameters.Add(key, form[key]);
                        }
                        data = CommonTools.SerializeObject(parameters);
                        break;
                    case "DELETE":
                        //第一步：取出所有get参数

                        for (int f = 0; f < form.Count; f++)
                        {
                            string key = form.Keys[f];
                            parameters.Add(key, form[key]);
                        }
                        data = CommonTools.SerializeObject(parameters);

                        break;
                    case "PUT":
                        data = new StreamReader(request.Body).ReadToEnd();
                        //请求参数只读一次，读取后释放，所以需要重新赋值body
                        request.Body = new MemoryStream(Encoding.GetEncoding("GB2312").GetBytes(data));

                        if (request.HasFormContentType && request.Form.Files.Any())
                        {
                            Dictionary<string, object> dict = new Dictionary<string, object>();
                            foreach (var key in request.Form.Keys)
                            {
                                dict.Add(key, request.Form[key]);
                            }
                            data = CommonTools.SerializeObject(dict);
                        }

                        break;
                    default:
                        data = string.Empty;
                        break;
                }
                return data;
            }
            catch
            {
                return string.Empty;
            }
        }

    }

}
