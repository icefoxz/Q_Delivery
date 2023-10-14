using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.OrderEntitys
{
    public class Tag : BaseEntity
    {
        /// <summary>
        /// 订单标签名称
        /// </summary>
        public string tag_Name { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool isEnable { get; set; }

        ///// <summary>
        ///// 该标签有哪些订单
        ///// </summary>
        //public List<Order_Tag> order_TagInfos { get; set; }

        /// <summary>
        /// 该标签所属的类型
        /// </summary>
        public List<Tag_Manager> tag_ManagerList { get; set; }
    }
}
