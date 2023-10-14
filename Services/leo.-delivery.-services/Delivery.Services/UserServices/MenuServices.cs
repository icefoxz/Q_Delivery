using Delivery.Commons.Result;
using Delivery.Commons.XHelper;
using Delivery.Domains.Dto.UserServicesDto.JobDto;
using Delivery.Domains.Dto.UserServicesDto.MenuDto;
using System.Net;
using Delivery.Domains.Dto.UserServicesDto.MenuDto;
using Delivery.Domains.Dto.Vo;

namespace Delivery.Services.UserServices
{
    [AutoInject(typeof(IMenuServices), InjectTypeEnum.Scope)]
    public class MenuServices : IMenuServices
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;

        public MenuServices(UserDbContext userDbContext, IMapper mapper)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// 旧的
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MenuResponse>> MenuListAsync(MenuRequest menuRequest = null, bool isLevel = true)
        {
            var MenuList = await _userDbContext.Menus.OrderBy(item => item.create_Time).ToListAsync();
            return MenuList.toVo(isLevel);
        }

        /// <summary>
        /// 获取菜单基础数据
        /// </summary>
        /// <param name="menuRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Menu>> MenuListAsync(MenuRequest menuRequest = null)
        {
            IQueryable<Menu> menuQuery = _userDbContext.Menus.AsNoTracking();

            if (string.IsNullOrWhiteSpace(menuRequest?.menu_Name) == false)
                menuQuery = menuQuery.Where(item => item.menu_Name.Contains(menuRequest.menu_Name) || item.menu_FullName.Contains(menuRequest.menu_Name) || item.menu_SimpleName.Contains(menuRequest.menu_Name));

            // 根据菜单Id进行筛选
            if (menuRequest?.IdArray?.Any() ?? false)
                menuQuery = menuQuery.Where(item => menuRequest.IdArray.Contains(item.Id));

            var menus = await menuQuery.OrderBy(item => item.expand_Order).ToListAsync();

            return menus;
        }

        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <param name="menuRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MenuResponse>> MenuFullListAsync(MenuRequest menuRequest = null)
        {
            var menuList = await MenuListAsync(menuRequest);
            var menuResponList = menuList.Select(item => _mapper.Map<MenuResponse>(item)).toVo();
            //menuResponList= GetMenuChildren(menuResponList, Guid.Empty);
            return GetMenuChildren(menuResponList, null);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menuRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> MenuSaveAsync(MenuRequest menuRequest = null)
        {

            var result = true;
            var isVerifyDept = false;
            var menu = new Menu();
            var menuList = await _userDbContext.Menus.ToListAsync();
            if (menuRequest.Id == Guid.Empty)
            {
                //Insert
                isVerifyDept = true;
            }
            else if (menuRequest.Id != Guid.Empty && menuRequest.Id != null)
            {
                // Edit
                var menuInfo = menuList.FirstOrDefault(item => item.Id == menuRequest.Id);
                if (menuInfo.menu_Name != menuRequest.menu_Name)
                    isVerifyDept = true;
                menu = menuInfo;
            }
            menu.menu_Name = menuRequest.menu_Name ?? "";
            menu.menu_FullName = SpellHelper.GetFull(menuRequest.menu_Name ?? "");
            menu.menu_SimpleName = SpellHelper.GetFrist(menuRequest.menu_Name ?? "");
            menu.menu_Path = menuRequest.menu_Path;
            // 新增
            menu.menu_Icon = menuRequest.menu_Icon;
            menu.menu_FileName = menuRequest.menu_FileName;
            menu.menu_ComponentName = menuRequest.menu_ComponentName;

            menu.menu_ParentId = menuRequest.menu_ParentId;
            menu.menu_PlatFrom = PlatformEnum.Delivery;
            menu.isOuterJoin = menuRequest.isOuterJoin;// 是否是外链
            menu.menu_Link = menuRequest.menu_Link;// 外链链接
            menu.expand_Order = menuRequest.expand_Order;
            //menu.menu_Type = (int)menuRequest.menu_Type;
            if (isVerifyDept)
                if (menuList.Any(item => item.menu_Name == menuRequest.menu_Name && menuRequest.Id != item.Id))
                    return new ResultMessage(false, $"{menuRequest.menu_Name}该菜单名称已经存在！");

            if (menuRequest.Id == Guid.Empty)
                await _userDbContext.Menus.AddAsync(menu);
            else if (menuRequest.Id != Guid.Empty)
                _userDbContext.Menus.Update(menu);

            result &= await _userDbContext.SaveChangesAsync() > 0;

            return new ResultMessage(result, result ? "保存成功" : "保存失败或未进行修改");
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> MenuDeleteAsync(MenuRequest menuRequest = null)
        {
            var result = true;

            // 验证是否绑定过菜单权限 
            if ((await _userDbContext.Limit_Menus.Where(item => item.menu_Id == menuRequest.Id).ToListAsync())?.Any() ?? false)
                return new ResultMessage(false, "该菜单已绑定过权限组，不允许删除");

            // 验证是否有下级数据
            if (await _userDbContext.Menus.AnyAsync(item => item.menu_ParentId == menuRequest.Id))
                return new ResultMessage(false, "该菜单包含子级，请先删除子级");

            var menuInfo = await _userDbContext.Menus.SingleOrDefaultAsync(item => item.Id == menuRequest.Id);

            if (menuInfo != null)
            {
                _userDbContext.Menus.Remove(menuInfo);
                result &= (await _userDbContext.SaveChangesAsync()) > 0;
            }
            return new ResultMessage(result, result ? "删除成功！" : "删除失败！");
        }


        public List<MenuResponse> GetMenuChildren(IEnumerable<MenuResponse> menuList, Guid? parentId)
        {
            var resultList = new List<MenuResponse>();
            //if (parentId == Guid.Empty)
            //    resultList.Add(new DeptResponse() { Id = Guid.Empty, dept_Name = "全部" });

            // 验证集合
            if (menuList?.Any() ?? false)
            {
                var menus = menuList.Where(item => item.menu_ParentId == parentId);
                if (menus?.Any() ?? false)
                {
                    foreach (var menu in menus)
                    {
                        var childrenList = GetMenuChildren(menuList, menu.Id);
                        if (childrenList?.Any() ?? false)
                            menu.children = childrenList.OrderBy(item => item.expand_Order).ToList();
                        resultList.Add(menu);
                    }
                }
            }

            return resultList;
        }
    }
}
