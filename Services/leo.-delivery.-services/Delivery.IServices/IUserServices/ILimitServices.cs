using Delivery.Commons.Result;
using Delivery.Domains.Dto.UserServicesDto.LimitDto;
using Delivery.Domains.UserEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.IServices.IUserServices
{
    public interface ILimitServices
    {
        Task<ResultMessage> GetSelectMenuListAsync(LimitRequest limitRequest = null);
        Task<ResultMessage> LimitRelevancyMenuSaveAsync(LimitRequest limitRequest = null);
        Task<ResultMessage> LimitSaveAsync(LimitRequest limitRequest = null);
        Task<ResultMessage> LimitDeleteAsync(LimitRequest limitRequest = null);
        Task<List<Limit>> LimitListAsync(LimitRequest limitRequest = null);
        Task<IEnumerable<LimitResponse>> LimitFullListAsync(LimitRequest limitRequest = null);
    }
}
