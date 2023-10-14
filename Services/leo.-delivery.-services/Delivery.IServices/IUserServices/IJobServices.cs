using Delivery.Commons.Result;
using Delivery.Domains.Dto.UserServicesDto.DeptDto;
using Delivery.Domains.Dto.UserServicesDto.JobDto;
using Delivery.Domains.UserEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.IServices.IUserServices
{
    public interface IJobServices
    {
        Task<ResultMessage> JobSaveAsync(JobRequest jobRequest = null);
        Task<ResultMessage>  JobDeleteAsync(JobRequest jobRequest = null);
        Task<bool> JobAddAsync();
        Task<IEnumerable<Job>> JobListAsync(JobRequest jobRequest = null);
        Task<IEnumerable<JobResponse>> JobFullListAsync(JobRequest jobRequest = null);

    }
}
