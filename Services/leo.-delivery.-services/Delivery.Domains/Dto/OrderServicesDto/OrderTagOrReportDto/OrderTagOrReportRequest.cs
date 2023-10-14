using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.OrderServicesDto.OrderTagOrReportDto
{
    public class OrderTagOrReportRequest:BasePageEntity
    {
        /// <summary>
        /// 标识
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 标签说明
        /// </summary>
        public string? order_TagOrReport_Name { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool? isEnable { get; set; }

        /// <summary>
        /// 数据所属类型
        /// </summary>
        public TagOwningType? tag_Type { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public Guid order_Id { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public List<Guid>? order_IdArray { get; set; }

        /// <summary>
        /// 标签Id
        /// </summary>
        public Guid? tagManager_Id { get; set; }

        /// <summary>
        /// 标签Id
        /// </summary>
        public List<Guid?>? tagManager_IdList { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? create_User { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? create_Time { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? expand_Desc { get; set; }

    }
}
