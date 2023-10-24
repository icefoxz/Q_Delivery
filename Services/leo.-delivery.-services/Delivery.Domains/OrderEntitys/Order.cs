
using System.Net.Cache;

namespace Delivery.Domains.OrderEntitys
{
    /// <summary>
    /// 订单表
    /// </summary>
    public class Order : BaseEntity
    {
        /// <summary>
        /// 订单名称
        /// </summary>
        public string order_Name { get; set; }

        #region 收货配送信息

        /// <summary>
        /// 配送人
        /// </summary>
        public string order_RiderName { get; set; }

        /// <summary>
        /// 配送人手机号
        /// </summary>
        public string order_RiderPhone { get; set; }

        /// <summary>
        /// 工作状态-字典配置
        /// </summary>
        public string order_JobStatus { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string order_ReceiverName { get; set; }

        /// <summary>
        /// 收货人手机号
        /// </summary>
        public string order_ReceiverPhone { get; set; }

        #endregion

        #region 新增配送信息
        public Guid? order_SenderPersonId { get; set; }
        
        #endregion

        #region 物品信息

        /// <summary>
        /// 物品名称
        /// </summary>
        public string order_GoodsName { get; set; }

        /// <summary>
        /// 物品类型-字典配置
        /// </summary>
        public string order_GoodsType { get; set; }

        /// <summary>
        /// 物品重量
        /// </summary>
        public double order_GoodsWeight { get; set; }

        /// <summary>
        /// 物品件数
        /// </summary>
        public int order_GoddsNums { get; set; }

        /// <summary>
        /// 物品长度
        /// </summary>
        public double order_GoodsLong { get; set; }

        /// <summary>
        /// 物品宽度
        /// </summary>
        public double order_GoodsWidth { get; set; }

        /// <summary>
        /// 物品高度
        /// </summary>
        public string order_GoodsHight { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        public double order_GoodsPrice { get; set; }

        #endregion

        #region 位置信息

        /// <summary>
        /// 开始经度
        /// </summary>
        public decimal order_BenginLng { get; set; } = 0;

        /// <summary>
        /// 结束经度
        /// </summary>
        public decimal order_BenginLat { get; set; } = 0;

        /// <summary>
        /// 开始地理Id
        /// </summary>
        public string? order_BenginPlaceId { get; set; }

        /// <summary>
        /// 结束经度
        /// </summary>
        public decimal? order_EndLng { get; set; }

        /// <summary>
        /// 结束纬度
        /// </summary>
        public decimal? order_EndLat { get; set; }

        /// <summary>
        /// 结束地理Id
        /// </summary>
        public string? order_EndPlaceId { get; set; }

        /// <summary>
        /// 舟属Id
        /// </summary>
        public string? order_StateId { get; set; }

        /// <summary>
        /// 配送距离
        /// </summary>
        public double order_PathDistance { get; set; }

        #endregion

        #region 订单费用信息

        /// <summary>
        /// 运送费
        /// </summary>
        public double order_GoodsDelivery { get; set; }

        /// <summary>
        /// 付款类型-字典配置
        /// </summary>
        public string order_PayType { get; set; }

        /// <summary>
        /// 付款标识
        /// </summary>
        public string order_PayIdentity { get; set; }

        /// <summary>
        /// 订单状态-字典配置
        /// </summary>
        public string order_Status { get; set; }

        /// <summary>
        /// 创建单位Id
        /// </summary>
        public string? order_CreateDeptId { get; set; }

        /// <summary>
        /// 创建单位名称-防止后续改变单位名称
        /// </summary>
        public string? order_CreateDeptName { get; set; }

        #endregion

        /// <summary>
        /// 该订单有哪些标签
        /// </summary>
        public List<Order_TagOrReport?>? order_TagOrReportInfos { get; set; }

    }
}
