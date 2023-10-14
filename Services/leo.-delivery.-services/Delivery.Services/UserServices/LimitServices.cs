
using Delivery.Domains.Dto.UserServicesDto.JobDto;
using Delivery.Domains.Dto.Vo;
using Delivery.EntityFramework.Core.DbContexts;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Delivery.Services.UserServices
{
    /// <summary>
    /// 权限组
    /// </summary>
    [AutoInject(typeof(ILimitServices), InjectTypeEnum.Scope)]
    public class LimitServices : ILimitServices
    {
        private readonly UserDbContext _userDbContext;
        private readonly ILimitMenuServices _limitMenuServices;
        private readonly IMenuServices _menuServices;
        private readonly ILogger<LimitServices> _logger;


        public LimitServices(UserDbContext userDbContext, ILimitMenuServices limitMenuServices, IMenuServices menuServices, ILogger<LimitServices> logger)
        {
            _userDbContext = userDbContext;
            _limitMenuServices = limitMenuServices;
            _menuServices = menuServices;
            _logger = logger;
        }

        /// <summary>
        /// 获取权限组信息
        /// </summary>
        /// <param name="limitRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LimitResponse>> LimitFullListAsync(LimitRequest limitRequest = null)
        {
            // 获取权限组信息
            var limitList = await LimitListAsync(limitRequest);
            // 获取权限关联信息
            var limitMenuList = await _limitMenuServices.LimitMenuFullListAsync(new LimitMenuRequest() { limit_IdArray = limitList?.Select(item => item.Id)?.ToList() });

            return limitList.toVo(limitMenuList);
        }

        /// <summary>
        /// 获取权限组基础信息
        /// </summary>
        /// <param name="limitRequest"></param>
        /// <returns></returns>
        public async Task<List<Limit>> LimitListAsync(LimitRequest limitRequest = null)
        {
            IQueryable<Limit> limitQuery = _userDbContext.Limits.AsNoTracking();

            // 权限组Id
            if (limitRequest?.Id != Guid.Empty && limitRequest?.Id != null)
                limitQuery = limitQuery.Where(item => item.Id == limitRequest.Id);
            else if (limitRequest?.IdList?.Any() ?? false)
                limitQuery = limitQuery.Where(item => limitRequest.IdList.Contains(item.Id));

            // 权限组名称
            if (string.IsNullOrWhiteSpace(limitRequest?.limit_Name) == false)
                limitQuery = limitQuery.Where(item => item.limit_Name.Contains(limitRequest.limit_Name));

            var limits = await limitQuery.OrderBy(item => item.create_Time).ToListAsync();

            return limits;
        }

        /// <summary>
        /// 权限组保存
        /// </summary>
        /// <param name="limitRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultMessage> LimitSaveAsync(LimitRequest limitRequest = null)
        {
            var result = true;
            var isVerifyDept = false;
            var limit = new Limit();
            var limitList = await LimitListAsync();

            if (limitRequest.Id == Guid.Empty)
                isVerifyDept = true;
            else
            {
                // Edit
                var limitTemp = limitList.FirstOrDefault(item => item.Id == limitRequest.Id);
                if (limitTemp?.limit_Name != limitRequest.limit_Name)
                    isVerifyDept = true;
                limit = limitTemp;
            }

            //验证重复
            if (isVerifyDept)
                if (limitList.Any(item => item.limit_Name == limitRequest.limit_Name))
                    return new ResultMessage(false, $"{limitRequest.limit_Name}已存在该名称相同的权限组、添加失败！");

            limit.limit_Name = limitRequest.limit_Name;

            if (limitRequest.Id == Guid.Empty)
                await _userDbContext.AddAsync(limit);
            else
                _userDbContext.Update(limit);

            result &= await _userDbContext.SaveChangesAsync() > 0;

            return new ResultMessage(result, result ? "保存成功" : "保存失败");

        }

        /// <summary>
        /// 权限组删除
        /// </summary>
        /// <param name="limitRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> LimitDeleteAsync(LimitRequest limitRequest = null)
        {
            var result = true;

            try
            {
                // 验证是否绑定过用户 
                if ((await _userDbContext.Users.Where(item => item.limit_Id == limitRequest.Id).ToListAsync())?.Any() ?? false)
                    return new ResultMessage(false, "该权限组已绑定用户、不允许删除");

                // 获取权限组信息
                var limitInfo = await _userDbContext.Limits.SingleOrDefaultAsync(item => item.Id == limitRequest.Id);

                if (limitInfo != null)
                {
                    // 获取权限组绑定的菜单信息
                    var limitMenuList = await _limitMenuServices.LimitMenuListAsync(new LimitMenuRequest() { limit_Id = limitInfo.Id });

                    // 删除权限组管理菜单
                    if (limitMenuList?.Any() ?? false)
                        _userDbContext.RemoveRange(limitMenuList);

                    // 删除权限组
                    _userDbContext.Remove(limitInfo);

                    result &= (await _userDbContext.SaveChangesAsync()) > 0;
                }
                return new ResultMessage(result, result ? "删除成功！" : "删除失败！");
            }
            catch (Exception ex)
            {
                _logger.LogError($"删除权限组失败：{ex.Message}");
                return new ResultMessage(false, "删除失败！");
            }
        }



        /// <summary>
        /// 保存权限组所属菜单信息
        /// </summary>
        /// <param name="limitRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> LimitRelevancyMenuSaveAsync(LimitRequest limitRequest = null)
        {
            try
            {
                // 权限组关联菜单信息
                var result = true;

                // 1.获取权限组Id
                var limitId = limitRequest.Id;

                // 2. 组装权限组关联数据 
                var limitRelationMenuList = limitRequest.menu_List?.Select(item => new Limit_Menu
                {
                    limit_Id = limitId,
                    menu_Id = item.menu_Id,
                    menu_IdArray = string.Join("&", item.menu_IdArray),
                    limit_Add = true,
                    limit_Del = true,
                    limit_Edit = true,
                    limit_Query = true,
                });

                // 3.删除原先的
                await _userDbContext.Limit_Menus.Where(item => item.limit_Id == limitId).ExecuteDeleteAsync();

                // 4.新增
                if (limitRelationMenuList?.Any() ?? false)
                    await _userDbContext.Limit_Menus.AddRangeAsync(limitRelationMenuList);

                // 5.保存
                result &= await _userDbContext.SaveChangesAsync() > 0;

                return new ResultMessage(result, result ? "保存成功" : "保存失败");
            }
            catch (Exception ex)
            {
                _logger.LogError($"删除权限组所属信息失败：{ex.Message}");
                return new ResultMessage(false, "保存失败！");
            }
        }

        /// <summary>
        /// 获取权限组已绑定的菜单
        /// </summary>
        /// <param name="limitRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> GetSelectMenuListAsync(LimitRequest limitRequest = null)
        {
            // 1.获取该权限组所有的菜单
            var limitMenuList = await _userDbContext.Limit_Menus.Where(item => item.limit_Id == limitRequest.Id).ToListAsync();

            //// 2.获取菜单中所有的菜单Id
            //var menuIdList = new List<Guid>();
            //limitMenuList.ForEach(item =>
            //{
            //    foreach (var itemMenu in item.menu_IdArray?.Split('&'))
            //    {
            //        var itemGuid = new Guid(itemMenu);
            //        if (menuIdList.Contains(itemGuid) == false)
            //            menuIdList.Add(itemGuid);
            //    }
            //});

            //// 3.调用菜单查询的方法传入菜单Id 
            //var result = await _menuServices.MenuFullListAsync(new MenuRequest() { IdArray = menuIdList });

            //return new ResultMessage(true, result);
            var menuList = new List<List<string>>();
            limitMenuList.ForEach(item =>
            {
                var tempList = item.menu_IdArray?.Split('&')?.ToList();
                if (tempList?.Any() ?? false)
                    menuList.Add(tempList);
            });

            return new ResultMessage(true, menuList);
        }
    }
}
