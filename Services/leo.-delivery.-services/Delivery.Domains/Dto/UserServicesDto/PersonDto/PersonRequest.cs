using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.UserServicesDto.PersonDto
{
    public class PersonRequest
    {
        public List<Guid?>? IdList { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 人员名称
        /// </summary>
        public string? per_Name { get; set; }

        /// <summary>
        /// 工作状态-字典配置
        /// </summary>
        public string? per_JobStatus { get; set; }

        /// <summary>
        /// 人员手机
        /// </summary>
        public string? per_Phone { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string? per_IdNo { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string? per_Birthday { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string? per_Address { get; set; }

        /// <summary>
        /// 政治面貌
        /// </summary>
        public string? per_Politics { get; set; }

        /// <summary>
        /// 所属单位
        /// </summary>
        public Guid? dept_Id { get; set; }

        /// <summary>
        /// 所属单位
        /// </summary>
        public List<Guid>? dept_IdArray { get; set; }

        #region 预留拓展

        /// <summary>
        /// 人员类型-（1-内部人员，2-骑手，3-普通用户）
        /// </summary>
        public string? per_Type { get; set; }

        /// <summary>
        /// 用户登录名称
        /// </summary>
        public string? per_UserName { get; set; }

        /// <summary>
        /// 用户登录密码
        /// </summary>
        public string? per_UserPwd { get; set; }

        /// <summary>
        /// 格式化电话号码
        /// </summary>
        public string? per_NormalizedPhone { get; set; }

        #endregion

    }
}
