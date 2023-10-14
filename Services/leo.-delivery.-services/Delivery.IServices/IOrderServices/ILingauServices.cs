using Delivery.Commons.Result;
using Delivery.Domains.Dto.OrderServicesDto.Lingau_OptLogDto;
using Delivery.Domains.Dto.OrderServicesDto.LingauDto;
using Delivery.Domains.Dto.OrderServicesDto.TagDto;
using Delivery.Domains.OrderEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.IServices.IOrderServices
{
    public interface ILingauServices
    {

        #region Lingau

        Task<List<Lingau>> LingauListAsync(LingauRequest lingauRequest = null);
        Task<IEnumerable<LingauResponse>> LingauFullListAsync(LingauRequest lingauRequest = null);
        Task<ResultMessage> LingauSaveAsync(LingauRequest lingauRequest = null);
        Task<ResultMessage> LingauDeleteAsync(LingauRequest lingauRequest = null);

        #endregion

        #region Lingau操作日志

        Task<List<Lingau_OptLog>> LingauOptLogListAsync(LingauOptLogRequest lingauOptLogRequest = null);
        Task<IEnumerable<LingauOptLogResponse>> LingauOptLogFullListAsync(LingauOptLogRequest lingauOptLogRequest = null);
        Task<bool> LingauOptLogSaveAsync(LingauOptLogRequest lingauOptLogRequest = null);
        Task<ResultMessage> LingauOptLogDeleteAsync(LingauOptLogRequest lingauOptLogRequest = null);

        #endregion


    }
}
