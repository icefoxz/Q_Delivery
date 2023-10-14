using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.OrderServicesDto.TagDto
{
    public class TagRequest
    {
        /// <summary>
        /// 标识
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public List<Guid?>? IdList { get; set; }

        /// <summary>
        /// 订单标签名称
        /// </summary>
        public string? tag_Name { get; set; }

        /// <summary>
        /// 是否全部
        /// </summary>
        public bool isAll { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool isEnable{ get; set; }

        /// <summary>
        /// 摘要说明
        /// </summary>
        public string? expand_Desc { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public string? create_User { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string? create_Time { get; set; }
    }
}
