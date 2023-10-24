using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.UserEntitys
{
    public enum PersonTypes : byte
    {
        User = 0,
        Staff = 1,
        Rider = 2
    }

    /// <summary>
    /// 人员表
    /// </summary>
    public class Person : BaseEntity
    {
        /// <summary>
        /// 人员名称
        /// </summary>
        public string per_Name { get; set; }

        /// <summary>
        /// 人员全称
        /// </summary>
        public string? per_FullName { get; set; }

        /// <summary>
        /// 人员简称
        /// </summary>
        public string? per_SimpleName { get; set; }

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
        public Dept deptInfo { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public Guid? job_Id { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public Job jobInfo { get; set; }

        #region 预留拓展

        /// <summary>
        /// 人员类型-内部人员，骑手，普通用户
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

        /// <summary>
        /// 绑定该人员的用户
        /// </summary>
        public List<User> userInfos { get; set; } = new List<User>();
    }

    public static class PersonTypesExtension
    {
        public static PersonTypes GetPersonType(this Person person) => person.per_Type switch
        {
            "1" => PersonTypes.Staff,
            "2" => PersonTypes.Rider,
            _ => PersonTypes.User,
        };
        public static string GetPersonTypeTitle(this Person person)=> person.per_Type switch
        {
            "1" => "员工",
            "2" => "骑手",
            _ => "用户",
        };
        public static void SetPersonType(this Person person,int personType)=> person.per_Type = personType switch
        {
            1 => "1",
            2 => "2",
            _ => "0",
        };
        public static void SetPersonType(this Person person, PersonTypes personType) =>
            person.per_Type = personType switch
            {
                PersonTypes.Staff => "1",
                PersonTypes.Rider => "2",
                _ => "0",
            };
        public static int ToInt(this PersonTypes type)=> type switch
        {
            PersonTypes.Staff => 1,
            PersonTypes.Rider => 2,
            _ => 0,
        };

        public static bool Is(this PersonTypes type, string? typeString) => type switch
        {
            PersonTypes.Staff => typeString is "1",
            PersonTypes.Rider => typeString is "2",
            PersonTypes.User => typeString is "0",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        public static bool Is(this PersonTypes type, int typeInt) => type == (PersonTypes)typeInt;
    }
}
