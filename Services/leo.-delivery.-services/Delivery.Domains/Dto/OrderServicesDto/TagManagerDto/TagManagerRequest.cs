using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.OrderServicesDto.TagManagerDto
{
    public class TagManagerRequest
    {
        public Guid? Id { get; set; }

        public List<Guid?>? IdList { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string? tag_Name { get; set; }

        /// <summary>
        /// 标签Id
        /// </summary>
        public Guid? tag_Id { get; set; }

        /// <summary>
        /// 是否展示全部
        /// </summary>
        public bool isAll { get; set; }

        /// <summary>
        /// 所属类型
        /// </summary>
        public TagOwningType? tag_Type { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string? expand_Desc { get; set; }
    }
}
