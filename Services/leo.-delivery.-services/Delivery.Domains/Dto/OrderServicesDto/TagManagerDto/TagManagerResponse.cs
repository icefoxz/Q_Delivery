using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.OrderServicesDto.TagManagerDto
{
    public class TagManagerResponse : TagManagerRequest
    {

        /// <summary>
        /// 标签名称
        /// </summary>
        public string? tag_Name { get; set; }

        /// <summary>
        /// 标签状态
        /// </summary>
        public bool isEnable { get; set; }

        /// <summary>
        /// 所属类型
        /// </summary>
        public string? tag_TypeName { get; set; }

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
