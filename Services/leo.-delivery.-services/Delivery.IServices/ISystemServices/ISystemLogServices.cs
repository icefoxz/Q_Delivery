using Delivery.Domains.BaseEntitys;
using Delivery.Domains.Dto.OrderServicesDto.TagDto;
using Delivery.Domains.Dto.SystemServicesDto.SystemLog;
using Delivery.Domains.OrderEntitys;
using Delivery.Domains.SystemEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.IServices.ISystemServices
{
    public interface ISystemLogServices
    {
        Task<List<SystemLog>> SystemLogListAsync(SystemLogRequest systemLogRequest = null);
        Task<PageList<SystemLog>> SystemLogPageListAsync(SystemLogRequest systemLogRequest = null);
        Task SystemLogAddAsync(SystemLog systemLog = null);
    }
}
