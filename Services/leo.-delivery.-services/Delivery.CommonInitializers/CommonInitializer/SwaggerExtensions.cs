
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.CommonInitializers.CommonInitializer
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerServices(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var ApiName = "Delivery.WebApi.Core";

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    // {ApiName} 定义成全局变量，方便修改
                    Version = "V1",
                    Title = $"{ApiName} 接口文档——Netcore 6.0",
                    Description = $"{ApiName} HTTP API V1",

                });
                var file = Path.Combine(AppContext.BaseDirectory, "Swagger.Core.xml");  // xml文档绝对路径
                var path = Path.Combine(AppContext.BaseDirectory, file); // xml文档绝对路径 
                c.IncludeXmlComments(file, true);
                c.OrderActionsBy(o => o.RelativePath);
            });

        }
    }
}
