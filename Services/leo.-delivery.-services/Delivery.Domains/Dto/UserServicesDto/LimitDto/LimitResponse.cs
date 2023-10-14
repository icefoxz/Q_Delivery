using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.UserServicesDto.LimitDto
{
    public class LimitResponse
    {
        /// <summary>
        /// 标识
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 权限组名称
        /// </summary>
        public string limit_Name { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? create_User { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string? create_Time { get; set; }


        /// <summary>
        /// 该权限组包含的所有菜单
        /// </summary>
        public List<LimitMenuResponse>? limit_Menus { get; set; }

        /// <summary>
        /// 权限组绑定了那些人
        /// </summary>
        public List<User>? userInfos { get; set; }
    }
}
