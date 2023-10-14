using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.OrderEntitys
{
    public class Lingau_OptLog : BaseEntity
    {
        /// <summary>
        /// 操作人
        /// </summary>
        public string opt_User { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string opt_Type { get; set; }

        /// <summary>
        /// 被操作人
        /// </summary>
        public string opt_BeUser { get; set; }

        /// <summary>
        /// 操作前金额
        /// </summary>
        public decimal opt_BeginAmount { get; set; }

        /// <summary>
        /// 操作后金额
        /// </summary>
        public decimal opt_EndAmount { get; set; }

    }
}
