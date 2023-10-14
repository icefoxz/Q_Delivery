using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Commons.Log4Error
{
    /// <summary>
    /// 异常类
    /// </summary>
    public class Log4Error
    {
        /// <summary>
        /// 
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RequestUri { get; set; }
         
        /// <summary>
        /// 上一路径
        /// </summary>
        public string Referrer { get; set; }

        /// <summary>
        /// 用户名记录
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ActionParameters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ResultMsg { get; set; }

        /// <summary>
        /// 客户机ip
        /// </summary>
        public string ClientIp { get; set; }

    }

}
