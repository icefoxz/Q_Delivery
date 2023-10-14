using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.UserServicesDto.PersonDto
{
    public class PersonResponse:Person
    {

        /// <summary>
        /// 工作状态-字典配置
        /// </summary>
        public string? per_JobStatusId { get; set; }

        /// <summary>
        /// 人员类型
        /// </summary>
        public string? per_TypeId { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string dept_Name { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string job_Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_Time { get; set; }
    }
}
