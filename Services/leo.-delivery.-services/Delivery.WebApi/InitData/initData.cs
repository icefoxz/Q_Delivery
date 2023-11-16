using Delivery.Domains.Dto.SystemServicesDto.SystemDict;
using Delivery.Domains.Dto.UserServicesDto.UserDto;
using System.Security;

namespace Delivery.WebApi.InitData
{
    public class initData
    {
        /// <summary>
        /// 单位信息
        /// </summary>
        public List<Dept> DeptList { get; set; }

        /// <summary>
        /// 职位信息
        /// </summary>
        public List<JobResponse> JobList { get; set; }

        /// <summary>
        /// 人员信息
        /// </summary>
        public List<PersonResponse> PersonList { get; set; }

        /// <summary>
        /// 权限组信息
        /// </summary>
        public List<Limit> LimitList { get; set; }

        /// <summary>
        /// 菜单信息
        /// </summary>
        public List<MenuResponse> MenuList { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public List<UserResponse> UserList { get; set; }

        /// <summary>
        /// 字典信息
        /// </summary>
        public List<SystemDictResponse> DictList { get; set; }

    }
    // 默认骑手
    public class Rider {
        public Guid Dept { get; set; }

        public Guid Permission { get; set; }
    };

}
