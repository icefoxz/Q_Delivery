using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.UserServicesDto.LimitDto
{
    public class LimitRequest
    {
        /// <summary>
        /// 标识
        /// </summary>
        public Guid Id { get; set; }

        public List<Guid?>? IdList { get; set; }

        /// <summary>
        /// 权限组名称
        /// </summary>
        public string? limit_Name { get; set; }

        /// <summary>
        /// 菜单Id，数组
        /// </summary>
        public List<menuItem?>? menu_List { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? create_User { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? create_Time { get; set; }

    }

    public class menuItem
    {
        public Guid menu_Id { get; set; }

        public List<Guid> menu_IdArray { get; set; }

    }
}
