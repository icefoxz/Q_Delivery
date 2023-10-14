using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.SystemServicesDto.SystemLog
{
    public class SystemLogRequest:BasePageEntity
    {

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
        public string? log_OptTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? begin_Time { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? end_Time { get; set; }


        /// <summary>
        /// 提交方式
        /// </summary>
        public string? log_ApiMethod { get; set; }

        /// <summary>
        /// 操作Ip
        /// </summary>
        public string? log_OptIP { get; set; }


    }
}
