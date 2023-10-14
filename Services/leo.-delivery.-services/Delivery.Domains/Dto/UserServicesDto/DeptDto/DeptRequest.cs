using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.UserServicesDto.DeptDto
{
    /// <summary>
    /// 单位列表请求入参
    /// </summary>
    public class DeptRequest
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// IdArray
        /// </summary>
        public List<Guid?>? IdList { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string? dept_Name { get; set; }

        /// <summary>
        /// 单位父级Id
        /// </summary>
        public Guid? dept_ParentId { get; set; }

        /// <summary>
        /// 单位父级Id
        /// </summary>
        public string? dept_ParentName { get; set; }

        /// <summary>
        /// 单位手机
        /// </summary>
        public string? dept_Phone { get; set; }

        /// <summary>
        /// 单位邮编
        /// </summary>
        public string? dept_PostCode { get; set; }

        /// <summary>
        /// 单位地址
        /// </summary>
        public string? dept_Address { get; set; }

      
    }
}
