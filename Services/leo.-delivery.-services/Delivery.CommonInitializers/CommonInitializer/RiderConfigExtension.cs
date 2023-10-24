using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Delivery.CommonInitializers.CommonInitializer
{
    public static class RiderConfigExtension
    {
        public static Guid GetRiderDeptId(this IConfiguration config) => new(config["Rider:Dept"] ?? throw new NullReferenceException("找不到 Rider Dept 配置!"));
        public static Guid GetRiderPermission(this IConfiguration config) => new(config["Rider:Permission"]??throw new NullReferenceException("找不到 Rider Permission 配置!"));
    }
}
