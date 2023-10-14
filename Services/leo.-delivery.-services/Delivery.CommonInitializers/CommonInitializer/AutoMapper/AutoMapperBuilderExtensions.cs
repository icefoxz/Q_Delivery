using Delivery.Commons.Attributes;
using Delivery.Commons.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Delivery.CommonInitializers.CommonInitializer.AutoMapper
{
    public static class AutoMapperBuilderExtensions
    {
        /// <summary>
        /// 注册AutoMapper
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapperServices(this IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAutoMapper(typeof(MapperProfile));

            return services;
        }
    }
}
