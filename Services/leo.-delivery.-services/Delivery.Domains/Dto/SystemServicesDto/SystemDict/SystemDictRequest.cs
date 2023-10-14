using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.SystemServicesDto.SystemDict
{
    public class SystemDictRequest
    {
        /// <summary>
        /// 标识
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 上级Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 字典Key
        /// </summary>
        public string? dict_Key { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public string? dict_Name { get; set; }

        /// <summary>
        /// 字典Value
        /// </summary>
        public string? dict_Value { get; set; }

        /// <summary>
        /// 字典JsonValue
        /// </summary>
        public string? dict_JsonValue { get; set; }

        /// <summary>
        /// 是否是系统默认
        /// </summary>
        public bool? isSystemBuilt { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? create_User { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? create_Time { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? expand_Order { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? expand_Desc { get; set; }

        /// <summary>
        /// 子项
        /// </summary>
        public List<SystemDictResponse?>? chilren { get; set; } = new List<SystemDictResponse>();

        /// <summary>
        /// 是否获取树
        /// </summary>
        public bool isTree { get; set; }
    }
}
