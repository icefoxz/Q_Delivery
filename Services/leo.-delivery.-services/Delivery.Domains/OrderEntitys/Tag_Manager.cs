using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.OrderEntitys
{
    public class Tag_Manager : BaseEntity
    {
        /// <summary>
        /// 标签Id
        /// </summary>
        public Guid? tag_Id { get; set; }

        /// <summary>
        /// 该所属类型对应的标签
        /// </summary>
        public Tag TagInfo { get; set; }

        /// <summary>
        /// 所属类型
        /// </summary>
        public string tag_Type { get; set; }

    }
}
