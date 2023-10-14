using Delivery.Commons.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.UserServicesDto.MenuDto
{
    public class MenuRequest
    {
        /// <summary>
        /// 标识Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string? menu_Name { get; set; }

        /// <summary>
        /// 是否是外链
        /// </summary>
        public bool isOuterJoin { get; set; }

        /// <summary>
        /// 外链地址
        /// </summary>
        public string? menu_Link { get; set; }

        ///// <summary>
        ///// 菜单类型（0-分组，1-功能）
        ///// </summary>
        //public MenuTypeEnum menu_Type { get; set; }

        /// <summary>
        /// 菜单路径
        /// </summary>
        public string? menu_Path { get; set; }

        #region 新增

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string? menu_Icon { get; set; }

        /// <summary>
        /// 菜单name
        /// </summary>
        public string? menu_FileName { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        public string? menu_ComponentName { get; set; }

        #endregion

        /// <summary>
        /// 所属平台
        /// </summary>
        public PlatformEnum? menu_PlatFrom { get; set; }

        /// <summary>
        /// 上级Id
        /// </summary>
        public Guid? menu_ParentId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public List<Guid>? IdArray { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? expand_Order { get; set; }
    }
}
