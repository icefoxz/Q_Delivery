using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.OrderServicesDto.OrderDto
{
    public class OrderResponse: OrderRequest
    {
        /// <summary>
        /// 订单标签
        /// </summary>
        public List<string> orderTagNameList { get; set; }

        /// <summary>
        /// 订单标签关系Id
        /// </summary>
        public List<Guid>? orderTagManagerIdList { get; set; }

        /// <summary>
        /// 订单标签
        /// </summary>
        public List<string> orderTagReportNameList { get; set; }

        /// <summary>
        /// 订单标签关系Id
        /// </summary>
        public List<Guid>? orderTagReportManagerIdList { get; set; }

        /// <summary>
        /// 订单标签
        /// </summary>
        public List<Order_TagOrReport> orderTagList { get; set; }

        /// <summary>
        /// 文件路径集合
        /// </summary>
        public List<imgUrl> fileUrlList { get; set; }

        /// <summary>
        /// 订单报告
        /// </summary>
        public List<Order_TagOrReport> orderTagReportList { get; set; }
    }
    public class imgUrl {

        public Guid fileId { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }

    }
}
