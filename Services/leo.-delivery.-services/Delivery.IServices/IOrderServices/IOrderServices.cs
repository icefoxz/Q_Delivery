

using Delivery.Commons.Result;
using Delivery.Domains.BaseEntitys;
using Delivery.Domains.Dto.OrderServicesDto.OrderDto;
using Delivery.Domains.Dto.OrderServicesDto.OrderTagOrReportDto;
using Delivery.Domains.OrderEntitys;
using Delivery.Domains.UserEntitys;

namespace Delivery.IServices.IOrderServices
{
    public interface IOrderServices
    { 
        /// <summary>
        /// 订单基础信息查询
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        Task<IEnumerable<Order>> OrderListAsync(OrderRequest orderRequest = null);

        /// <summary>
        /// 订单基础信息分页查询
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        Task<PageList<Order>> OrderPageListAsync(OrderRequest orderRequest = null);

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        Task<IEnumerable<OrderResponse>> OrderFullListAsync(OrderRequest orderRequest = null);

        /// <summary>
        /// 订单分页查询
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        Task<PageList<OrderResponse>> OrderFullPageListAsync(OrderRequest orderRequest = null);

        /// <summary>
        /// 订单编辑保存 
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        Task<ResultMessage> OrderSaveAsync(OrderRequest orderRequest = null);

        /// <summary>
        /// 订单删除
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        Task<ResultMessage> OrderDeleteAsync(OrderRequest orderRequest = null);

        /// <summary>
        /// 订单文件保存
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        Task<ResultMessage> OrderImgUrlSaveAsync(OrderRequest orderRequest = null);

        /// <summary>
        /// 获取订单报告信息
        /// </summary>
        /// <param name="orderTagOrReportRequest"></param>
        /// <returns></returns>
        Task<IEnumerable<OrderTagOrReportResponse>> OrderTagOrReportFullListAsync(OrderTagOrReportRequest orderTagOrReportRequest = null);

        /// <summary>
        /// 获取订单报告分页信息
        /// </summary>
        /// <param name="orderTagOrReportRequest"></param>
        /// <returns></returns>
        Task<PageList<OrderTagOrReportResponse>> OrderTagOrReportPageListAsync(OrderTagOrReportRequest orderTagOrReportRequest = null);
    }
}