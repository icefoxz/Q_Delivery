using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.SystemEntitys
{
    /// <summary>
    /// 系统字典表
    /// </summary>
    public class SystemDict : BaseEntity
    {
        /// <summary>
        /// 上级Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 字典Key
        /// </summary>
        public string dict_Key { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public string dict_Name { get; set; }

        /// <summary>
        /// 字典Value
        /// </summary>
        public string dict_Value { get; set; }

        /// <summary>
        /// 字典JsonValue
        /// </summary>
        public string? dict_JsonValue { get; set; }

        /// <summary>
        /// 是否是系统默认
        /// </summary>
        public bool? isSystemBuilt { get; set; }
    }
}
