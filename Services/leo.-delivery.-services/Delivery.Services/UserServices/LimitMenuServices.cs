using Delivery.Commons.Result;
using Delivery.Domains.Dto.UserServicesDto.DeptDto;
using Delivery.Domains.Dto.UserServicesDto.MenuLimitDto;
using Delivery.Domains.Dto.Vo;
using Delivery.EntityFramework.Core.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services.UserServices
{
    /// <summary>
    /// 菜单组管理菜单
    /// </summary>
    [AutoInject(typeof(ILimitMenuServices), InjectTypeEnum.Scope)]
    public class LimitMenuServices : ILimitMenuServices
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMenuServices _menuServices;

        public LimitMenuServices(UserDbContext userDbContext, IMenuServices menuServices)
        {
            _userDbContext = userDbContext;
            _menuServices = menuServices;
        }

        /// <summary>
        ///  获取菜单权限关联信息
        /// </summary>
        /// <param name="limitMenuRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LimitMenuResponse>> LimitMenuFullListAsync(LimitMenuRequest limitMenuRequest = null)
        {
            var limitMenuList = await LimitMenuListAsync(limitMenuRequest);
            var menuList = await _menuServices.MenuFullListAsync(new MenuRequest() { IdArray = limitMenuList?.Select(item => item.menu_Id).ToList() });
            return limitMenuList?.toVo(menuList) ?? new List<LimitMenuResponse>();
        }

        /// <summary>
        /// 获取菜单权限关联基础信息
        /// </summary>
        /// <param name="limitMenuRequest"></param>
        /// <returns></returns>
        public async Task<List<Limit_Menu>> LimitMenuListAsync(LimitMenuRequest limitMenuRequest = null)
        {
            IQueryable<Limit_Menu> limitMenuQuery = _userDbContext.Limit_Menus.AsNoTracking();

            // 指定权限组
            if (limitMenuRequest?.limit_Id != Guid.Empty && limitMenuRequest?.limit_Id != null)
                limitMenuQuery = limitMenuQuery.Where(item => item.limit_Id == limitMenuRequest.limit_Id);

            // 指定权限组数组
            else if (limitMenuRequest?.limit_IdArray?.Any() ?? false)
                limitMenuQuery = limitMenuQuery.Where(item => limitMenuRequest.limit_IdArray.Contains(item.limit_Id));

            var limitMenus = await limitMenuQuery.OrderBy(item => item.create_Time).ToListAsync();

            return limitMenus;
        }

        /// <summary>
        ///  菜单权限保存
        /// </summary>
        /// <param name="limitMenuRequest"></param>
        /// <returns></returns>
        public async Task<bool> LimitMenuSaveAsync(LimitMenuRequest limitMenuRequest = null)
        {
            var result = true;
            if (limitMenuRequest?.limitMenuList.Any() ?? false)
                await _userDbContext.AddRangeAsync(limitMenuRequest.limitMenuList);

            result &= (await _userDbContext.SaveChangesAsync()) > 0;
            return result;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="limitMenuRequest"></param>
        /// <returns></returns>
        public async Task<bool> LimitMenuDeleteAsync(LimitMenuRequest limitMenuRequest = null)
        {
            var result = true;

            var limitMenuList = await LimitMenuListAsync(limitMenuRequest);
            if (limitMenuList?.Any() ?? false)
            {
                _userDbContext.Remove(limitMenuList);
                result &= (await _userDbContext.SaveChangesAsync()) > 0;
            }
            return result;
        }

    }
}
