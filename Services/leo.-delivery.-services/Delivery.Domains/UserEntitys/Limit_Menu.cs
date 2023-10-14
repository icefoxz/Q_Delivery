using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.UserEntitys
{
    /// <summary>
    /// 菜单权限关系表
    /// </summary>
    public class Limit_Menu : BaseEntity
    {
        /// <summary>
        /// 所属权限组
        /// </summary>
        public Guid limit_Id { get; set; }

        /// <summary>
        /// 所属权限组
        /// </summary>
        public Limit limitInfo { get; set; }


        /// <summary>
        /// 关联菜单
        /// </summary>
        public Guid menu_Id { get; set; }

        /// <summary>
        /// 关联菜单
        /// </summary>
        public string menu_IdArray { get; set; }

        /// <summary>
        /// 关联菜单
        /// </summary>
        public Menu menuInfo { get; set; }

        /// <summary>
        /// 增加权限
        /// </summary>
        public bool? limit_Add { get; set; }

        /// <summary>
        /// 删除权限
        /// </summary>
        public bool? limit_Del { get; set; }

        /// <summary>
        /// 编辑权限
        /// </summary>
        public bool? limit_Edit { get; set; }

        /// <summary>
        /// 查询权限
        /// </summary>
        public bool? limit_Query { get; set; }
    }
}
