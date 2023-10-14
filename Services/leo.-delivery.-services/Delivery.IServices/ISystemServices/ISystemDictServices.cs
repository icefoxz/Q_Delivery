using Delivery.Commons.Result;
using Delivery.Domains.Dto.OrderServicesDto.TagDto;
using Delivery.Domains.Dto.SystemServicesDto.SystemDict;
using Delivery.Domains.Dto.SystemServicesDto.SystemLog;
using Delivery.Domains.SystemEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.IServices.ISystemServices
{
    public interface ISystemDictServices
    {
        Task<List<SystemDict>> SystemDictListAsync(SystemDictRequest systemDictRequest = null);
        Task<IEnumerable<SystemDictResponse>> SystemDictFullListAsync(SystemDictRequest systemDictRequest = null);
        Task<ResultMessage> SystemDictSaveAsync(SystemDictRequest systemDictRequest = null);
        Task<ResultMessage> SystemDictDeleteAsync(SystemDictRequest systemDictRequest = null);
    }
}
