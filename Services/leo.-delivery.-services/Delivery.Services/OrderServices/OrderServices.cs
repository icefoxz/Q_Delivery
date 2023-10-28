

using Delivery.Commons.Const;
using Delivery.Commons.Cookie;
using Delivery.Domains.BaseEntitys;
using Delivery.Domains.Dto.OrderServicesDto.Lingau_OptLogDto;
using Delivery.Domains.Dto.OrderServicesDto.OrderDto;
using Delivery.Domains.Dto.OrderServicesDto.OrderTagOrReportDto;
using Delivery.Domains.Dto.SystemServicesDto.SystemFile;
using Delivery.EntityFramework.Core.DbContexts;
using Delivery.IServices.IOrderServices;
using Delivery.IServices.ISystemServices;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog.Fluent;
using System.Diagnostics.Metrics;

namespace Delivery.Services.OrderServices
{
    [AutoInject(typeof(IOrderServices), InjectTypeEnum.Scope)]
    public class OrderServices : IOrderServices
    {
        private readonly ILogger<OrderServices> _logger;
        private readonly OrderDbContext _orderDbContext;
        private readonly SystemDbContext _systemDbContext;
        private readonly IOrderTagOrReportServices _orderTagServices;

        private readonly ITagServices _tagServices;
        private readonly ITagManagerServices _tagManagerServices;
        private readonly ISystemFileServices _systemFileServices;
        private readonly IMapper _mapper;
        public OrderServices(OrderDbContext orderDbContext, SystemDbContext systemDbContext, IMapper mapper, IOrderTagOrReportServices orderTagServices, ITagManagerServices tagManagerServices, ITagServices tagServices, ISystemFileServices systemFileServices, ILogger<OrderServices> logger)
        {
            _mapper = mapper;
            _orderDbContext = orderDbContext;
            _systemDbContext = systemDbContext;
            _orderTagServices = orderTagServices;
            _tagManagerServices = tagManagerServices;
            _tagServices = tagServices;
            _systemFileServices = systemFileServices;
            _logger = logger;
        }

        /// <summary>
        /// 整合订单分页信息
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        public async Task<PageList<OrderResponse>> OrderFullPageListAsync(OrderRequest orderRequest = null)
        {
            // 获取订单信息
            var orderList = await OrderPageListAsync(orderRequest);
            // 订单Id数组
            var orderIdArray = orderList.CurrentPageData.Select(item => item.Id).ToList();
            // 获取订单的所有标签
            var orderTagList = await _orderTagServices.OrderTagOrReportListAsync(new OrderTagOrReportRequest() { order_IdArray = orderIdArray });

            // 文件信息
            var fileList = await _systemFileServices.SystemFileListAsync(new Domains.Dto.SystemServicesDto.SystemFile.SystemFileRequest()
            {
                data_Type = SystemFileConst.orderImgUrl,
                data_IdArray = orderIdArray.Select(item => item.ToString())?.ToList() ?? new List<string>(),
            });

            // 关系映射
            var orderResponses = _mapper.Map<List<OrderResponse>>(orderList.CurrentPageData);
            // 数据整合
            var objData = orderResponses.toVo(orderTagList, fileList);
            var resultData = new List<OrderResponse>();
            var pageData = new PageList<OrderResponse>();
            if (orderRequest.tagManager_IdList?.Any() ?? false)
            {
                if (orderRequest.tag_Type.ToString() == TagOwningType.Tag.ToString())
                {
                    foreach (var itemTemp in objData)
                    {
                        var isExist = itemTemp.orderTagManagerIdList?
                                .Any(
                                    item =>
                                    orderRequest.tagManager_IdList.Contains(item)
                                    ) ?? false;
                        if (isExist)
                            resultData.Add(itemTemp);
                    }

                }
                else if (orderRequest.tag_Type.ToString() == TagOwningType.TagReport.ToString())
                {
                    foreach (var itemTemp in objData)
                    {
                        var isExist = itemTemp.orderTagReportManagerIdList?.Any(item => orderRequest.tagManager_IdList.Contains(item)) ?? false;
                        if (isExist)
                            resultData.Add(itemTemp);
                    }
                }

            }
            else
                resultData = objData.ToList();

            pageData.CurrentPageData = resultData.Skip(orderRequest.current_PageIndex * orderRequest.page_Size)
             .Take(orderRequest.page_Size).ToList();//在这里进行分页
            pageData.totalPages = orderList.totalPages;
            pageData.total = orderList.total;
            pageData.page_Size = orderList.page_Size;
            pageData.current_PageIndex = orderList.current_PageIndex;
            return pageData;
        }

