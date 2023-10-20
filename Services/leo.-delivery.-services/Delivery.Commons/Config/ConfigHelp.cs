using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Commons.Config
{
    public class ConfigHelp
    {
        /// <summary>
        /// 读取Json类型的配置文件
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="FilePath">文件路径，默认为：appsettings.json</param>
        /// <returns></returns>
        public static string GetString(string key, string FilePath = "appsettings.json")
        {
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(FilePath, optional: true, reloadOnChange: true);
            var configuration = configurationBuilder.Build();
            return configuration[key];
        }
    }
}
