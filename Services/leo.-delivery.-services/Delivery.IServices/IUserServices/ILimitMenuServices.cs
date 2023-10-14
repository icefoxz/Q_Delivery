using Delivery.Commons.Result;
using Delivery.Domains.Dto.UserServicesDto.LimitDto;
using Delivery.Domains.Dto.UserServicesDto.MenuLimitDto;
using Delivery.Domains.UserEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.IServices.IUserServices
{
    public interface ILimitMenuServices
    {
        Task<bool> LimitMenuSaveAsync(LimitMenuRequest limitMenuRequest = null);
        Task<bool> LimitMenuDeleteAsync(LimitMenuRequest limitMenuRequest = null);
        Task<List<Limit_Menu>> LimitMenuListAsync(LimitMenuRequest limitMenuRequest = null);
        Task<IEnumerable<LimitMenuResponse>> LimitMenuFullListAsync(LimitMenuRequest limitMenuRequest = null);
    }
}
