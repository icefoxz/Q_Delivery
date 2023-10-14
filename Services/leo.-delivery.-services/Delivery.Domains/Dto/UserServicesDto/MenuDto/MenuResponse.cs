using Delivery.Commons.Enum;
using Delivery.Domains.UserEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.UserServicesDto.MenuDto
{
    public class MenuResponse
    {
        public Guid Id { get; set; }
        public string Menu_TypeName { get; set; }
        public List<Menu> Menu_ChildList { get; set; }

        /// <summary>
        /// 是否是外链
        /// </summary>
        public bool isOuterJoin { get; set; }

        /// <summary>
        /// 外链地址
        /// </summary>
        public string? menu_Link { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string menu_Name { get; set; }

        /// <summary>
        /// 菜单全称
        /// </summary>
        public string menu_FullName { get; set; }

        /// <summary>
        /// 菜单简称
        /// </summary>
        public string menu_SimpleName { get; set; }

        /// <summary>
        /// 菜单类型（0-分组，1-功能）
        /// </summary>
        public int menu_TypeId { get; set; }

        ///// <summary>
        ///// 菜单类型（0-分组，1-功能）
        ///// </summary>
        //public string menu_Type { get; set; }

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
        /// 上级菜单名称
        /// </summary>
        public string? menu_ParentName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string? create_Time { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public string? create_User { get; set; }

        // 特殊
        /// <summary>
        /// 路由名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 路由路径
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 组件地址
        /// </summary>
        public string component { get; set; }

        /// <summary>
        /// 子级数据
        /// </summary>
        public List<MenuResponse> children { get; set; }

        public meta meta { get; set; }
        //name: "system",   路由名称 唯一的
        //  path: "/system",  路由路径
        //  redirect: "/system/logManage", 路由重定向地址不知道干嘛用的
        //  component: "/home/index",    组件地址

        /// <summary>
        /// 排序
        /// </summary>
        public int? expand_Order { get; set; }


    }
    public class meta {

        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 标题 菜单名
        /// </summary>
        public string title { get; set; }

        public bool isAffix { get; set; }
        public string isLink { get; set; }
        public bool isHide { get; set; }
        public bool isFull { get; set; }
        public bool isKeepAlive { get; set; }

        //isLink: "",
        //    isHide: false,
        //    isFull: false,
        //    isKeepAlive: true
        //icon: "Tickets",  菜单icon
        //      title: "系统配置", 菜单名
        //    isAffix: false,   菜单是否固定在标签页中
    }
}
