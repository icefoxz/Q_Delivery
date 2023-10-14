using Delivery.Domains.Dto.OrderServicesDto.OrderDto;
using Delivery.Domains.Dto.OrderServicesDto.OrderTagOrReportDto;
using Delivery.Domains.Dto.OrderServicesDto.TagDto;
using Delivery.Domains.Dto.OrderServicesDto.TagManagerDto;
using Delivery.IServices.IOrderServices;

namespace Delivery.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private readonly IOrderTagOrReportServices _orderTagOrReportServices;

        public OrderController(IOrderServices orderServices, IOrderTagOrReportServices orderTagOrReportServices)
        {
            _orderServices = orderServices;
            _orderTagOrReportServices = orderTagOrReportServices;
        }

        #region 订单管理

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getOrderList")]
        public async Task<ResultMessage> GetOrderListAsync([FromQuery] OrderRequest orderRequest)
        {
            var orderList = await _orderServices.OrderFullListAsync(orderRequest);

            return new ResultMessage(true, orderList);
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("getOrderPageList")]
        public async Task<ResultMessage> GetOrderPageListAsync([FromBody] OrderRequest orderRequest)
        {
            var orderList = await _orderServices.OrderFullPageListAsync(orderRequest);

            return new ResultMessage(true, orderList);
        }
        /// <summary>
        /// 保存订单
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("saveOrder")]
        public async Task<ResultMessage> SaveOrderAsync([FromBody] OrderRequest orderRequest) => await _orderServices.OrderSaveAsync(orderRequest);

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        [HttpDelete, Route("deleteOrder")]
        public async Task<ResultMessage> DeleteTagAsync([FromQuery] OrderRequest orderRequest) => await _orderServices.OrderDeleteAsync(orderRequest);

        #endregion

        #region 订单报告信息或打标签信息管理

        /// <summary>
        /// 获取标签信息-根据订单Id
        /// </summary>
        /// <param name="orderTagOrReportRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getOrderTagOrReportList")]
        public async Task<ResultMessage> GetOrderTagOrReportListAsync([FromQuery] OrderTagOrReportRequest orderTagOrReportRequest) => new ResultMessage(true, await _orderServices.OrderTagOrReportFullListAsync(orderTagOrReportRequest));

        /// <summary>
        /// 获取标签信息分页-根据订单Id
        /// </summary>
        /// <param name="orderTagOrReportRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getOrderTagOrReportPageList")]
        public async Task<ResultMessage> GetOrderTagOrReportPageListAsync([FromQuery] OrderTagOrReportRequest orderTagOrReportRequest) => new ResultMessage(true, await _orderServices.OrderTagOrReportPageListAsync(orderTagOrReportRequest));

        /// <summary>
        /// 保存标签或报告
        /// </summary>
        /// <param name="orderTagOrReportRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("saveOrderTagOrReport")]
        public async Task<ResultMessage> SaveOrderTagOrReportAsync([FromBody] OrderTagOrReportRequest orderTagOrReportRequest) => await _orderTagOrReportServices.OrderTagOrReportSaveAsync(orderTagOrReportRequest);

        /// <summary>
        /// 作废报告
        /// </summary>
        /// <param name="orderTagOrReportRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("disableOrderTagOrReport")]
        public async Task<ResultMessage> DisableOrderTagOrReportAsync([FromBody] OrderTagOrReportRequest orderTagOrReportRequest) => await _orderTagOrReportServices.OrderTagOrReportDisableAsync(orderTagOrReportRequest);

        /// <summary>
        /// 删除报告
        /// </summary>
        /// <param name="orderTagOrReportRequest"></param>
        /// <returns></returns>
        [HttpDelete, Route("deleteOrderTagOrReport")]
        public async Task<ResultMessage> DeleteOrderTagOrReportAsync([FromQuery] OrderTagOrReportRequest orderTagOrReportRequest) => await _orderTagOrReportServices.OrderTagOrReportDeleteAsync(orderTagOrReportRequest);

        #endregion

    }
}
