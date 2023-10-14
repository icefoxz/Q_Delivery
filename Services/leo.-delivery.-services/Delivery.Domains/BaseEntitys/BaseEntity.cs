using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.BaseEntitys
{
    public class BaseEntity
    {
        /// <summary>
        /// 标识
        /// </summary>
        public Guid Id { get;  set; } = Guid.NewGuid();
        //public long Id { get; set; }
        //public Guid sys_Id { get; set; }
        
        /// <summary>
        /// 创建人
        /// </summary>
        public string? create_User { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? create_Time { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string? update_User { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? update_Time { get; set; }

        /// <summary>
        /// 删除人
        /// </summary>
        public string? del_User { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? del_Time { get; set; }

        /// <summary>
        /// 删除状态
        /// </summary>
        public bool? del_Status { get; set; }

        /// <summary>
        /// 摘要说明
        /// </summary>
        public string? expand_Desc { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? expand_Order { get; set; }
    }
}