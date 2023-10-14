using Delivery.Commons.XHelper;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Diagnostics;
using System;
using NLog.Fluent;
using Delivery.Domains.SystemEntitys;
using Delivery.IServices.ISystemServices;

namespace Delivery.WebApi.Logs
{
    public class LogHandler : ILogHandler
    {
        private readonly ILogger _logger;
        private readonly ISystemLogServices _systemLogServices;

        public LogHandler(ILogger<LogHandler> logger, ISystemLogServices systemLogServices)
        {
            _systemLogServices = systemLogServices;
            _logger = logger;
        }

        public async Task LogAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var sw = new Stopwatch();
            sw.Start();
            var actionExecutedContext = await next();
            sw.Stop();

            //操作参数
            var args = JsonConvert.SerializeObject(context.ActionArguments);
            ////操作结果
            //var result1 = JsonConvert.SerializeObject(result?.Value) ?? "";

            try
            {
                var model = new SystemLog()
                {
                    log_Type = "INFO",
                    log_OptUser = "测试",
                    log_Params = args,
                    log_ApiMethod = context.HttpContext.Request.Method.ToLower() ?? "",
                    log_ApiPath = context.ActionDescriptor.AttributeRouteInfo.Template.ToLower() ?? "",
                    log_ElapsedMilliseconds = sw.ElapsedMilliseconds.ToString()
                };
                ObjectResult result = actionExecutedContext.Result as ObjectResult;
                //if (result != null)
                model.log_Message = JsonConvert.SerializeObject(result?.Value ?? "") ?? "";
                string ua = context.HttpContext.Request.Headers["User-Agent"];
                var client = UAParser.Parser.GetDefault().Parse(ua);
                var device = client.Device.Family;
                device = device.ToLower() == "other" ? "" : device;
                model.log_Browser = client.UA.Family;
                model.log_Os = client.OS.Family;
                model.log_Device = device;
                model.log_BrowserInfo = ua;
                model.log_OptIP = IPHelper.GetIP(context?.HttpContext?.Request);
                model.log_OptTime = DateTime.Now.ToString("u");
                await _systemLogServices.SystemLogAddAsync(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("操作日志插入异常：{@ex}", ex);
            }
        }
    }
}
