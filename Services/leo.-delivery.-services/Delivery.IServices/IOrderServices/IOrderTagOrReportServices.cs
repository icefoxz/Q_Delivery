using Delivery.Commons.Result;
using Delivery.Domains.BaseEntitys;
using Delivery.Domains.Dto.OrderServicesDto.OrderDto;
using Delivery.Domains.Dto.OrderServicesDto.OrderTagOrReportDto;
using Delivery.Domains.OrderEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.IServices.IOrderServices
{
    public interface IOrderTagOrReportServices
    {
        Task<IEnumerable<Order_TagOrReport>> OrderTagOrReportListAsync(OrderTagOrReportRequest orderTagOrReportRequest = null);
        Task<PageList<OrderTagOrReportResponse>> OrderTagOrReportPageListAsync(OrderTagOrReportRequest orderTagOrReportRequest = null);
        Task<IEnumerable<OrderTagOrReportResponse>> OrderTagOrReportFullListAsync(OrderTagOrReportRequest orderTagOrReportRequest = null);
        Task<ResultMessage> OrderTagOrReportSaveAsync(OrderTagOrReportRequest orderTagOrReportRequest = null);
        Task<ResultMessage> OrderTagOrReportDeleteAsync(OrderTagOrReportRequest orderTagOrReportRequest = null);
        Task<ResultMessage> OrderTagOrReportDisableAsync(OrderTagOrReportRequest orderTagOrReportRequest = null);
    }
}
