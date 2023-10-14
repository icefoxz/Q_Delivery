using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : BaseApiController
    {
        private readonly IJobServices _jobServices;
        public JobController(IJobServices jobServices)
        {
            _jobServices = jobServices;
        }

        #region 职位管理

        /// <summary>
        /// 获取职位信息
        /// </summary>
        /// <param name="jobRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getJobList")]
        public async Task<ResultMessage> GetJobListAsync([FromQuery] JobRequest jobRequest) => new ResultMessage(true, await _jobServices.JobFullListAsync(jobRequest));


        /// <summary>
        /// 保存职位
        /// </summary>
        /// <param name="jobRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("saveJob")]
        public async Task<ResultMessage> SaveJobAsync([FromBody] JobRequest jobRequest) => await _jobServices.JobSaveAsync(jobRequest);

        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="jobRequest"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultMessage> DeleteJobAsync([FromQuery] JobRequest jobRequest) => await _jobServices.JobDeleteAsync(jobRequest);

        #endregion
    }
}
