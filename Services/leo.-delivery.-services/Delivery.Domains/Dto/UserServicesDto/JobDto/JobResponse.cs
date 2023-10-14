using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.UserServicesDto.JobDto
{
    /// <summary>
    /// 职位列表请求入参
    /// </summary>
    public class JobResponse
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string job_Name { get; set; }

        /// <summary>
        /// 职位所属单位
        /// </summary>
        public Guid? dept_Id { get; set; }

        /// <summary>
        /// 职位所属单位
        /// </summary>
        public string dept_Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string? create_Time { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public string? create_User { get; set; }
    }
}
