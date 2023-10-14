using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.UserServicesDto.UserDto
{
    public class UserRequest
    {
        /// <summary>
        /// 登录名称
        /// </summary>
        public string? user_LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string? user_LoginPwd { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool isEnable { get; set; }

        /// <summary>
        /// 管辖单位
        /// </summary>
        public Guid? dept_Id { get; set; }

        /// <summary>
        /// 管辖单位数组
        /// </summary>
        public List<Guid?>? dept_IdArray { get; set; }

        /// <summary>
        /// 绑定的人员
        /// </summary>
        public Guid? person_Id { get; set; }

        /// <summary>
        /// 用户权限
        /// </summary>
        public Guid? limit_Id { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public new Guid Id { get; set; }
    }
}
