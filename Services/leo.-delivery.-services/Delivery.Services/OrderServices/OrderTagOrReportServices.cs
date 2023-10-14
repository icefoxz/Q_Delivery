using Delivery.Domains.BaseEntitys;
using Delivery.Domains.Dto.OrderServicesDto.OrderDto;
using Delivery.Domains.Dto.OrderServicesDto.OrderTagOrReportDto;
using Delivery.Domains.Dto.UserServicesDto.JobDto;
using Delivery.Domains.Dto.Vo;
using Delivery.Domains.SystemEntitys;
using Delivery.EntityFramework.Core.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services.OrderServices
{
    [AutoInject(typeof(IOrderTagOrReportServices), InjectTypeEnum.Scope)]
    public class OrderTagOrReportServices : IOrderTagOrReportServices
    {
        private readonly ITagManagerServices _tagManagerServices;
        private readonly OrderDbContext _orderDbContext;
        private readonly ITagServices _tagServices;
        private readonly IMapper _mapper;

        public OrderTagOrReportServices(OrderDbContext orderDbContext, ITagManagerServices tagManagerServices, ITagServices tagServices
            , IMapper mapper)
        {
            _tagManagerServices = tagManagerServices;
            _orderDbContext = orderDbContext;
            _tagServices = tagServices;
            _mapper = mapper;
        }

        /// <summary>
        /// 订单标签信息
        /// </summary>
        /// <param name="orderTagOrReportRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OrderTagOrReportResponse>> OrderTagOrReportFullListAsync(OrderTagOrReportRequest orderTagOrReportRequest = null)
        {
            // 订单报告信息
            var orderTagList = await OrderTagOrReportListAsync(orderTagOrReportRequest);

            if (orderTagList?.Any() ?? false)
                return new List<OrderTagOrReportResponse>();


            //// 订单报告标签关联信息
            //var tagManagerIdList = orderTagList?.Select(item => (Guid?)item.tagManager_Id).ToList();
            //var tagManagerList = await _tagManagerServices.TagManagerListAsync(new TagManagerRequest() { IdList = tagManagerIdList });

            //// 关联的标签信息
            //var tagIdList = tagManagerList?.Select(item => item.tag_Id).ToList();
            //var tagList = await _tagServices.TagListAsync(new TagRequest() { IdList = tagIdList });

            //return orderTagList?.toVo(orders: orderList, tags: tagList);
            return orderTagList?.toVo(orders: null, tags: null);

        }

        /// <summary>
        ///  订单标签基础信息
        /// </summary>
        /// <param name="orderTagOrReportRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Order_TagOrReport>> OrderTagOrReportListAsync(OrderTagOrReportRequest orderTagOrReportRequest = null)
        {
            IQueryable<Order_TagOrReport> orderTagQuery = _orderDbContext.Order_TagOrReports.AsNoTracking();

            // 订单IdArray
            if (orderTagOrReportRequest?.order_IdArray?.Any() ?? false)
                orderTagQuery = orderTagQuery.Where(item => orderTagOrReportRequest.order_IdArray.Contains(item.order_Id));

            // 订单Id
            else if (orderTagOrReportRequest?.order_Id != Guid.Empty)
                orderTagQuery = orderTagQuery.Where(item => item.order_Id == orderTagOrReportRequest!.order_Id);

            // 订单标签Id
            if (orderTagOrReportRequest?.tagManager_Id.Guid_NoEmpty() == false)
                orderTagQuery = orderTagQuery.Where(item => item.tagManager_Id == orderTagOrReportRequest.tagManager_Id);

            // 订单报告或标签名称
            if (string.IsNullOrWhiteSpace(orderTagOrReportRequest?.order_TagOrReport_Name) == false)
                orderTagQuery = orderTagQuery.Where(item => item.order_TagOrReport_Name.Contains(orderTagOrReportRequest.order_TagOrReport_Name));

            // 所属报告
            if (string.IsNullOrEmpty(orderTagOrReportRequest?.tag_Type?.ToString()) == false)
                orderTagQuery = orderTagQuery.Where(item => item.tag_Type == orderTagOrReportRequest.tag_Type.ToString());

            // 报告描述
            if (string.IsNullOrEmpty(orderTagOrReportRequest?.expand_Desc?.ToString()) == false)
                orderTagQuery = orderTagQuery.Where(item => (item.expand_Desc.Contains(orderTagOrReportRequest.expand_Desc)));

            // 不为null时查询指定状态的值
            if (orderTagOrReportRequest?.isEnable != null)
                orderTagQuery = orderTagQuery.Where(item => item.isEnable == orderTagOrReportRequest.isEnable);

            var orderTags = await orderTagQuery.OrderBy(item => item.create_Time).ToListAsync();

            return orderTags;
        }

        /// <summary>
        ///  订单标签基础信息-分页
        /// </summary>
        /// <param name="orderTagOrReportRequest"></param>
        /// <returns></returns>
        public async Task<PageList<OrderTagOrReportResponse>> OrderTagOrReportPageListAsync(OrderTagOrReportRequest orderTagOrReportRequest = null)
        {
            orderTagOrReportRequest.current_PageIndex -= 1;
            IQueryable<Order_TagOrReport> totalQuery = _orderDbContext.Order_TagOrReports;

            IQueryable<Order_TagOrReport> pagListQuery = _orderDbContext.Order_TagOrReports.AsNoTracking();

            // 订单IdArray
            if (orderTagOrReportRequest?.order_IdArray?.Any() ?? false)
                pagListQuery = pagListQuery.Where(item => orderTagOrReportRequest.order_IdArray.Contains(item.order_Id));

            // 订单Id
            else if (orderTagOrReportRequest?.order_Id != Guid.Empty)
            {
                pagListQuery = pagListQuery.Where(item => item.order_Id == orderTagOrReportRequest!.order_Id);
                totalQuery = totalQuery.Where(item => item.order_Id == orderTagOrReportRequest.order_Id);
            }
            // 订单标签Id
            if (orderTagOrReportRequest?.tagManager_Id.Guid_NoEmpty() == false)
                pagListQuery = pagListQuery.Where(item => item.tagManager_Id == orderTagOrReportRequest.tagManager_Id);

            // 订单报告或标签名称
            if (string.IsNullOrWhiteSpace(orderTagOrReportRequest?.order_TagOrReport_Name) == false)
                pagListQuery = pagListQuery.Where(item => item.order_TagOrReport_Name.Contains(orderTagOrReportRequest.order_TagOrReport_Name));

            // 所属报告
            if (string.IsNullOrEmpty(orderTagOrReportRequest?.tag_Type?.ToString()) == false)
            {
                pagListQuery = pagListQuery.Where(item => item.tag_Type == orderTagOrReportRequest.tag_Type.ToString());
                totalQuery = totalQuery.Where(item => item.tag_Type == orderTagOrReportRequest.tag_Type.ToString());
            }

            // 报告描述
            if (string.IsNullOrEmpty(orderTagOrReportRequest?.expand_Desc?.ToString()) == false)
                pagListQuery = pagListQuery.Where(item => (item.expand_Desc.Contains(orderTagOrReportRequest.expand_Desc)));

            // 不为null时查询指定状态的值
            if (orderTagOrReportRequest?.isEnable != null)
                pagListQuery = pagListQuery.Where(item => item.isEnable == orderTagOrReportRequest.isEnable);

            var pagList = await pagListQuery.OrderBy(item => item.create_Time)
          .Skip(orderTagOrReportRequest.current_PageIndex * orderTagOrReportRequest.page_Size)
          .Take(orderTagOrReportRequest.page_Size)
          .ToListAsync();

            var resultData = pagList.Select(item => _mapper.Map<OrderTagOrReportResponse>(item)).ToList();
            var totalCount = await totalQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)orderTagOrReportRequest.page_Size);

            return new PageList<OrderTagOrReportResponse>()
            {
                current_PageIndex = orderTagOrReportRequest.current_PageIndex,// 当前页
                page_Size = orderTagOrReportRequest.page_Size,// 当前页数量
                CurrentPageData = resultData,// 当前页数据
                total = totalCount,// 总数量
                totalPages = totalPages,// 总页数
            };

        }

        /// <summary>
        /// 订单标签保存-1：标签，2-报告
        /// </summary>
        /// <param name="orderTagOrReportRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> OrderTagOrReportSaveAsync(OrderTagOrReportRequest orderTagOrReportRequest = null)
        {
            var result = true;
            if (orderTagOrReportRequest.order_Id == Guid.Empty)
                return new ResultMessage(false, "保存失败，订单编号不能为空！");

            var order_TagOrReportList = await _orderDbContext.Order_TagOrReports.ToListAsync();
            if (orderTagOrReportRequest.tag_Type == TagOwningType.Tag)
            {
                // 订单标签
                // 获取该订单当前已存在的订单，删掉
                var exitsList = order_TagOrReportList.Where(item => item.order_Id == orderTagOrReportRequest.order_Id).ToList();

                // 删除取消选中的数据
                var deleteList = exitsList.Where(item => orderTagOrReportRequest.tagManager_IdList.Contains(item.tagManager_Id) == false).ToList();

                // 添加新选中的数据
                var insertIdList = orderTagOrReportRequest.tagManager_IdList.Where(item => exitsList.Select(tag => (Guid?)tag.tagManager_Id).Contains(item) == false);

                // 获取标签关联数据
                var tagManagerList = (await _orderDbContext.Tag_Managers.Where(item => insertIdList.Contains(item.Id)).ToListAsync());
                var tagIdList = tagManagerList.Select(item => item.tag_Id);

                // 获取标签数据
                var tagList = await _orderDbContext.Tags.Where(item => tagIdList.Contains(item.Id)).ToListAsync();
                var insertList = insertIdList.ToList().Select(item => new Order_TagOrReport()
                {
                    order_TagOrReport_Name = tagList.FirstOrDefault(tag => tag.Id == (tagManagerList.FirstOrDefault(tagMana => tagMana.Id == (Guid)item)?.tag_Id))?.tag_Name ?? default,
                    tagManager_Id = (Guid)item,
                    order_Id = orderTagOrReportRequest.order_Id,
                    tag_Type = TagOwningType.Tag.ToString(),
                });

                if (insertList?.Any() ?? false)
                    await _orderDbContext.Order_TagOrReports.AddRangeAsync(insertList);
                if (deleteList?.Any() ?? false)
                    _orderDbContext.Order_TagOrReports.RemoveRange(deleteList);
            }
            else
            {
                // 订单报告
                var orderTag = order_TagOrReportList?.FirstOrDefault(item => item.Id == orderTagOrReportRequest.Id) ?? new Order_TagOrReport();

                orderTag.order_TagOrReport_Name = orderTagOrReportRequest?.order_TagOrReport_Name ?? default;
                orderTag.tagManager_Id = orderTagOrReportRequest.tagManager_Id ?? default;
                orderTag.tag_Type = TagOwningType.TagReport.ToString();
                orderTag.order_Id = orderTagOrReportRequest.order_Id;

                if (orderTagOrReportRequest.Id == Guid.Empty)
                    await _orderDbContext.Order_TagOrReports.AddAsync(orderTag);
                else
                    _orderDbContext.Order_TagOrReports.Update(orderTag);

            }

            result &= await _orderDbContext.SaveChangesAsync() > 0;

            return new ResultMessage(result, result ? "保存成功" : "保存失败");
        }

        /// <summary>
        /// 订单标签删除
        /// </summary>
        /// <param name="orderTagOrReportRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> OrderTagOrReportDeleteAsync(OrderTagOrReportRequest orderTagOrReportRequest = null)
        {
            var result = true;

            var orderTagInfo = await _orderDbContext.Order_TagOrReports.SingleOrDefaultAsync(item => item.Id == orderTagOrReportRequest.Id);
            if (orderTagInfo != null)
            {
                _orderDbContext.Remove(orderTagInfo);
                result &= (await _orderDbContext.SaveChangesAsync()) > 0;
            }
            return new ResultMessage(result, result ? "删除成功！" : "删除失败！");
        }

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="orderTagOrReportRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> OrderTagOrReportDisableAsync(OrderTagOrReportRequest orderTagOrReportRequest = null)
        {
            var result = true;

            var orderTagInfo = await _orderDbContext.Order_TagOrReports.SingleOrDefaultAsync(item => item.Id == orderTagOrReportRequest.Id);
            if (orderTagInfo != null)
            {
                orderTagInfo.isEnable = orderTagOrReportRequest.isEnable ?? false;
                _orderDbContext.Update(orderTagInfo);
                result &= (await _orderDbContext.SaveChangesAsync()) > 0;
            }
            return new ResultMessage(result, result ? "作废成功！" : "作废失败！");
        }
    }
}
