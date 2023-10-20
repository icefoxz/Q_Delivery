

using Delivery.Commons.Attributes;
using Delivery.Commons.Config;
using Delivery.Commons.Enum;
using Delivery.Commons.XHelper;
using Delivery.EntityFramework.Core.DbContexts.BaseDbContexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Delivery.CommonInitializers.CommonInitializer
{
    /// <summary>
    /// 注册数据上下文
    /// </summary>
    public static class DbContextsBuilderExtensions
    {
        public static void AddContextServices(this IServiceCollection services,IConfiguration config)
        {

            // 注册其他具体的 DbContext
            services.AddDbContext<UserDbContext>(
                options => options.UseSqlServer(config.GetConnectionString("DbContext")));

            services.AddDbContext<OrderDbContext>(
                options => options.UseSqlServer(config.GetConnectionString("DbContext")));

            services.AddDbContext<SystemDbContext>(
                options => options.UseSqlServer(config.GetConnectionString("DbContext")));
        }

    }
}
