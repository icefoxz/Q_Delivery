using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.OrderServicesDto.OrderTagOrReportDto
{
    public class OrderTagOrReportResponse : OrderTagOrReportRequest
    {

        /// <summary>
        /// 订单名称
        /// </summary>
        public string? order_Name { get; set; }

        /// <summary>
        /// 标签或报告名称
        /// </summary>
        public string? tag_Name { get; set; }


        /// <summary>
        /// 所属类型
        /// </summary>
        public string? tag_TypeName { get; set; }

        /// <summary>
        /// 摘要说明
        /// </summary>
        public string? expand_Desc { get; set; }
    }
}
