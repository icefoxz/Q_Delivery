using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.OrderEntitys
{
    /// <summary>
    /// 订单标签表
    /// </summary>
    public class Order_TagOrReport : BaseEntity
    {
        /// <summary>
        /// 标签说明
        /// </summary>
        public string order_TagOrReport_Name { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool isEnable { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public Guid order_Id { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public Order orderInfo { get; set; }

        /// <summary>
        /// 标签Id
        /// </summary>
        public Guid tagManager_Id { get; set; }

        ///// <summary>
        ///// 标签
        ///// </summary>
        //public Tag_Manager tagManagerInfo { get; set; }

        /// <summary>
        /// 所属类型 Tag = 1 ,// 打标签 TagReport= 2 ,// 报告
        /// </summary>
        public string tag_Type { get; set; }
    }
}
