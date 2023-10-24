using Delivery.Commons.Result;
using Delivery.Commons.XHelper;
using Delivery.Domains.Dto.UserServicesDto.DeptDto;
using Delivery.Domains.Dto.UserServicesDto.JobDto;
using Delivery.Domains.Dto.UserServicesDto.PersonDto;
using Delivery.Domains.Dto.Vo;
using Delivery.Domains.UserEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services.UserServices
{
    [AutoInject(typeof(IPersonServices), InjectTypeEnum.Scope)]
    public class PersonServices : IPersonServices
    {
        private readonly UserDbContext _userDbContext;
        private readonly IDeptServices _deptServices;
        private readonly IJobServices _jobServices;

        public PersonServices(UserDbContext userDbContext, IDeptServices deptServices, IJobServices jobServices)
        {
            _userDbContext = userDbContext;
            _deptServices = deptServices;
            _jobServices = jobServices;
        }

        /// <summary>
        /// 获取人员基础信息
        /// </summary>
        /// <param name="personRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Person>> PersonListAsync(PersonRequest personRequest = null)
        {
            IQueryable<Person> personQuery = _userDbContext.Persons.AsNoTracking();

            if (string.IsNullOrWhiteSpace(personRequest?.per_Name) == false)
                personQuery = personQuery.Where(item => item.per_Name.Contains(personRequest.per_Name) || item.per_Phone.Contains(personRequest.per_Name));

            if (personRequest?.IdList?.Any() ?? false)
                personQuery = personQuery.Where(item => personRequest.IdList.Contains(item.Id) || item.Id == Guid.Empty);

            if (personRequest?.dept_IdArray?.Any() ?? false)
                personQuery = personQuery.Where(item => personRequest!.dept_IdArray!.Contains((Guid)item.dept_Id));
            else if (personRequest?.dept_Id.Guid_IsEmpty() == false)
                personQuery = personQuery.Where(item => item.dept_Id == personRequest!.dept_Id);

            // 人员类型 1- 内部人员，2-骑手，3-普通用户
            if (string.IsNullOrEmpty(personRequest?.per_Type) == false)
                personQuery = personQuery.Where(item => item.per_Type == personRequest!.per_Type);

            var persons = await personQuery.OrderBy(item => item.create_Time).ToListAsync();

            return persons;
        }

        /// <summary>
        /// 获取人员全信息
        /// </summary>
        /// <param name="personRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PersonResponse>> PersonFullListAsync(PersonRequest personRequest = null)
        {
            // 获取所有单位的下级
            var deptList = (await _deptServices.DeptFullListAsync(new DeptRequest() { dept_ParentId = personRequest.dept_Id })).Data as List<DeptResponse>;

            var personList = await PersonListAsync(new PersonRequest() { dept_IdArray = deptList.Select(item => item.Id).ToList(), per_Type = personRequest.per_Type,per_Name=personRequest.per_Name });
            var deptIdList = personList.Select(item => item.dept_Id).ToList();
            var jobIdList = personList.Select(item => item.job_Id).ToList();// 职位
            //var deptList = await _deptServices.DeptListAsync(new DeptRequest() { IdList = deptIdList });
            var jobList = await _jobServices.JobListAsync(new JobRequest() { IdList = jobIdList });
            return personList.ToList().toVo(deptList, jobList);
        }


        /// <summary>
        /// 人员保存
        /// </summary>
        /// <param name="personRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultMessage> PersonSaveAsync(PersonRequest personRequest = null)
        {
            var result = true;
            var isVerifyDept = false;
            var person = new Person();
            var personList = await _userDbContext.Persons.ToListAsync();
            if (personRequest.Id == Guid.Empty)
            {
                //Insert
                isVerifyDept = true;
            }
            else
            {
                // Edit
                var personTemp = personList.FirstOrDefault(item => item.Id == personRequest.Id);
                if (personTemp.per_Name != personRequest.per_Name)
                    isVerifyDept = true;
                person = personTemp;
            }

            person.per_Name = personRequest.per_Name ?? "";
            person.per_FullName = SpellHelper.GetFull(personRequest.per_Name ?? "");
            person.per_SimpleName = SpellHelper.GetFull(personRequest.per_Name ?? "");
            person.per_JobStatus = personRequest.per_JobStatus;
            person.per_Phone = personRequest.per_Phone;
            person.per_IdNo = personRequest.per_IdNo;
            person.per_Address = personRequest.per_Address;
            person.per_Politics = personRequest.per_Politics;
            person.per_Birthday = personRequest.per_Birthday;
            person.dept_Id = personRequest.dept_Id;// 所属单位
                                                   // 预留拓展
            person.per_Type = personRequest.per_Type;
            person.per_UserName = personRequest.per_UserName;
            person.per_UserPwd = personRequest.per_UserPwd;
            person.per_NormalizedPhone = personRequest.per_NormalizedPhone;

            // 验证名称
            if (isVerifyDept)
                if (personList?.Any(item => item.per_Name == personRequest.per_Name && item.Id != personRequest.Id) ?? false)
                    return new ResultMessage(false, $"{personRequest.per_Name}已存在该名称相同的人员");

            if (personRequest.Id == Guid.Empty)
                await _userDbContext.Persons.AddAsync(person);
            else
                _userDbContext.Persons.Update(person);

            result &= await _userDbContext.SaveChangesAsync() > 0;

            return new ResultMessage(result, result ? "保存成功" : "保存失败", 1, person);
        }

        /// <summary>
        /// 人员删除
        /// </summary>
        /// <param name="PersonRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> PersonDeleteAsync(PersonRequest personRequest = null)
        {
            var result = true;

            // 验证是否绑定过让用户 
            if ((await _userDbContext.Users.Where(item => item.person_Id == personRequest.Id).ToListAsync())?.Any() ?? false)
                return new ResultMessage(false, "该人员已绑定用户不允许删除");

            var personInfo = await _userDbContext.Persons.SingleOrDefaultAsync(item => item.Id == personRequest.Id);
            if (personInfo != null)
            {
                _userDbContext.Remove(personInfo);
                result &= (await _userDbContext.SaveChangesAsync()) > 0;
            }
            return new ResultMessage(result, result ? "删除成功！" : "删除失败！");
        }

        /// <summary>
        /// 获取指定类型的人员
        /// </summary>
        /// <param name="personRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PersonResponse>> PersonFullListByPersonTypeAsync(PersonRequest personRequest = null)
        {
            var personList = await PersonListAsync(personRequest);
            return personList.ToList().toVo(null, null);
        }
    }
}
