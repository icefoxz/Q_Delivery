﻿

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
            //var dbConfig = ConfigFileHelper
            //    .GetConfig<CommonConfig>(Path.Combine(
            //        AppDomain.CurrentDomain.BaseDirectory,
            //        "config/systemConfig.json"
            //        CommonConfigConst.systemConfigUrl
            //            ));
            // 注册基础的 DbContext
            //     services.AddDbContext<BaseDbContext>(
            //options => options.UseSqlServer(config.dbConfig));

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
