
using Delivery.Commons.Cookie;
using Delivery.Commons.Result;
using Delivery.Domains.Dto.Vo;
using Delivery.Domains.UserEntitys;
using Delivery.IServices.IUserServices;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Delivery.Services.UserServices
{

    [AutoInject(typeof(IDeptServices), InjectTypeEnum.Scope)]
    public class DeptServices : IDeptServices
    {
        private readonly UserDbContext _userDbContext;

        public DeptServices(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        /// <summary>
        /// 单位保存
        /// </summary>
        /// <param name="deptRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> DeptSaveAsync(DeptRequest deptRequest)
        {
            var result = true;
            var isVerifyDept = false;
            var deptInfo = new Dept();
            //var parentDept = await _userDbContext.Depts.OrderBy(item => item.create_Time).FirstOrDefaultAsync();

            // 验证单位名称是否存在
            if ((bool)(deptRequest?.Id.Guid_IsEmpty()))
            {
                // Insert
                if ((await _userDbContext.Depts.FirstOrDefaultAsync(item => item.dept_Name == deptRequest.dept_Name)) != null)
                    isVerifyDept = true;
            }
            else if (deptRequest?.Id.Guid_IsEmpty() == false)
            {
                // Edit
                var deptList = await _userDbContext.Depts.Where(item => item.Id == deptRequest.Id || item.dept_Name == deptRequest.dept_Name).ToListAsync();
                deptInfo = deptList.FirstOrDefault(item => item.Id == deptRequest.Id);
                // 验证编辑的当前数据，名称是否发生改变，如果发生改变，验证重复
                if (!deptInfo?.dept_Name.Equals(deptRequest.dept_Name) ?? false)
                    if ((await _userDbContext.Depts.FirstOrDefaultAsync(item => item.dept_Name == deptRequest.dept_Name)) != null)
                        isVerifyDept = true;
                //if() 需验证当前Id和父级Id是否是一个单位
            }
            if (isVerifyDept)
                if ((await _userDbContext.Depts.FirstOrDefaultAsync(item => item.dept_Name == deptRequest.dept_Name)) != null)
                    return new ResultMessage(false, $"{deptRequest.dept_Name}该名称已存在，不允许重复添加！");

            if (deptRequest.Id.Guid_IsEmpty())
                await _userDbContext.Depts.AddAsync(new Dept()
                {
                    dept_Name = deptRequest.dept_Name,
                    dept_Phone = deptRequest.dept_Phone,
                    dept_PostCode = deptRequest.dept_PostCode,
                    dept_Address = deptRequest.dept_Address,
                    dept_ParentId = deptRequest?.dept_ParentId ?? default,
                });
            else
            {
                deptInfo.dept_Name = deptRequest.dept_Name;
                deptInfo.dept_Phone = deptRequest.dept_Phone;
                deptInfo.dept_PostCode = deptRequest.dept_PostCode;
                deptInfo.dept_Address = deptRequest.dept_Address;
                deptInfo.dept_ParentId = deptRequest?.dept_ParentId ?? default;
                _userDbContext.Update(deptInfo);
            }

            result &= (await _userDbContext.SaveChangesAsync()) > 0;
            return new ResultMessage(result, result ? "保存成功" : "保存失败,可能未进行修改！");
        }

        /// <summary>
        /// 获取基础单位信息
        /// </summary>
        /// <returns>单位信息以及上级单位</returns>
        public async Task<IEnumerable<Dept>> DeptListAsync(DeptRequest deptRequest = null)
        {
            IQueryable<Dept> deptQuery = _userDbContext.Depts.AsNoTracking();

            if (string.IsNullOrWhiteSpace(deptRequest?.dept_Name) == false)
                deptQuery = deptQuery.Where(item => item.dept_Name.Contains(deptRequest.dept_Name));

            if (string.IsNullOrWhiteSpace(deptRequest?.dept_Phone) == false)
                deptQuery = deptQuery.Where(item => item.dept_Phone!.Contains(deptRequest.dept_Phone));

            if (deptRequest?.Id.Guid_IsEmpty() == false)
                deptQuery = deptQuery.Where(item => deptRequest.Id == item.Id);
            else if (deptRequest?.IdList?.Any() ?? false)
                deptQuery = deptQuery.Where(item => deptRequest.IdList.Contains(item.Id));

            if (deptRequest?.dept_ParentId.Guid_IsEmpty() == false)
                deptQuery = deptQuery.Where(item => deptRequest.dept_ParentId == item.dept_ParentId);

            var depts = await deptQuery.OrderBy(item => item.expand_Order).OrderBy(item => item.create_Time).ToListAsync();

            return depts;
        }

        /// <summary>
        /// 上级单位处理
        /// </summary>
        /// <param name="deptList"></param>
        /// <returns></returns>
        public async Task<ResultMessage> DeptFullListAsync(DeptRequest deptRequest)
        {
            var deptBaseList = await DeptListAsync(new DeptRequest() { dept_Name = deptRequest.dept_Name, dept_Phone = deptRequest.dept_Phone });

            var deptList = GetDeptChildrenTile(deptBaseList, deptRequest.dept_ParentId ?? default, true);

            var result = deptList.ToList().toVo(deptBaseList).ToList();

            return new ResultMessage(true, result);
        }

        /// <summary>
        /// 删除单位
        /// </summary>
        /// <param name="deptRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> DeptDeleteAsync(DeptRequest deptRequest = null)
        {
            var result = true;

            // 验证是否绑定过职位
            if ((await _userDbContext.Jobs.FirstOrDefaultAsync(item => item.dept_Id == deptRequest.Id)) != null)
                return new ResultMessage(false, "该单位已绑定职位不允许删除");
            if ((await _userDbContext.Users.FirstOrDefaultAsync(item => item.dept_Id == deptRequest.Id)) != null)
                return new ResultMessage(false, "该单位已绑定用户不允许删除");
            if ((await _userDbContext.Persons.FirstOrDefaultAsync(item => item.dept_Id == deptRequest.Id)) != null)
                return new ResultMessage(false, "该单位已绑定人员不允许删除");
            if ((await _userDbContext.Depts.FirstOrDefaultAsync(item => item.dept_ParentId == deptRequest.Id)) != null)
                return new ResultMessage(false, "该单位存在下级单位不允许删除");

            var deptInfo = await _userDbContext.Depts.SingleOrDefaultAsync(item => item.Id == deptRequest.Id);
            if (deptInfo != null)
            {
                _userDbContext.Remove(deptInfo);
                result &= (await _userDbContext.SaveChangesAsync()) > 0;
            }
            return new ResultMessage(result, result ? "删除成功！" : "删除失败！");
        }

        /// <summary>
        /// 获取上级单位列表
        /// </summary>
        /// <param name="deptRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> DeptParentListAsync(DeptRequest deptRequest = null)
        {
            var deptList = await DeptListAsync();
            var deptParentList = GetDeptChildren(deptList.toVo(), deptRequest.Id);
            return new ResultMessage(true, deptParentList);
        }

        /// <summary>
        /// 获取下级
        /// </summary>
        /// <param name="depts"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<DeptResponse> GetDeptChildren(IEnumerable<DeptResponse> deptList, Guid? parentId)
        {
            var resultList = new List<DeptResponse>();
            //if (parentId == Guid.Empty)
            //    resultList.Add(new DeptResponse() { Id = Guid.Empty, dept_Name = "全部" });

            // 验证集合
            if (deptList?.Any() ?? false)
            {
                var depts = new List<DeptResponse>();

                if (parentId.Guid_IsEmpty())
                    depts = deptList.Where(item => item.dept_ParentId == parentId || item.dept_ParentId == Guid.Empty).ToList();
                else
                    depts = deptList.Where(item => item.dept_ParentId == parentId).ToList();

                if (depts?.Any() ?? false)
                {
                    foreach (var dept in depts)
                    {
                        var childrenList = GetDeptChildren(deptList, dept.Id);
                        if (childrenList?.Any() ?? false)
                            dept.children = childrenList;
                        resultList.Add(dept);
                    }
                }
            }

            return resultList;
        }

        /// <summary>
        /// 获取下级
        /// </summary>
        /// <param name="depts"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Dept> GetDeptChildrenTile(IEnumerable<Dept> deptList, Guid parentId, bool isFrist = false)
        {
            var resultList = new List<Dept>();

            if (isFrist)
            {
                var deptItem = deptList.FirstOrDefault(item => item.Id == parentId);
                if (deptItem != null)
                    resultList.Add(deptItem);
            }

            // 验证集合
            if (deptList?.Any() ?? false)
            {
                var depts = deptList.Where(item => item.dept_ParentId == parentId);
                if (depts?.Any() ?? false)
                {
                    foreach (var dept in depts)
                    {
                        var childrenList = GetDeptChildrenTile(deptList, dept.Id);
                        if (childrenList?.Any() ?? false)
                            resultList.AddRange(childrenList);
                        //dept.children = childrenList;
                        resultList.Add(dept);

                    }
                }
            }

            return resultList;
        }
    }
}