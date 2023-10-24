using Delivery.CommonInitializers.CommonInitializer.AutoMapper;
using Delivery.CommonInitializers.CommonInitializer;
using Delivery.CommonInitializers.CommonMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Delivery.Commons.Cookie;
using Delivery.Commons.JWTHelper;
using Delivery.WebApi.Filter;
using Delivery.WebApi.Logs;

namespace Delivery.WebApi
{
    public class Program
    {
        private const string CorsPolicyName = "AllowSpecificOrigin";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            TokenHelp.SetTokenSecret(config["AccessTokenKey"]);
            // Add services to the container.
            builder.Services.AddControllers();

            // Add NLogServices to the container
            builder.Services.AddNLogServices();

            // Add ContextServices to the container
            builder.Services.AddContextServices(config);

            // Add AutoDIServices to the container
            builder.Services.AddAutoDIServices();

            // Add AutoMapperServices to the container
            builder.Services.AddAutoMapperServices();

            // Add AutoMapperServices to the container
            builder.Services.AddSwaggerServices();

#if DEBUG
            Console.WriteLine($"骑手单位Id = {config.GetRiderDeptId()}");
            Console.WriteLine($"骑手权限Id = {config.GetRiderPermission()}");
            // azure 支持外设cors, 所以不需要配置
            // ConfigureCors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName,
                    b =>
                    {
                        var allowedOrigins = builder.Configuration
                            .GetSection("CORS:AllowedOrigins").Get<string[]>();
                        
                        b.WithOrigins(allowedOrigins)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
#endif

            //内存缓存
            builder.Services.AddMemoryCache();

            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //builder.Services.AddControllers(c=>c.Filters.Add<ApiAuthorize>());

            builder.Services.AddControllers(o =>
            {
                o.Filters.Add<ApiAuthorizeFilter>();
                o.Filters.Add<IgnoreAttributeFilter>();
            });

            // 日志过滤器
            builder.Services.AddScoped<ILogHandler, LogHandler>();


            builder.Services.AddControllers(options =>
            {
                ////添加全局异常过滤器
                //options.Filters.Add<GlobalExceptionsFilter>();
                //日志过滤器
                //options.Filters.Add<LogActionFilter>();
                options.Filters.Add<ExceptionFilter>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors(CorsPolicyName);

            // Use logging Middleware
            app.UseMiddleware<LoggingMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", "WebApi.Core V1");
                c.RoutePrefix = "ApiDoc";
            });

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}