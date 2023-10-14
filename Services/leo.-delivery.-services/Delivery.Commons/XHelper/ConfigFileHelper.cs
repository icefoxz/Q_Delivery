
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Delivery.Commons.XHelper
{
    public class ConfigFileHelper
    {
        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T GetConfig<T>(string url) where T : class
        {
            if (string.IsNullOrWhiteSpace(url)) return default;

            var configJson = FileOperateHelper.ReadAllText(url, encoding: Encoding.UTF8).ToString();

            if (string.IsNullOrWhiteSpace(configJson)) return default;

            try
            {
                return JsonConvert.DeserializeObject<T>(configJson);
            }
            catch (Exception ex)
            { 
                return default(T);
            }
        }

        /// <summary>
        /// 设置配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool SetConfig<T>(T configObject, string url) where T : class
        {
            if (string.IsNullOrWhiteSpace(url)) return false;

            if (configObject == null) return false;

            var configJson = JsonConvert.SerializeObject(configObject);

            var jObject = JsonConvert.DeserializeObject<JObject>(configJson);

            try
            {
                FileOperateHelper.WriteJson(content: jObject, url: url);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
