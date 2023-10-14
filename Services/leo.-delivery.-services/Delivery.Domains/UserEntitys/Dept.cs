using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.UserEntitys
{
    /// <summary>
    /// 单位表
    /// </summary>
    public class Dept : BaseEntity
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public string dept_Name { get; set; }

        /// <summary>
        /// 单位名称全拼
        /// </summary>
        public string? dept_FullName { get; set; }

        /// <summary>
        /// 单位名称简拼
        /// </summary>
        public string? dept_SimpleName { get; set; }

        ///// <summary>
        ///// 单位层级
        ///// </summary>
        //public string? dept_Level { get; set; }

        /// <summary>
        /// 单位父级Id
        /// </summary>
        public Guid? dept_ParentId { get; set; }

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
        /// 该单位的所有人员
        /// </summary>
        public List<Person> personInfos { get; set; } = new List<Person>();

        /// <summary>
        /// 一个单位下含有多个职位，与此同时，选择某个单位，只能选择这个单位下的职位
        /// </summary>
        public List<Job> jobInfos { get; set; } = new List<Job>();

        /// <summary>
        /// 该管辖单位绑定了那些用户
        /// </summary>
        public List<User> userInfos { get; set; } = new List<User>();

        public Dept() { }

        public Dept(string dept_Name, string? dept_FullName, string? dept_SimpleName, string? dept_Level, Guid? dept_ParentId, string? dept_Phone, string? dept_PostCode, string? dept_Address, List<Person>? personInfos, List<Job>? jobInfos, List<User>? userInfos)
        {
            this.dept_Name = dept_Name ?? throw new ArgumentNullException(nameof(dept_Name));
            this.dept_FullName = dept_FullName;
            this.dept_SimpleName = dept_SimpleName;
            //this.dept_Level = dept_Level;
            this.dept_ParentId = dept_ParentId;
            this.dept_Phone = dept_Phone;
            this.dept_PostCode = dept_PostCode;
            this.dept_Address = dept_Address;
            this.personInfos = personInfos ?? throw new ArgumentNullException(nameof(personInfos));
            this.jobInfos = jobInfos ?? throw new ArgumentNullException(nameof(jobInfos));
            this.userInfos = userInfos ?? throw new ArgumentNullException(nameof(userInfos));
        }

        public Dept(string dept_Name, string? dept_FullName, string? dept_SimpleName, string dept_Phone)
        {
            this.dept_Name = dept_Name ?? throw new ArgumentNullException(nameof(dept_Name));
            this.dept_FullName = dept_FullName;
            this.dept_SimpleName = dept_SimpleName;
            this.dept_Phone = dept_Phone;
        }
    }
}
