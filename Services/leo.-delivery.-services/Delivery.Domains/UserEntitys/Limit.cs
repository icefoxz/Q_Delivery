using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.UserEntitys
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Limit : BaseEntity
    {
        /// <summary>
        /// 权限组名称
        /// </summary>
        public string limit_Name { get; set; }

        /// <summary>
        /// 该权限组包含的所有菜单
        /// </summary>
        public List<Limit_Menu> limit_Menus { get; set; } = new List<Limit_Menu>();

        /// <summary>
        /// 权限组绑定了那些人
        /// </summary>
        public List<User> userInfos { get; set; } = new List<User>();
    }
}
