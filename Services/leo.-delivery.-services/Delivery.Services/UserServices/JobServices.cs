using Delivery.Commons.Result;
using Delivery.Domains.Dto.UserServicesDto.DeptDto;
using Delivery.Domains.Dto.UserServicesDto.JobDto;
using Delivery.Domains.Dto.Vo;
using Delivery.EntityFramework.Core.Migrations;
using Delivery.IServices.IRepository;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Delivery.Services.UserServices
{
    [AutoInject(typeof(IJobServices), InjectTypeEnum.Scope)]
    public class JobServices : IJobServices
    {
        private readonly UserDbContext _userDbContext;
        private readonly IDeptServices _deptServices;
        private readonly ILogger<JobServices> _logger;
        public JobServices(UserDbContext userDbContext, IDeptServices deptServices,
            ILogger<JobServices> logger)
        {
            _userDbContext = userDbContext;
            _deptServices = deptServices;
            _logger = logger;
        }
        public async Task<bool> JobAddAsync()
        {
            await _userDbContext.Jobs.AddAsync(new Job()
            {
                job_Name = "测试职位" + DateTime.Now,
                create_Time = DateTime.Now,
                deptInfo = await _userDbContext.Depts.FirstOrDefaultAsync() ?? default
                //dept_Id =  _userDbContext.Depts?.FirstOrDefault()?.Id ?? default
            });
            return await _userDbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 获取基础职位信息
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Job>> JobListAsync(JobRequest jobRequest = null)
        {
            IQueryable<Job> jobQuery = _userDbContext.Jobs.AsNoTracking();
            //jobQuery= jobQuery.Where(item => !string.IsNullOrWhiteSpace(jobRequest.job_Name) && item.job_Name.Contains(jobRequest.job_Name.Trim()));

            if (string.IsNullOrWhiteSpace(jobRequest?.job_Name) == false)
                jobQuery = jobQuery.Where(item => item.job_Name.Contains(jobRequest.job_Name));

            if (jobRequest?.dept_IdArray?.Any() ?? false)
                jobQuery = jobQuery.Where(item => jobRequest.dept_IdArray.Contains((Guid)item.dept_Id));

            else if (jobRequest?.dept_Id != Guid.Empty)
                jobQuery = jobQuery.Where(item => item.dept_Id == jobRequest!.dept_Id);

            else if (jobRequest?.IdList?.Any() ?? false)
                jobQuery = jobQuery.Where(item => jobRequest.IdList.Contains(item.Id));

            var jobs = await jobQuery.OrderBy(item => item.expand_Order).OrderBy(item => item.create_Time).ToListAsync();

            return jobs;
        }

        /// <summary>
        /// 获取基础职位信息
        /// </summary>
        public async Task<IEnumerable<JobResponse>> JobFullListAsync(JobRequest jobRequest = null)
        {
            // 获取所有单位的下级
            var deptList = (await _deptServices.DeptFullListAsync(new DeptRequest() { dept_ParentId = jobRequest.dept_Id })).Data as List<DeptResponse>;

            // 获取基础数据
            var jobList = await JobListAsync(new JobRequest() { dept_IdArray = deptList?.Select(item => item.Id)?.ToList() ?? default });

            return jobList.ToList().toVo(deptList);
        }

        /// <summary>
        /// 职位保存
        /// </summary>
        /// <param name="jobRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultMessage> JobSaveAsync(JobRequest jobRequest = null)
        {
            var result = true;
            var isVerifyDept = false;
            var job = new Job();
            try
            {
                if (jobRequest.Id == Guid.Empty)
                {
                    //Insert
                    isVerifyDept = true;
                    job.job_Name = jobRequest.job_Name ?? "";
                    job.dept_Id = jobRequest.dept_Id;
                }
                else
                {
                    // Edit
                    var jobTemp = await _userDbContext.Jobs?.FirstOrDefaultAsync(item => item.Id == jobRequest.Id) ?? new Job();
                    if (jobTemp.job_Name != jobTemp.job_Name)
                        isVerifyDept = true;
                    job = jobTemp;
                    job.job_Name = jobRequest.job_Name ?? "";
                    job.dept_Id = jobRequest.dept_Id;
                }
                // 验证名称
                if (isVerifyDept)
                    if (await _userDbContext.Jobs.AnyAsync(item => item.job_Name == jobRequest.job_Name))
                        return new ResultMessage(false, $"{jobRequest.job_Name}已存在该名称相同的职位");

                if (jobRequest.Id == Guid.Empty)
                    await _userDbContext.Jobs.AddAsync(job);
                else
                    _userDbContext.Jobs.Update(job);

                result &= await _userDbContext.SaveChangesAsync() > 0;

                return new ResultMessage(result, result ? "保存成功" : "保存失败");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[职位保存异常]:{ex.Message}");
                return new ResultMessage(false, "保存失败");
            }
        }

        /// <summary>
        /// 职位删除
        /// </summary>
        /// <param name="jobRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> JobDeleteAsync(JobRequest jobRequest = null)
        {
            var result = true;
            try
            {

                // 验证是否绑定过职位 
                if ((await _userDbContext.Persons.Where(item => item.dept_Id == jobRequest.Id).ToListAsync())?.Any() ?? false)
                    return new ResultMessage(false, "该单位已绑定人员不允许删除");

                var jobInfo = await _userDbContext.Jobs.SingleOrDefaultAsync(item => item.Id == jobRequest.Id);
                if (jobInfo != null)
                {
                    _userDbContext.Remove(jobInfo);
                    result &= (await _userDbContext.SaveChangesAsync()) > 0;
                }
                return new ResultMessage(result, result ? "删除成功！" : "删除失败！");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[职位保存异常]:{ex.Message}");
                return new ResultMessage(false, "保存失败");
            }
        }

    }

    [AutoInject(typeof(IRepository<Job>), InjectTypeEnum.Scope)]
    public class JobServicess : IRepository<Job>
    {
        private readonly UserDbContext _userDbContext;

        public JobServicess(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public void Create(Job item)
        {
            throw new NotImplementedException();
        }

        public void CreateList(List<Job> itemList)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteList(List<Job> itemList)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }

        public Job Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Job> GetAll()
        {
            return _userDbContext.Jobs.ToList();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Job item)
        {
            throw new NotImplementedException();
        }

        public void UpdateList(List<Job> itemList)
        {
            throw new NotImplementedException();
        }
    }

}
