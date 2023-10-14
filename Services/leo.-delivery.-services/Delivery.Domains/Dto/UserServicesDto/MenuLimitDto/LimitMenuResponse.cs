using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.UserServicesDto.MenuLimitDto
{
    public class LimitMenuResponse
    {
        /// <summary>
        /// 标识
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 所属权限组
        /// </summary>
        public Guid limit_Id { get; set; }

        /// <summary>
        /// 关联菜单
        /// </summary>
        public string menu_IdArray { get; set; }

        /// <summary>
        /// 所属权限组名称
        /// </summary>
        public string limit_Name { get; set; }

        /// <summary>
        /// 关联菜单
        /// </summary>
        public Guid menu_Id { get; set; }

        /// <summary>
        /// 关联菜单名称
        /// </summary>
        public string menu_Name { get; set; }

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

        /// <summary>
        /// 创建人
        /// </summary>
        public string? create_User { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? create_Time { get; set; }
    }
}