        /// <summary>
        /// 整合订单信息
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OrderResponse>> OrderFullListAsync(OrderRequest orderRequest = null)
        {
            try
            {
                // 获取订单信息
                var orderList = await OrderListAsync(orderRequest);
                // 订单Id数组
                var orderIdArray = orderList.Select(item => item.Id).ToList();
                //// 获取订单的所有标签
                var orderTagList = await _orderTagServices.OrderTagOrReportListAsync(new OrderTagOrReportRequest() { order_IdArray = orderIdArray });
                // 关系映射
                var orderReuturn = _mapper.Map<List<OrderResponse>>(orderList);
                // 数据整合
                return orderReuturn.toVo(orderTagList);

            }
            catch (Exception ex)
            {
                _logger.LogError($"[订单查询]-异常：{ex.Message}");
                return default;
            }
        }

        /// <summary>
        /// 订单标签基础信息-分页
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        public async Task<PageList<Order>> OrderPageListAsync(OrderRequest orderRequest = null)
        {
            orderRequest.current_PageIndex -= 1;
            var totalCount = await _orderDbContext.Orders.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)orderRequest.page_Size);

            IQueryable<Order> pagListQuery = _orderDbContext.Orders.AsNoTracking();

            // 订单Id
            if (!orderRequest.Id.Guid_IsEmpty())
                pagListQuery = pagListQuery.Where(item => orderRequest.Id == item.Id);
            else if (orderRequest.IdList?.Any() ?? false)
                pagListQuery = pagListQuery.Where(item => orderRequest.IdList.Contains(item.Id));

            // 订单名称
            if (string.IsNullOrWhiteSpace(orderRequest?.order_Name) == false)
                pagListQuery = pagListQuery.Where(item => item.order_Name.Contains(orderRequest.order_Name));
            // 配送人
            if (string.IsNullOrWhiteSpace(orderRequest?.order_RiderName) == false)
                pagListQuery = pagListQuery.Where(item => item.order_RiderName.Contains(orderRequest.order_RiderName));
            // 配送人手机号
            if (string.IsNullOrWhiteSpace(orderRequest?.order_RiderPhone) == false)
                pagListQuery = pagListQuery.Where(item => item.order_RiderPhone.Contains(orderRequest.order_RiderPhone));
            // 收货人
            if (string.IsNullOrWhiteSpace(orderRequest?.order_ReceiverName) == false)
                pagListQuery = pagListQuery.Where(item => item.order_ReceiverName.Contains(orderRequest.order_ReceiverName));
            // 收货人手机号
            if (string.IsNullOrWhiteSpace(orderRequest?.order_ReceiverPhone) == false)
                pagListQuery = pagListQuery.Where(item => item.order_ReceiverPhone.Contains(orderRequest.order_ReceiverPhone));
            // 订单状态
            if (string.IsNullOrWhiteSpace(orderRequest?.order_Status) == false && orderRequest?.order_Status != "''")
                pagListQuery = pagListQuery.Where(item => item.order_Status.Contains(orderRequest.order_Status));

            if (orderRequest.begin_Time != null)
                pagListQuery = pagListQuery.Where(item => item.create_Time > orderRequest.begin_Time);

            if (orderRequest.end_Time != null)
                pagListQuery = pagListQuery.Where(item => item.create_Time < orderRequest.end_Time);

            var pagList = await pagListQuery.OrderBy(item => item.create_Time)

             .ToListAsync();

