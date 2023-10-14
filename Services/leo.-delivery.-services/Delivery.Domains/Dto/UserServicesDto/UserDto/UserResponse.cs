using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.UserServicesDto.UserDto
{
    public class UserResponse:User
    {
        /// <summary>
        /// 管辖单位名称
        /// </summary>
        public string dept_Name { get; set; }

        /// <summary>
        /// 人员名称
        /// </summary>
        public string person_Name { get; set; }

        /// <summary>
        /// 角色权限
        /// </summary>
        public string limit_Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_Time { get; set; }
    }
}
