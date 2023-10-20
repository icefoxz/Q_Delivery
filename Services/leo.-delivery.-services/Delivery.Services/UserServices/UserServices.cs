using Delivery.Commons.Config;
using Delivery.Commons.Encry;
using Delivery.Commons.Result;
using Delivery.Commons.XHelper;
using Delivery.Domains.Dto.UserServicesDto.JobDto;
using Delivery.Domains.Dto.UserServicesDto.PersonDto;
using Delivery.Domains.Dto.UserServicesDto.UserDto;
using Delivery.Domains.Dto.Vo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services.UserServices
{
    [AutoInject(typeof(IUserServices), InjectTypeEnum.Scope)]
    public class UserServices : IUserServices
    {
        private readonly UserDbContext _userDbContext;
        private readonly ILimitServices _limitServices;
        private readonly IDeptServices _deptServices;
        private readonly IPersonServices _personServices;
        private readonly ILogger<UserServices> _logger;

        public UserServices(UserDbContext userDbContext, IPersonServices personServices, ILimitServices limitServices, IDeptServices deptServices, ILogger<UserServices> logger)
        {
            _personServices = personServices;
            _userDbContext = userDbContext;
            _limitServices = limitServices;
            _deptServices = deptServices;
            _logger = logger;
        }

        /// <summary>
        /// 用户基础信息
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> UserListAsync(UserRequest userRequest = null)
        {
            IQueryable<User> userQuery = _userDbContext.Users.AsNoTracking();

            if (string.IsNullOrWhiteSpace(userRequest?.user_LoginName) == false)
                userQuery = userQuery.Where(item => item.user_LoginName.Contains(userRequest.user_LoginName));


            if (userRequest?.limit_Id.Guid_NoEmpty() == false)
                userQuery = userQuery.Where(item => item.limit_Id == userRequest!.limit_Id);

            if (userRequest?.dept_IdArray?.Any() ?? false)
                userQuery = userQuery.Where(item => userRequest.dept_IdArray.Contains(item.dept_Id));

            else if (userRequest?.dept_Id.Guid_NoEmpty() == false)
                userQuery = userQuery.Where(item => item.dept_Id == userRequest!.dept_Id);

            var users = await userQuery.OrderBy(item => item.create_Time).ToListAsync();

            return users;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="UserRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserResponse>> UserFullListAsync(UserRequest userRequest = null)
        {
            try
            {
                // 单位
                var deptIdList = new List<Guid?>();

                // 获取所有单位的下级
                var deptChildrenList = (await _deptServices.DeptFullListAsync(new DeptRequest() { dept_ParentId = userRequest.dept_Id })).Data as List<DeptResponse>;

                if (deptChildrenList?.Any() ?? false)
                    deptIdList = deptChildrenList?.Select(item => (Guid?)item.Id)?.ToList();
                userRequest.dept_IdArray = deptIdList;

                // 获取用户基础信息
                var userList = await UserListAsync(userRequest);
                var personIdList = userList.Select(item => item.person_Id).ToList();// 人员
                var limitIdList = userList.Select(item => item.limit_Id).ToList();// 权限
                var deptList = await _deptServices.DeptListAsync(new DeptRequest() { IdList = deptIdList });
                var personList = await _personServices.PersonListAsync(new PersonRequest() { IdList = personIdList });
                var limitList = await _limitServices.LimitListAsync(new LimitRequest() { IdList = limitIdList });
                return userList.ToList().toVo(deptList, personList.ToList(), limitList);

            }
            catch (Exception ex)
            {
                _logger.LogError($"[获取用户信息异常]:{ex.Message}");
                return new List<UserResponse>();
            }
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> UserSaveAsync(UserRequest userRequest = null)
        {
            var result = true;
            var isVerifyDept = false;
            var user = new User();
            try
            {
                if (userRequest.Id == Guid.Empty)
                    isVerifyDept = true;
                else
                {
                    // Edit
                    var userTemp = await _userDbContext.Users.FirstOrDefaultAsync(item => item.Id == userRequest.Id);
                    if (userTemp is null)
                        return new ResultMessage(false, "User not exist!");

                    if (userTemp!.user_LoginName != userRequest.user_LoginName)
                        isVerifyDept = true;
                    user = userTemp;
                }

                user.user_LoginName = userRequest.user_LoginName!;
                user.user_LoginPwd = userRequest.user_LoginPwd!;
                user.user_LoginPwdCipher = userRequest.user_LoginPwd!;
                user.dept_Id = userRequest.dept_Id;
                user.limit_Id = userRequest.limit_Id;
                user.person_Id = userRequest.person_Id;

                // 验证名称
                if (isVerifyDept)
                    if (await _userDbContext.Users.AnyAsync(item => item.user_LoginName == userRequest.user_LoginName && item.Id != userRequest.Id))
                        return new ResultMessage(false, $"{userRequest.user_LoginName} already exists!");

                if (userRequest.Id == Guid.Empty)
                    await _userDbContext.AddAsync(user);
                else
                    _userDbContext.Update(user);

                result &= await _userDbContext.SaveChangesAsync() > 0;

                return new ResultMessage(result, result ? "Saved" : "Unsaved!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[用户保存异常]:{ex.Message}");
                return new ResultMessage(false, "Dave failed!");
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> UserDeleteAsync(UserRequest userRequest = null)
        {
            var result = true;

            try
            {
                var userInfo = await _userDbContext.Users.SingleOrDefaultAsync(item => item.Id == userRequest.Id);
                if (userInfo != null)
                {
                    _userDbContext.Remove(userInfo);
                    result &= (await _userDbContext.SaveChangesAsync()) > 0;
                }
                return new ResultMessage(result, result ? "删除成功！" : "删除失败！");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[用户删除异常]:{ex.Message}");
                return new ResultMessage(false, "用户删除异常");
            }
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> UserDisableAsync(UserRequest userRequest = null)
        {
            var result = true;

            try
            {
                var userInfo = await _userDbContext.Users.SingleOrDefaultAsync(item => item.Id == userRequest.Id);
                if (userInfo != null)
                {
                    userInfo.isEnable = userRequest.isEnable;
                    _userDbContext.Update(userInfo);
                    result &= (await _userDbContext.SaveChangesAsync()) > 0;
                }
                return new ResultMessage(result, result ? "操作成功！" : "操作失败！");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[用户禁用异常]:{ex.Message}");
                return new ResultMessage(false, "用户禁用异常");
            }
        }

        /// <summary>
        /// 用户信息校验
        /// </summary>
        /// <param name="UserRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<UserResponse> UserAccountVerificationAsync(UserRequest userRequest = null)
        {
            var userList = await _userDbContext.Users.AsNoTracking().Where(item => item.user_LoginName == userRequest.user_LoginName).ToListAsync();

            if (userList?.Any() ?? false)
            {
                var userInfo = userList.FirstOrDefault();
                var deptId = (userInfo?.dept_Id ?? null);
                var personId = (userInfo?.person_Id ?? null);
                var limitId = (userInfo?.limit_Id ?? null);

                var deptList = await _userDbContext.Depts.AsNoTracking().Where(item => item.Id == deptId).ToListAsync();
                var personList = await _userDbContext.Persons.AsNoTracking().Where(item => item.Id == personId).ToListAsync();
                var limitList = await _userDbContext.Limits.AsNoTracking().Where(item => item.Id == limitId).ToListAsync();

                return userList?
                        .toVo(deptList, personList, limitList)?
                        .FirstOrDefault() ?? null;

            }

            return null;
        }
    }
}
