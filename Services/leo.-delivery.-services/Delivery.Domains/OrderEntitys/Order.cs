
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
        public string order_ItemName { get; set; }

        /// <summary>
        /// 物品类型-字典配置
        /// </summary>
        public string order_ItemType { get; set; }

        /// <summary>
        /// 物品重量
        /// </summary>
        public double order_ItemWeight { get; set; }

        /// <summary>
        /// 物品数量
        /// </summary>
        public int order_ItemQuantity { get; set; }

        /// <summary>
        /// 物品体积 长×宽×高/6000
        /// </summary>
        public double order_ItemVolume { get; set; }

        /// <summary>
        /// 物品附加信息
        /// </summary>
        public string order_ItemRemark { get; set; }

        /// <summary>
        /// 物品长度-单位米
        /// </summary>
        public double order_ItemLong { get; set; }

        /// <summary>
        /// 物品宽度-单位米
        /// </summary>
        public double order_ItemWidth { get; set; }

        /// <summary>
        /// 物品高度-单位米
        /// </summary>
        public double order_ItemHight { get; set; }

        /// <summary>
        /// 物品价格，价值
        /// </summary>
        public double order_ItemPrice { get; set; }

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
        /// 结束地理Id
        /// </summary>
        public string? order_EndPlaceId { get; set; }

        /// <summary>
        /// 结束经度
        /// </summary>
        public decimal? order_EndLng { get; set; }

        /// <summary>
        /// 结束纬度
        /// </summary>
        public decimal? order_EndLat { get; set; }

        /// <summary>
        /// 开始地址
        /// </summary>
        public string? order_BeginAddress { get; set; }

        /// <summary>
        /// 结束地址
        /// </summary>
        public string? order_EndAddress { get; set; }

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
        public double order_Fee { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public double order_Charge { get; set; }

        /// <summary>
        /// 付款类型PaymentMethods
        /// </summary>
        public string order_PayMethond { get; set; }

        /// <summary>
        /// 付款Reference,如果骑手代收是骑手Id
        /// 如果在线支付将会是支付平台的Reference
        /// 如果是用户扣账将会是用户Id
        /// </summary>
        public string? order_Reference { get; set; }

        /// <summary>
        /// 付款TransactionId
        /// </summary>
        public int order_TransactionId { get; set; }

        /// <summary>
        /// 是否已完成付款
        /// </summary>
        public bool? order_IsReceived { get; set; }

        #endregion

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
        /// 订单图片
        /// </summary>
        public List<string> order_ImgUrls { get; set; }

        ///// <summary>
        ///// 创建单位Id
        ///// </summary>
        //public string? order_CreateDeptId { get; set; }

        ///// <summary>
        ///// 创建单位名称-防止后续改变单位名称
        ///// </summary>
        //public string? order_CreateDeptName { get; set; }


        /// <summary>
        /// 该订单有哪些标签
        /// </summary>
        public List<Order_TagOrReport?>? order_TagOrReportInfos { get; set; }

    }
}
