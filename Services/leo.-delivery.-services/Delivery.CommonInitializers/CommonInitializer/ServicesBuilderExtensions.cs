

using Delivery.Commons.Attributes;
using Delivery.Commons.Enum;
using Delivery.Services.OrderServices;
using System.Reflection;

namespace Delivery.CommonInitializers.CommonInitializer
{
    /// <summary>
    /// 服务注册
    /// </summary>
    public static class ServicesBuilderExtensions
    {
        /// <summary>
        /// 自动注入所有的程序集有InjectAttribute标签
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IServiceCollection AddAutoDIServices(this IServiceCollection services)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var assemblies = Directory.GetFiles(path, "Delivery.Services.dll").Select(Assembly.LoadFrom).ToList();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes()
                    .Where(a => a.IsClass && a.GetCustomAttribute<AutoInjectAttribute>() != null)
                    .ToList();
                if (types.Count <= 0) continue;
                foreach (var type in types)
                {
                    var attr = type.GetCustomAttribute<AutoInjectAttribute>();
                    if (attr?.Type == null) continue;
                    switch (attr.InjectType)
                    {
                        case InjectTypeEnum.Scope:
                            services.AddScoped(attr.Type, type);
                            break;
                        case InjectTypeEnum.Single:
                            services.AddSingleton(attr.Type, type);
                            break;
                        case InjectTypeEnum.Transient:
                            services.AddTransient(attr.Type, type);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

            }
            return services;

        }

    }
}
