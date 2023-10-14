using Delivery.Commons.Cookie;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Delivery.CommonInitializers.CommonMiddleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //    HttpContextUtil.str = "123";
                //var request = context.Request;

                //// 记录方法请求的参数
                //var requestParams = context.Request.Query;
                ////_logger.LogInformation("请求参数: {@RequestParams}", requestParams);
                ////_logger.LogDebug("请求参数: {@RequestParams}", requestParams);

                //// 记录电脑IP
                //var remoteIpAddress = context.Connection.RemoteIpAddress;
                //var localPort = context.Connection.LocalPort;
                ////_logger.LogInformation("IP地址: {RemoteIpAddress}", remoteIpAddress+":"+ localPort);

                //// 执行下一个中间件
                //await _next(context);

                //// 记录方法返回结果
                //var responseStatusCode = context.Response.StatusCode;
                ////_logger.LogInformation($"接口请求日志：电脑Ip: {remoteIpAddress}:{localPort},请求结果状态码：{responseStatusCode}");

                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

    }
}
