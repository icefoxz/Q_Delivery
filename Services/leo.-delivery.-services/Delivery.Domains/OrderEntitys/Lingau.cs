using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.OrderEntitys
{
    public class Lingau : BaseEntity
    {
        /// <summary>
        /// 令凹币余额
        /// </summary>
        public decimal lingau_Balance { get; set; }

        /// <summary>
        /// 所属人员
        /// </summary>
        public Guid person_Id { get; set; }
    }
}
