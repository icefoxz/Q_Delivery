using Delivery.Domains.Dto.UserServicesDto.MenuDto;
using Delivery.Domains.UserEntitys;
using System;
using System.ComponentModel;
using System.IO;

namespace Delivery.Domains.Dto.Vo
{
    public static class EntitysVo
    {
        public static List<MenuResponse> toVo(this List<Menu> menus, bool isLevel = true)
        {

            var result = new List<MenuResponse>();
            if (isLevel)
            {
                //var menuTypeList = menus.Where(item => item.menu_Type == 0).ToList();
                var menuTypeList = menus.ToList();
                result = menuTypeList.Select(type =>
                {

                    // 判断当前子级是否是分类，如果是递归获取子级
                    return new MenuResponse()
                    {
                        Id = type.Id,
                        Menu_TypeName = type.menu_Name,
                        Menu_ChildList = menus.Where(child => child.menu_ParentId == type.Id).ToList()
                    };
                }).ToList();
            }
            else
                //result = menus.Where(item => item.menu_Type == 0).Select(type => new MenuResponse()
                result = menus.Select(type => new MenuResponse()
                {
                    Id = type.Id,
                    //Menu_TypeName = type.menu_Type == 0 ? $"分组：{type.menu_Name}" : $"功能：{type.menu_Name}",
                    //menu_FullName= type.menu_FullName,
                    //menu_SimpleName=type.menu_SimpleName,
                    //menu_Type=type.menu_Type.ToString(),
                    //m
                }).ToList();
            return result;
        }


        #region 单位信息

        /// <summary>
        /// 单位实体转请求
        /// </summary>
        /// <param name="depts"></param>
        /// <returns></returns>
        public static IEnumerable<DeptResponse> toVo(this IEnumerable<Dept> depts, IEnumerable<Dept> parentDepts = null)
        {
            return depts?.Select(dept =>
            {
                return new DeptResponse()
                {
                    Id = dept.Id,
                    dept_Name = dept.dept_Name,
                    dept_ParentId = dept.dept_ParentId,
                    dept_ParentName = parentDepts?.FirstOrDefault(parent => parent.Id == dept.dept_ParentId)?.dept_Name ?? "",
                    dept_Address = dept.dept_Address,
                    dept_Phone = dept.dept_Phone,
                    dept_PostCode = dept.dept_PostCode,
                    create_Time = dept.create_Time,
                    create_User = dept.create_User,
                };
            }) ?? new List<DeptResponse>();

        }

        #endregion

        #region 职位信息

        /// <summary>
        /// 职位实体转请求
        /// </summary>
        /// <param name="depts"></param>
        /// <returns></returns>
        public static IEnumerable<JobResponse> toVo(this List<Job> jobs, IEnumerable<DeptResponse> depts)
        {
            return jobs?.Select(job =>
            {
                return new JobResponse()
                {
                    Id = job.Id,
                    job_Name = job.job_Name,
                    dept_Id = job.dept_Id,
                    dept_Name = depts?.FirstOrDefault(parent => parent.Id == job.dept_Id)?.dept_Name,
                    create_Time = job.create_Time.ObjToDate().ToString("yyyy-MM-dd hh:mm"),
                };
            }) ?? new List<JobResponse>();

        }

        #endregion

        #region 菜单信息

