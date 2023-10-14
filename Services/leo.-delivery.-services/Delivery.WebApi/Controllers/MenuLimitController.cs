


using Microsoft.Extensions.Caching.Memory;

namespace Delivery.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuLimitController : BaseApiController
    {
        private readonly ILimitServices _limitServices;
        private readonly IMenuServices _menuServices;
        private readonly IMemoryCache _memoryCache;

        public MenuLimitController(ILimitServices limitServices, IMemoryCache memoryCache, IMenuServices menuServices)
        {
            _limitServices = limitServices;
            _memoryCache = memoryCache;
            _menuServices = menuServices;
        }

        // 需提供一个根据权限组Id，查询这个权限组绑定了那些菜单
        // 需提供一个权限组Id，选择的菜单列表保存接口

        #region 权限组

        /// <summary>
        /// 获取权限组信息
        /// </summary>
        /// <param name="limitRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getLimitList")]
        public async Task<ResultMessage> GetLimitListAsync([FromQuery] LimitRequest limitRequest) => new ResultMessage(true, await _limitServices.LimitFullListAsync(limitRequest));

        /// <summary>
        /// 保存权限组
        /// </summary>
        /// <param name="limitRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("saveLimit")]
        public async Task<ResultMessage> SaveLimitAsync([FromBody] LimitRequest limitRequest) => await _limitServices.LimitSaveAsync(limitRequest);

        /// <summary>
        /// 删除权限组
        /// </summary>
        /// <param name="limitRequest"></param>
        /// <returns></returns>
        [HttpDelete, Route("deleteLimit")]
        public async Task<ResultMessage> DeleteLimitAsync([FromQuery] LimitRequest limitRequest) => await _limitServices.LimitDeleteAsync(limitRequest);

        #endregion

        #region 菜单管理

        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="menuRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getMenuListAsync")]
        public async Task<ResultMessage> GetMenuListAsync([FromQuery] MenuRequest menuRequest) => new ResultMessage(true, await _menuServices.MenuFullListAsync(menuRequest));

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="menuRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("saveMenu")]
        public async Task<ResultMessage> SaveMenuAsync([FromBody] MenuRequest menuRequest) => await _menuServices.MenuSaveAsync(menuRequest);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuRequest"></param>
        /// <returns></returns>
        [HttpDelete, Route("deleteMenu")]
        public async Task<ResultMessage> DeleteMenuAsync([FromQuery] MenuRequest menuRequest) => await _menuServices.MenuDeleteAsync(menuRequest);


        #endregion

        #region 权限绑定菜单管理

        /// <summary>
        /// 权限组分配菜单
        /// </summary>
        /// <param name="limitRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("saveLimitRelevancyMenu")]
        public async Task<ResultMessage> SaveLimitRelevancyMenuAsync([FromBody] LimitRequest limitRequest) => await _limitServices.LimitRelevancyMenuSaveAsync(limitRequest);

        /// <summary>
        /// 获取指定权限组已选中的菜单
        /// </summary>
        /// <param name="limitRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getSelectMenuList")]
        public async Task<ResultMessage> GetSelectMenuList([FromQuery] LimitRequest limitRequest) => await _limitServices.GetSelectMenuListAsync(limitRequest);

        #endregion

        //[HttpGet, Route("get")]
        //public async Task<object> get(int id)
        //{
        //   return  _memoryCache.Get(id);

        //}
        //[HttpGet,Route("set")]
        //public async Task<ResultMessage> set(int id)
        //{
        //    _memoryCache.Set(id, id + "测试");

        //    return new ResultMessage(true, "成功");
        //}

    }
}
