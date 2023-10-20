using Delivery.Commons.Config;
using Delivery.Domains.Dto.OrderServicesDto.LingauDto;
using Delivery.Domains.Dto.OrderServicesDto.OrderDto;
using Delivery.Domains.Dto.OrderServicesDto.OrderTagOrReportDto;
using Delivery.Domains.Dto.OrderServicesDto.TagDto;
using Delivery.Domains.Dto.OrderServicesDto.TagManagerDto;
using Delivery.Domains.SystemEntitys;
using System;
using System.IO;
using System.Linq;

namespace Delivery.Domains.Dto.Vo
{
    public static class OrderEntitiesVo
    {
        #region 订单信息

        /// <summary>
        /// 订单实体转请求
        /// </summary>
        /// <param name="depts"></param>
        /// <returns></returns>
        public static IEnumerable<OrderResponse> toVo(this IEnumerable<OrderResponse> orders, IEnumerable<Order_TagOrReport> order_TagOrReports, List<SystemFile> fileList = null)
        {

            orders?.ToList().ForEach(item =>
            {
                var tagList = order_TagOrReports.Where(report => report.tag_Type == TagOwningType.Tag.ToString() && report.order_Id == item.Id);
                var tagReportList = order_TagOrReports.Where(report => report.tag_Type == TagOwningType.TagReport.ToString() && report.order_Id == item.Id);

                // 标签
                item.orderTagNameList = tagList.Select(report => report.order_TagOrReport_Name).ToList();
                item.orderTagManagerIdList = tagList.Select(report => report.tagManager_Id).ToList();

                // 报告
                item.orderTagReportNameList = tagReportList.Select(report => report.order_TagOrReport_Name).ToList();
                item.orderTagReportManagerIdList = tagReportList.Select(report => report.tagManager_Id).ToList();

                //处理文件
                if (fileList?.Any() ?? false)
                {
                    //var config = ConfigFileHelper.GetConfig<CommonConfig>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CommonConfigConst.systemConfigUrl));
                    var imgUrl=ConfigHelp.GetStringFromFile("imgUrlFolder");
                    item.fileUrlList = fileList.Where(file => file.data_Id == item.Id.ToString())?.Select(file => new imgUrl()
                    {
                        fileId= file.Id,
                        fileName = file.file_Name,
                        filePath = Path.Combine(imgUrl, file.file_Path)
                    }).ToList() ?? new List<imgUrl>();
                }
                    

            });
            return orders;
        }

        #endregion

        #region 订单关联信息

        public static IEnumerable<OrderTagOrReportResponse> toVo(this IEnumerable<Order_TagOrReport> order_TagOrReports, IEnumerable<Order> orders = null, IEnumerable<Tag_Manager> tagManagers = null, IEnumerable<Tag> tags = null)
        {
            return order_TagOrReports?.Select(item =>
            {
                return new OrderTagOrReportResponse()
                {
                    Id = item.Id,
                    order_TagOrReport_Name = item.order_TagOrReport_Name,
                    order_Id = item.order_Id,
                    order_Name = orders?.FirstOrDefault(order => order.Id == item.order_Id)?.order_Name,
                    tagManager_Id = item.tagManager_Id,
                    tag_Name = tags?.FirstOrDefault(tag => (tag.Id == tagManagers?.FirstOrDefault(tagMana => tagMana.Id == item.tagManager_Id)?.tag_Id))?.tag_Name,
                    tag_TypeName = item.tag_Type == TagOwningType.Tag.ToString() ? "标签" : item.tag_Type == TagOwningType.TagReport.ToString() ? "报告" : "未知",
                    isEnable = item.isEnable,
                    expand_Desc = item.expand_Desc,
                    create_User = item.create_User,
                };
            }) ?? new List<OrderTagOrReportResponse>();
        }
        public static IEnumerable<OrderTagOrReportResponse> toVo(this IEnumerable<OrderTagOrReportResponse> order_TagOrReports, IEnumerable<Order> orders = null, IEnumerable<Tag_Manager> tagManagers = null, IEnumerable<Tag> tags = null)
        {
            return order_TagOrReports?.Select(item =>
            {
                return new OrderTagOrReportResponse()
                {
                    Id = item.Id,
                    order_TagOrReport_Name = item.order_TagOrReport_Name,
                    order_Id = item.order_Id,
                    order_Name = orders?.FirstOrDefault(order => order.Id == item.order_Id)?.order_Name,
                    tagManager_Id = item.tagManager_Id,
                    tag_Name = tags?.FirstOrDefault(tag => (tag.Id == tagManagers?.FirstOrDefault(tagMana => tagMana.Id == item.tagManager_Id)?.tag_Id))?.tag_Name,
                    tag_TypeName = item.tag_Type == TagOwningType.Tag ? "标签" : item.tag_Type == TagOwningType.TagReport ? "报告" : "未知",
                    isEnable = item.isEnable,
                    expand_Desc = item.expand_Desc,
                    create_User = item.create_User,
                };
            }) ?? new List<OrderTagOrReportResponse>();
        }
        #endregion

        #region 标签信息

        public static IEnumerable<TagResponse> toVo(this IEnumerable<Tag> tag)
        {
            return tag?.Select(item =>
            {

                return new TagResponse()
                {
                    Id = item.Id,
                    tag_Name = item.tag_Name,
                    isEnable = item.isEnable,
                    expand_Desc = item.expand_Desc ?? "",
                    create_User = item.create_User,
                    create_Time = Convert.ToDateTime(item.create_Time).ToString("yyyy-MM-dd HH:mm:ss"),
                };
            }) ?? new List<TagResponse>();
        }

        #endregion

        #region 标签所属信息

        public static IEnumerable<TagManagerResponse> toVo(this IEnumerable<Tag_Manager> tagManager, List<Tag> tags = null)
        {
            return tagManager?.Select(item =>
            {
                var tagItem = tags?.FirstOrDefault(tag => tag.Id == item.tag_Id);
                return new TagManagerResponse()
                {
                    Id = item.Id,
                    tag_Id = (Guid)item.tag_Id,
                    tag_Name = tagItem?.tag_Name ?? "",
                    isEnable = tagItem?.isEnable ?? false,
                    tag_Type = (TagOwningType)Enum.Parse(typeof(TagOwningType), item.tag_Type),
                    tag_TypeName = item.tag_Type == TagOwningType.Tag.ToString() ? "标签" : item.tag_Type == TagOwningType.TagReport.ToString() ? "报告" : "未知",
                    create_Time = Convert.ToDateTime(item.create_Time).ToString("yyyy-MM-dd HH:mm:ss"),
                    create_User = item.create_User,
                    expand_Desc = item.expand_Desc,
                };
            }) ?? new List<TagManagerResponse>();
        }

        #endregion


        #region Lingau

        /// <summary>
        /// Lingau
        /// </summary>
        /// <param name="lingaus"></param>
        /// <param name="persons"></param>
        /// <returns></returns>
        public static List<LingauResponse> toVo(this List<Lingau> lingaus, IEnumerable<Person> persons)
        {
            return lingaus.Select(ling => new LingauResponse()
            {
                Id = ling.Id,
                person_Id = ling.person_Id,
                lingau_Balance = ling.lingau_Balance,
                person_Name = persons?.FirstOrDefault(item => item.Id == ling.person_Id)?.per_Name ?? "",
                create_User = ling.create_User,
                create_Time = Convert.ToDateTime(ling.create_Time).ToString("yyyy-MM-dd HH:mm:ss"),
            }).ToList() ?? new List<LingauResponse>();
        }

        #endregion
    }
}
