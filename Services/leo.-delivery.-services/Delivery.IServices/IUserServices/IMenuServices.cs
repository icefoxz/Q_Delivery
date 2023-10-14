using Delivery.Domains.Dto.UserServicesDto.MenuDto;
using Delivery.Commons.Result;
using Delivery.Domains.Dto.UserServicesDto.DeptDto; 
using Delivery.Domains.UserEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.IServices.IUserServices
{
    public interface IMenuServices
    {
        Task<ResultMessage> MenuSaveAsync(MenuRequest menuRequest = null);
        Task<ResultMessage> MenuDeleteAsync(MenuRequest menuRequest = null);
        Task<IEnumerable<Menu>> MenuListAsync(MenuRequest menuRequest = null);
        Task<IEnumerable<MenuResponse>> MenuListAsync(MenuRequest menuRequest = null, bool isLevel = true);
        Task<IEnumerable<MenuResponse>> MenuFullListAsync(MenuRequest menuRequest =null);
    }
}
