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
    public class DeptResponse
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string dept_Name { get; set; }

        ///// <summary>
        ///// 单位名称全拼
        ///// </summary>
        //public string? dept_FullName { get; set; }

        ///// <summary>
        ///// 单位名称简拼
        ///// </summary>
        //public string? dept_SimpleName { get; set; }

        ///// <summary>
        ///// 单位层级
        ///// </summary>
        //public string? dept_Level { get; set; }

        /// <summary>
        /// 单位父级Id
        /// </summary>
        public Guid? dept_ParentId { get; set; }

        /// <summary>
        /// 单位父级姓名
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

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? create_Time { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public string? create_User { get; set; }

        /// <summary>
        /// 子级集合
        /// </summary>
        public List<DeptResponse> children { get; set; }
    }
}