            return new PageList<Order>()
            {
                current_PageIndex = orderRequest.current_PageIndex,// 当前页
                page_Size = orderRequest.page_Size,// 当前页数量
                CurrentPageData = pagList,// 当前页数据
                total = totalCount,// 总数量
                totalPages = totalPages,// 总页数
            };

        }

        /// <summary>
        ///  订单基础信息
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Order>> OrderListAsync(OrderRequest orderRequest = null)
        {
            IQueryable<Order> orderQuery = _orderDbContext.Orders.AsNoTracking();

            // 订单Id
            if (!orderRequest.Id.Guid_IsEmpty())
                orderQuery = orderQuery.Where(item => orderRequest.Id == item.Id);
            else if (orderRequest.IdList?.Any() ?? false)
                orderQuery = orderQuery.Where(item => orderRequest.IdList.Contains(item.Id));

            // 订单名称
            if (string.IsNullOrWhiteSpace(orderRequest?.order_Name) == false)
                orderQuery = orderQuery.Where(item => item.order_Name.Contains(orderRequest.order_Name));
            // 配送人
            if (string.IsNullOrWhiteSpace(orderRequest?.order_RiderName) == false)
                orderQuery = orderQuery.Where(item => item.order_RiderName.Contains(orderRequest.order_RiderName));
            // 配送人手机号
            if (string.IsNullOrWhiteSpace(orderRequest?.order_RiderPhone) == false)
                orderQuery = orderQuery.Where(item => item.order_RiderPhone.Contains(orderRequest.order_RiderPhone));
            // 收货人
            if (string.IsNullOrWhiteSpace(orderRequest?.order_ReceiverName) == false)
                orderQuery = orderQuery.Where(item => item.order_ReceiverName.Contains(orderRequest.order_ReceiverName));
            // 收货人手机号
            if (string.IsNullOrWhiteSpace(orderRequest?.order_ReceiverPhone) == false)
                orderQuery = orderQuery.Where(item => item.order_ReceiverPhone.Contains(orderRequest.order_ReceiverPhone));
            // 订单状态
            if (string.IsNullOrWhiteSpace(orderRequest?.order_Status) == false && orderRequest?.order_Status != "''")
                orderQuery = orderQuery.Where(item => item.order_Status.Contains(orderRequest.order_Status));

            var orders = await orderQuery.OrderBy(item => item.create_Time).ToListAsync();

            return orders;
        }

        /// <summary>
        /// 订单保存
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> OrderSaveAsync(OrderRequest orderRequest = null)
        {
            var result = true;
            var order = _mapper.Map<Order>(orderRequest);
            try
            {
                var orderTemp = await _orderDbContext.Orders
                                .AsNoTracking()
                                .FirstOrDefaultAsync(item =>
                                    item.Id == orderRequest.Id
                                    );

                if (orderTemp != null)
                    order.Id = orderTemp.Id;

                //order.order_CreateDeptId = UserInfoCookie.dept_Id ?? default;
                //order.order_CreateDeptName = UserInfoCookie.dept_Name ?? default;

                if (orderRequest.Id.Guid_IsEmpty())
                    await _orderDbContext.AddAsync(order);
                else
                    _orderDbContext.Update(order);
                // 处理文件
                if (orderRequest.fileIdList?.Any() ?? false)
                {
                    await _systemDbContext.SystemFiles
                        .Where(item => orderRequest.fileIdList.Contains(item.Id))
                        .ExecuteUpdateAsync(
                            item =>
                            item.SetProperty(
                                pro =>
                                pro.data_Id,
                                pro => order.Id.ToString()
                                )
                            );

                    // 删除订单中有的，入参里没有的
                    if (orderRequest.Id != null)
                    {
                        await _systemDbContext.SystemFiles
                            .Where(
                                item =>
                                item.data_Id == orderRequest.Id.ToString() &&
                                !orderRequest.fileIdList.Contains(item.Id)
                                )
                            .ExecuteDeleteAsync();

                    }
                }

                result &= await _orderDbContext.SaveChangesAsync() > 0;
                // 处理订单报告信息
                if (string.IsNullOrEmpty(orderRequest.order_TagOrReport_Name) == false)
                    await _orderTagServices.OrderTagOrReportSaveAsync(new OrderTagOrReportRequest()
                    {
                        order_Id = order.Id,
                        order_TagOrReport_Name = orderRequest.order_TagOrReport_Name,
                        tagManager_Id = orderRequest.tagManager_Id,
                        tag_Type = (TagOwningType?)Enum.Parse(typeof(TagOwningType), orderRequest.tag_Type),
                    });

                return new ResultMessage(result, result ? "保存成功" : "保存失败");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[订单保存]-异常：{ex.Message}");
                return new ResultMessage(false, "保存失败！");
            }
        }

        /// <summary>
        /// 订单删除
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> OrderDeleteAsync(OrderRequest orderRequest = null)
        {
            var result = true;

            try
            {
                // 验证是否绑定过标签 
                if ((await _orderDbContext.Order_TagOrReports.Where(item => item.order_Id == orderRequest.Id).ToListAsync())?.Any() ?? false)
                    return new ResultMessage(false, "该订单已创建标签不允许删除");

                var orderInfo = await _orderDbContext.Orders.SingleOrDefaultAsync(item => item.Id == orderRequest.Id);
                if (orderInfo != null)
                {
                    _orderDbContext.Remove(orderInfo);
                    result &= (await _orderDbContext.SaveChangesAsync()) > 0;
                }
                return new ResultMessage(result, result ? "删除成功！" : "删除失败！");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[订单删除]-异常：{ex.Message}");
                return new ResultMessage(false, "删除失败！");
            }

        }

        #region 订单报告信息

        /// <summary>
        /// 获取订单报告信息
        /// </summary>
        /// <param name="orderTagOrReportRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OrderTagOrReportResponse>> OrderTagOrReportFullListAsync(OrderTagOrReportRequest orderTagOrReportRequest = null)
        {
            // 订单报告信息
            var orderTagList = await _orderTagServices.OrderTagOrReportListAsync(orderTagOrReportRequest);

            if (!orderTagList?.Any() ?? false)
                return new List<OrderTagOrReportResponse>();

            // 订单信息
            var orderIdList = orderTagList?.Select(item => (Guid?)item.order_Id).ToList();
            var orderList = await OrderListAsync(new OrderRequest() { IdList = orderIdList });

            // 订单报告标签关联信息
            var tagManagerIdList = orderTagList?.Select(item => (Guid?)item.tagManager_Id).ToList();
            var tagManagerList = await _tagManagerServices.TagManagerListAsync(new TagManagerRequest() { IdList = tagManagerIdList });

            // 关联的标签信息
            var tagIdList = tagManagerList?.Select(item => item.tag_Id).ToList();
            var tagList = await _tagServices.TagListAsync(new TagRequest() { IdList = tagIdList });

            return orderTagList?.toVo(orders: orderList, tagManagerList, tags: tagList);

        }

        /// <summary>
        /// 获取订单报告信息-分页
        /// </summary>
        /// <param name="orderTagOrReportRequest"></param>
        /// <returns></returns>
        public async Task<PageList<OrderTagOrReportResponse>> OrderTagOrReportPageListAsync(OrderTagOrReportRequest orderTagOrReportRequest = null)
        {
            try
            {
                // 订单报告信息
                var orderTagList = await _orderTagServices.OrderTagOrReportPageListAsync(orderTagOrReportRequest);
                //var resultData = _mapper.Map<PageList<OrderTagOrReportResponse>>(orderTagList);

                if (!orderTagList.CurrentPageData?.Any() ?? false || orderTagList is null)
                    return new PageList<OrderTagOrReportResponse>();

                // 订单信息
                var orderIdList = orderTagList.CurrentPageData?.Select(item => (Guid?)item.order_Id).ToList();
                var orderList = await OrderListAsync(new OrderRequest() { IdList = orderIdList });

                // 订单报告标签关联信息
                var tagManagerIdList = orderTagList.CurrentPageData?.Select(item => (Guid?)item.tagManager_Id).ToList();
                var tagManagerList = await _tagManagerServices.TagManagerListAsync(new TagManagerRequest() { IdList = tagManagerIdList });

                // 关联的标签信息
                var tagIdList = tagManagerList?.Select(item => item.tag_Id).ToList();
                var tagList = await _tagServices.TagListAsync(new TagRequest() { IdList = tagIdList });

                orderTagList.CurrentPageData = orderTagList.CurrentPageData?.toVo(orders: orderList, tagManagerList, tags: tagList).ToList();

                return orderTagList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[订单分页查询]-异常：{ex.Message}");
                return default;
            }
        }

        #endregion

        /// <summary>
        /// 订单保存
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultMessage> OrderImgUrlSaveAsync(OrderRequest orderRequest = null)
        {
            var result = true;

            try
            {
                if (string.IsNullOrWhiteSpace(orderRequest.order_imgUrl))
                    return new ResultMessage(false, "The picture information cannot be empty!");

                var orderItem = await _orderDbContext.Orders.FirstOrDefaultAsync(item => item.Id == orderRequest.Id);
                if (orderItem is null)
                    return new ResultMessage(false, "The order does not exist!");

                if (orderRequest.isUploadImg)
                    orderItem.order_ImgUrls.Add(orderRequest.order_imgUrl);
                else
                {
                    if (orderItem.order_ImgUrls.Contains(orderRequest.order_imgUrl))
                        orderItem.order_ImgUrls.Remove(orderRequest.order_imgUrl);
                }

                _orderDbContext.Update(orderItem);
                result &= (await _orderDbContext.SaveChangesAsync()) > 0;

                return new ResultMessage(result, result ? "Success！" : "Failed！");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[订单图片保存]-异常：{ex.Message}");
                return new ResultMessage(false, "Failed！");
            }
        }


    }
}
