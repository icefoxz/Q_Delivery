using Delivery.Commons.Result;
using Delivery.Commons.XHelper;
using Delivery.Domains.Dto.SystemServicesDto.SystemDict;
using Delivery.Domains.Dto.UserServicesDto.UserDto;
using Delivery.Domains.SystemEntitys;
using Delivery.EntityFramework.Core.DbContexts;
using Delivery.WebApi.Filter;
using Delivery.WebApi.InitData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Delivery.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitDataController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;
        private readonly IConfiguration _configuration;
        public InitDataController(UserDbContext userDbContext, IConfiguration configuration)
        {
            _userDbContext = userDbContext;
            _configuration = configuration;
        }

        /// <summary>
        /// 初始化数据
        /// 汇总
        /// 0、创建单位 OK
        /// 1、创建职位 OK
        /// 2、创建人员 OK
        /// 3、创建权限组 OK
        /// 4、创建菜单 OK
        /// 5、菜单绑定权限组 OK 
        /// 6、初始化字典信息 
        /// 7、创建用户 OK
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [IgnoreAttributeFilter]
        public async Task<ResultMessage> InitData()
        {
            try
            {
                var isInitData = _configuration
                   .GetSection("isInitSeedData")
                   .Get<bool>();
                var Rider = _configuration.GetSection("Rider").Get<Rider>();
                if (_userDbContext?.Users.Any() ?? false)
                {
                    return new ResultMessage(false, "已存在数据，不允许进行初始化！");
                }
                else
                if (isInitData)
                {
                    // 读取配置文件

                    string jsonFilePath = "InitData/baseData.json";

                    // 读取JSON文件内容  
                    string jsonContent = System.IO.File.ReadAllText(jsonFilePath);

                    var jsonList = JsonConvert.DeserializeObject<List<initData>>(jsonContent);
                    var deptInsertList = new List<Dept>();
                    var jobInsertList = new List<Job>();
                    var personInsertList = new List<Person>();
                    var limitInsertList = new List<Limit>();
                    var menuInsertList = new List<Menu>();
                    var limitMenuInsertList = new List<Limit_Menu>();
                    var userInsertList = new List<User>();
                    var dictInsertList = new List<SystemDict>();

                    jsonList?.ForEach(dataItem =>
                    {
                        // 1、创建单位
                        var deptList = dataItem
                            .DeptList?
                            .Select(item =>
                                new Dept()
                                {
                                    dept_Name = item.dept_Name,
                                    dept_SimpleName = SpellHelper.GetFrist(item.dept_Name),
                                    dept_FullName = SpellHelper.GetFull(item.dept_Name),
                                    dept_Phone = item.dept_Phone,
                                    dept_Address = item.dept_Address,
                                    dept_PostCode = item.dept_PostCode,
                                }).ToList();

                        // 2、创建职位
                        var jobList = dataItem
                            .JobList?
                            .Select(item =>
                                new Job()
                                {
                                    job_Name = item.job_Name,
                                    dept_Id = deptList?
                                        .FirstOrDefault(deptItem =>
                                            deptItem.dept_Name == item.dept_Name)?
                                        .Id
                                }).ToList();

                        // 3、创建人员
                        var personList = dataItem
                            .PersonList?
                            .Select(item =>
                                new Person()
                                {
                                    per_Name = item.per_Name,
                                    per_SimpleName = SpellHelper.GetFrist(item.per_Name),
                                    per_FullName = SpellHelper.GetFull(item.per_Name),
                                    per_Phone = item.per_Phone,
                                    per_IdNo = item.per_IdNo,
                                    per_Address = item.per_Address,
                                    dept_Id = deptList?.FirstOrDefault(deptItem => deptItem.dept_Name == item.dept_Name)?.Id,
                                    job_Id = jobList?.FirstOrDefault(jobItem => jobItem.job_Name == item.job_Name)?.Id,

                                }).ToList();

                        // 4、创建权限组
                        var limitList = dataItem
                            .LimitList?
                            .Select(item =>
                                new Limit()
                                {
                                    limit_Name = item.limit_Name
                                }).ToList();

                        // 5、创建菜单
                        var menuList = dataItem
                            .MenuList?
                            .Select(item =>
                                new Menu()
                                {
                                    menu_Name = item.menu_Name,
                                    menu_SimpleName = SpellHelper.GetFrist(item.menu_Name),
                                    menu_FullName = SpellHelper.GetFull(item.menu_Name),
                                    menu_Path = item.menu_Path,
                                    menu_PlatFrom = item.menu_PlatFrom,
                                    menu_ParentName = item.menu_ParentName,// 后置处理
                                    expand_Order = item.expand_Order,
                                    menu_Icon = item.menu_Icon,
                                    menu_FileName = item.menu_FileName,
                                    menu_ComponentName = item.menu_ComponentName,
                                    isOuterJoin = item.isOuterJoin,
                                    menu_Link = item.menu_Link,
                                }).ToList();

                        menuList.ForEach(item =>
                            item.menu_ParentId =
                                                menuList?
                                                    .FirstOrDefault(menuItem =>
                                                        menuItem.menu_Name == item.menu_ParentName
                                                        )?.Id
                                                        );

                        // 6、菜单绑定权限组
                        var limitMenuList = menuList
                            .Select(item => new Limit_Menu()
                            {
                                limit_Id = limitList?.FirstOrDefault()?.Id ?? default,
                                menu_Id = item.Id,
                                menu_IdArray = item.menu_ParentId.Guid_IsEmpty() ?
                                            $"{item.Id}" :
                                            $"{item.menu_ParentId}&{item.Id}"
                            });

                        // 7、创建用户
                        var userList = dataItem
                            .UserList
                            .Select(item =>
                                new User()
                                {
                                    user_LoginName = item.user_LoginName,
                                    user_LoginPwd = MD5Helper.MD5X2String(item.user_LoginPwd),
                                    user_LoginPwdCipher = MD5Helper.MD5X2String(item.user_LoginPwd),
                                    dept_Id = deptList?.FirstOrDefault(deptItem => deptItem.dept_Name == item.dept_Name)?.Id,
                                    person_Id = personList?.FirstOrDefault(personItem => personItem.per_Name == item.person_Name)?.Id,
                                    limit_Id = limitList?.FirstOrDefault(limitItem => limitItem.limit_Name == item.limit_Name)?.Id,
                                    isEnable = true,
                                }).ToList();

                        // 8、字典信息
                        var dictList = dataItem
                                .DictList?
                                .Select(item =>
                                    new SystemDict()
                                    {
                                        dict_Key = item.dict_Key,
                                        dict_Value = item.dict_Value,
                                        dict_JsonValue = item.dict_JsonValue,
                                        expand_Desc = item.expand_Desc,
                                        expand_Order = item.expand_Order,
                                        dict_Name = item.dict_Name,
                                        isSystemBuilt = item.isSystemBuilt,
                                        dict_ParentKey = item.dict_ParentKey
                                    }).ToList();

                        dictList?.ForEach(item =>
                                item.ParentId =
                                    dictList
                                    .FirstOrDefault(item =>
                                        item.dict_Key == item.dict_ParentKey)?.Id
                        );

                        if (deptList?.Any() ?? false)
                            deptInsertList.AddRange(deptList);

                        if (jobList?.Any() ?? false)
                            jobInsertList.AddRange(jobList);

                        if (personList?.Any() ?? false)
                            personInsertList.AddRange(personList);

                        if (limitList?.Any() ?? false)
                            limitInsertList.AddRange(limitList);

                        if (menuList?.Any() ?? false)
                            menuInsertList.AddRange(menuList);

                        if (limitMenuList?.Any() ?? false)
                            limitMenuInsertList.AddRange(limitMenuList);

                        if (userList?.Any() ?? false)
                            userInsertList.AddRange(userList);

                        if (dictList?.Any() ?? false)
                            dictInsertList.AddRange(dictList);
                    });
                    if (deptInsertList?.Any() ?? false)
                        await _userDbContext.AddRangeAsync(deptInsertList);

                    if (jobInsertList?.Any() ?? false)
                        await _userDbContext.AddRangeAsync(jobInsertList);

                    if (personInsertList?.Any() ?? false)
                        await _userDbContext.AddRangeAsync(personInsertList);

                    if (limitInsertList?.Any() ?? false)
                        await _userDbContext.AddRangeAsync(limitInsertList);

                    if (menuInsertList?.Any() ?? false)
                        await _userDbContext.AddRangeAsync(menuInsertList);

                    if (limitMenuInsertList?.Any() ?? false)
                        await _userDbContext.AddRangeAsync(limitMenuInsertList);

                    if (userInsertList?.Any() ?? false)
                        await _userDbContext.AddRangeAsync(userInsertList);

                    if (dictInsertList?.Any() ?? false)
                        await _userDbContext.AddRangeAsync(dictInsertList);

                    await _userDbContext.SaveChangesAsync();
                }

                return new ResultMessage(isInitData);
            }
            catch (Exception ex)
            {
                return new ResultMessage(false, $"error-{ex.Message}");
            }
        }

    }
}