        /// <summary>
        /// 菜单实体转请求
        /// </summary>
        /// <param name="depts"></param>
        /// <returns></returns>
        public static IEnumerable<MenuResponse> toVo(this IEnumerable<Menu> menus)
        {
            return menus?.Select(menu =>
            {
                return new MenuResponse()
                {
                    Id = menu.Id,
                    menu_Name = menu.menu_Name,
                    menu_FullName = menu.menu_FullName,
                    menu_SimpleName = menu.menu_SimpleName,
                    menu_ParentId = menu.menu_ParentId,
                    menu_ParentName = menus.FirstOrDefault(item => item.Id == menu.menu_ParentId)?.menu_Name,
                    menu_Path = menu.menu_Path,
                    // 新增
                    menu_Icon = menu.menu_Icon,
                    menu_FileName = menu.menu_FileName,
                    menu_ComponentName = menu.menu_ComponentName,

                    menu_PlatFrom = menu.menu_PlatFrom,
                    //menu_TypeId = menu.menu_Type,
                    //menu_Type = menu.menu_Type == (int)MenuTypeEnum.MenuGroup ? "菜单分组" : menu.menu_Type == (int)MenuTypeEnum.MenuFunction ? "菜单功能" : "未知类型",
                    Menu_TypeName = menus.FirstOrDefault(item => item.Id == menu.menu_ParentId)?.menu_Name,
                    create_User = menu.create_User,
                    create_Time = Convert.ToDateTime(menu.create_Time).ToString("yyyy-MM-dd HH:mm:ss"),
                };
            }) ?? new List<MenuResponse>();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        public static IEnumerable<MenuResponse> toVo(this IEnumerable<MenuResponse> menus)
        {
            return menus.Select(item => new MenuResponse()
            {
                Id = item.Id,
                name = item.menu_FileName,// 路由名称
                path = item.menu_Path,// 菜单路径
                component = item.menu_ComponentName,// 组件名称,
                menu_ParentId = item.menu_ParentId,
                isOuterJoin = item.isOuterJoin,
                create_Time = Convert.ToDateTime(item.create_Time).ToString("yyyy-MM-dd HH:mm:ss"),
                create_User = item.create_User,
                expand_Order = item.expand_Order,
                meta = new meta()
                {
                    icon = item.menu_Icon,// 图标
                    title = item.menu_Name,// 菜单名称
                    isLink = item.isOuterJoin ? item.menu_Link : ""
                }
            }); ;


        }
        #endregion

        #region 人员信息

        /// <summary>
        /// 人员实体转请求
        /// </summary>
        /// <param name="depts"></param>
        /// <returns></returns>
        public static IEnumerable<PersonResponse> toVo(this List<Person> persons, IEnumerable<DeptResponse> depts, IEnumerable<Job> jobs)
        {
            return persons?.Select(person =>
            {
                return new PersonResponse()
                {
                    Id = person.Id,
                    per_Name = person.per_Name,
                    per_FullName = SpellHelper.GetFull(person.per_Name),
                    per_SimpleName = SpellHelper.GetFull(person.per_Name),
                    per_JobStatusId = person.per_JobStatus,
                    per_JobStatus = person.per_JobStatus == "2" ? "离职" : "在职",
                    per_Phone = person.per_Phone,
                    per_IdNo = person.per_IdNo,
                    per_Address = person.per_Address,
                    per_Politics = person.per_Politics,
                    per_Birthday = person.per_Birthday,
                    dept_Id = person.dept_Id,// 所属单位
                    dept_Name = depts?.FirstOrDefault(parent => parent.Id == person.dept_Id)?.dept_Name ?? "",
                    job_Id = person.job_Id,// 职位
                    job_Name = jobs?.FirstOrDefault(parent => parent.Id == person.job_Id)?.job_Name ?? "",
                    create_User = person.create_User,
                    create_Time = Convert.ToDateTime(person.create_Time).ToString("yyyy-MM-dd HH:mm:ss"),

                    // 预留拓展
                    per_TypeId = person.per_Type,
                    per_Type = person.per_Type == "1" ? "内部人员" : person.per_Type == "2" ? "骑手" : "普通用户",
                    per_UserName = person.per_UserName,
                    per_UserPwd = person.per_UserPwd,
                    per_NormalizedPhone = person.per_NormalizedPhone,
                };
            }) ?? new List<PersonResponse>();

        }


        #endregion

        #region 用户信息

        /// <summary>
        /// 用户实体转请求
        /// </summary>
        /// <param name="depts"></param>
        /// <returns></returns>
        public static IEnumerable<UserResponse> toVo(this IEnumerable<User> users, IEnumerable<Dept> depts, List<Person> persons, List<Limit> limits = null)
        {
            return users?.Select(user =>
            {
                return new UserResponse()
                {
                    Id = user.Id,
                    user_LoginName = user.user_LoginName,
                    user_LoginPwd = user.user_LoginPwd,
                    user_LoginPwdCipher = user.user_LoginPwdCipher,
                    isEnable = user.isEnable,
                    dept_Id = user.dept_Id,
                    dept_Name = depts?.FirstOrDefault(item => item.Id == user.dept_Id)?.dept_Name ?? "",
                    limit_Id = user.limit_Id,
                    limit_Name = limits?.FirstOrDefault(item => item.Id == user.limit_Id)?.limit_Name ?? "",
                    person_Id = user.person_Id,
                    person_Name = persons?.FirstOrDefault(item => item.Id == user.person_Id)?.per_Name ?? "",
                    create_User = user.create_User,
                    create_Time = Convert.ToDateTime(user.create_Time).ToString("yyyy-MM-dd HH:mm:ss"),
                };
            }) ?? new List<UserResponse>();

        }
        #endregion

        #region 菜单权限信息


        /// <summary>
        /// 获取菜单组以及菜单组关联的菜单信息
        /// </summary>
        /// <param name="limitMenus"></param>
        /// <param name="menus"></param>
        /// <returns></returns>
        public static IEnumerable<LimitResponse> toVo(this List<Limit> limitMenus, IEnumerable<LimitMenuResponse> menus)
        {
            return limitMenus?.Select(limit => new LimitResponse()
            {
                Id = limit.Id,
                limit_Name = limit.limit_Name,
                limit_Menus = menus?.Where(item => item.limit_Id == limit.Id)?.ToList() ?? default,
                create_User = limit.create_User,
                create_Time = limit.create_Time?.ObjToDate().ToString("yyyy-MM-dd HH:mm") ?? "",
            })?.ToList() ?? new List<LimitResponse>();
        }

        /// <summary>
        ///  菜单权限关系表，菜单名称
        /// </summary>
        /// <param name="limitMenus"></param>
        /// <param name="menus"></param>
        /// <returns></returns>
        public static IEnumerable<LimitMenuResponse> toVo(this List<Limit_Menu> limitMenus, IEnumerable<MenuResponse> menus)
        {

            return limitMenus.Select(limitMenu => new LimitMenuResponse()
            {
                Id = limitMenu.Id,
                create_Time = limitMenu.create_Time,
                create_User = limitMenu.create_User,
                limit_Add = limitMenu.limit_Add,
                limit_Del = limitMenu.limit_Del,
                limit_Edit = limitMenu.limit_Edit,
                limit_Id = limitMenu.limit_Id,
                menu_IdArray = limitMenu.menu_IdArray,
                menu_Id = limitMenu.menu_Id,
                menu_Name = menus?.SingleOrDefault(item => item.Id == limitMenu.menu_Id)?.menu_Name ?? ""
            });
        }

        #endregion

    }
}
