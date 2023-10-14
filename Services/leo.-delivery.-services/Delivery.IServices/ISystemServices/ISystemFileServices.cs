using Delivery.Commons.Result;
using Delivery.Domains.Dto.SystemServicesDto.SystemFile;
using Delivery.Domains.SystemEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.IServices.ISystemServices
{
    public interface ISystemFileServices
    {
        Task<List<SystemFile>> SystemFileListAsync(SystemFileRequest systemFileRequest = null);
        Task<ResultMessage> SystemFileSaveAsync(SystemFileRequest systemFileRequest = null);
    }
}
