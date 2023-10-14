using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.UserEntitys
{
    /// <summary>
    /// 登录用户表
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// 登录名称
        /// </summary>
        public string user_LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string user_LoginPwd { get; set; }

        /// <summary>
        /// 登录密码密文
        /// </summary>
        public string user_LoginPwdCipher { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool isEnable { get; set; }

        /// <summary>
        /// 管辖单位
        /// </summary>
        public Guid? dept_Id { get; set; }

        /// <summary>
        /// 管辖单位
        /// </summary>
        public Dept deptInfo { get; set; }

        /// <summary>
        /// 绑定的人员
        /// </summary>
        public Guid? person_Id { get; set; }

        /// <summary>
        /// 绑定人员
        /// </summary>
        public Person personInfo { get; set; }

        /// <summary>
        /// 用户权限
        /// </summary>
        public Guid? limit_Id { get; set; }

        /// <summary>
        ///  用户权限
        /// </summary>
        public Limit limitInfo { get; set; }

    }
}
