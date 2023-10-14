using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.SystemEntitys
{
    /// <summary>
    /// 日志系统表
    /// </summary>
    public class SystemLog : BaseEntity
    {
        /// <summary>
        /// 操作Ip
        /// </summary>
        public string? log_OptIP { get; set; }

        /// <summary>
        /// 操作人 
        /// </summary>
        public string? log_OptUser { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public string? log_Type { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string? log_Message { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public string log_OptTime { get; set; }

        // 新增

        /// <summary>
		/// 操作参数
		/// </summary>
        public string? log_Params { get; set; }

        /// <summary>
        /// 提交方式
        /// </summary>
        public string? log_ApiMethod { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        public string? log_ApiPath { get; set; }

        /// <summary>
        /// 耗时（毫秒）
        /// </summary>
        public string? log_ElapsedMilliseconds { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        public string? log_Browser { get; set; }

        /// <summary>
        /// 操作系统
        /// </summary>
        public string? log_Os { get; set; }

        /// <summary>
        /// 设备
        /// </summary>
        public string? log_Device { get; set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string? log_BrowserInfo { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int? log_OptPort { get; set; }

    }
}
