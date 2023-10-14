using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.OrderServicesDto.Lingau_OptLogDto
{
    public class LingauOptLogRequest
    {
        /// <summary>
        /// 标识
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string? opt_User { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string? opt_Type { get; set; }

        /// <summary>
        /// 被操作人
        /// </summary>
        public string? opt_BeUser { get; set; }

        /// <summary>
        /// 操作前金额
        /// </summary>
        public decimal? opt_BeginAmount { get; set; }

        /// <summary>
        /// 操作后金额
        /// </summary>
        public decimal? opt_EndAmount { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? create_User { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? create_Time { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? begin_Time { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? end_Time { get; set; }
    }
}
