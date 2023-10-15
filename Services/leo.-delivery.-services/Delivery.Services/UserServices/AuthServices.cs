﻿using Delivery.Commons.Cookie;
using Delivery.Commons.JWTHelper;
using Delivery.Domains.Dto.UserServicesDto.UserDto;
using Delivery.Domains.UserEntitys;
using Delivery.IServices.ISystemServices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Delivery.Services.UserServices
{
    [AutoInject(typeof(IAuthServices), InjectTypeEnum.Scope)]
    public class AuthServices : IAuthServices
    {
        private readonly UserDbContext _userDbContext;
        private readonly IUserServices _userServices;
        private readonly IMemoryCache _memoryCache;
        private readonly IMenuServices _menuServices;
        private readonly ILimitServices _limitServices;
        private readonly ILimitMenuServices _limitMenuServices;
        private readonly ISystemDictServices _systemDictServices;
        private readonly ILogger<AuthServices> _logger;
        public AuthServices(UserDbContext userDbContext, IUserServices userServices, IMemoryCache memoryCache, IMenuServices menuServices, ILimitServices limitServices, ILimitMenuServices limitMenuServices, ISystemDictServices systemDictServices, ILogger<AuthServices> logger)
        {
            _userDbContext = userDbContext;
            _userServices = userServices;
            _memoryCache = memoryCache;
            _menuServices = menuServices;
            _limitServices = limitServices;
            _limitMenuServices = limitMenuServices;
            _systemDictServices = systemDictServices;
            _logger = logger;
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultMessage> LoginOnAsync(UserRequest userRequest)
        {
            try
            {
                // 1:查询用户信息
                var user = await _userServices.UserAccountVerifictionAsync(userRequest);

                //2:校验用户是否存在，以及是否禁用
                if (user is null)
                    return new ResultMessage(false, $"{userRequest.user_LoginName}该用户不存在");
                if (!user.isEnable)
                    return new ResultMessage(false, $"账号已停用");
                if (user.user_LoginPwdCipher.ToUpper() != userRequest.user_LoginPwd.ToUpper())
                    return new ResultMessage(false, $"用户名或密码错误，请重新输入");
                var person = await _userDbContext.Persons.SingleOrDefaultAsync(item => item.Id == user.person_Id);
                if (person is null)
                    return new ResultMessage(false, "绑定人员不存在");
                if (person.per_Type == "2")
                    return new ResultMessage(false, "骑手禁止访问平台");

                // 3:生成Token
                var token = TokenHelp.GetToken(userRequest.user_LoginName ?? "");

                // 4:Token存缓存
                _memoryCache.Set(token, JsonConvert.SerializeObject(new BaseQuery()
                {
                    user_Id = user.Id.ToString(),
                    user_Name = user.person_Name,
                    login_Name = user.user_LoginName,
                    dept_Id = user.dept_Id?.ToString() ?? "",
                    dept_Name = user.dept_Name,
                    limit_Id = user.limit_Id?.ToString() ?? "",
                }));
                return new ResultMessage(true, token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[用户登录异常]：{ex.Message}");
                return new ResultMessage(false, ex.Message);
            }
        }

        /// <summary>
        /// 获取用户菜单信息
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultMessage> GetUserLimitAndMenuInfo(UserRequest userRequest = null)
        {
            try
            {
                var limitId = UserInfoCookie.limit_Id;

                #region 校验信息

                if (string.IsNullOrEmpty(limitId))
                    return new ResultMessage(false, "授权失败，请重新登录");

                // 验证权限组
                var limitInfo = await _limitServices.LimitListAsync(new LimitRequest() { Id = new Guid(limitId) });
                if (!limitInfo?.Any() ?? false)
                    return new ResultMessage(false, "当前用户未绑定权限组，或绑定的权限组不存在！");

                // 验证权限组关联
                var limitMenuRealtionList = await _limitMenuServices.LimitMenuFullListAsync(new LimitMenuRequest() { limit_Id = limitInfo?.FirstOrDefault()?.Id ?? default });

                #endregion

                #region 菜单信息

                var menuIdList = new List<Guid>();
                limitMenuRealtionList.ToList().ForEach(limitItem =>
                {
                    var menuIdArray = limitItem.menu_IdArray?.Split('&') ?? default;
                    if (menuIdArray?.Any() ?? false)
                        foreach (var item in menuIdArray)
                        {
                            var id = new Guid(item);
                            if (menuIdList.Contains(id) == false)
                                menuIdList.Add(id);
                        }
                });

                // 获取菜单信息
                var menuList = await _menuServices.MenuFullListAsync(new MenuRequest() { IdArray = menuIdList });

                #endregion

                #region 字典信息

                var dictList = await _systemDictServices.SystemDictFullListAsync(new Domains.Dto.SystemServicesDto.SystemDict.SystemDictRequest() { isTree = true, isSystemBuilt = true });

                #endregion

                return new ResultMessage(true, new
                {
                    menuList = menuList,
                    dictList = dictList,
                    userInfo = new BaseQuery()
                    {
                        user_Name = UserInfoCookie.user_Name,
                        login_Name = UserInfoCookie.login_Name,
                        dept_Name = UserInfoCookie.dept_Name,
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"[获取用户菜单信息异常]：{ex.Message}");
                return new ResultMessage(false, "获取用户信息异常");
            }
        }
    }
}
