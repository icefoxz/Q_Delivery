
namespace Delivery.EntityFramework.Core.EntityConfigs.OrderConfigs
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("sys_Order");
            //因为SQLServer对于Guid主键默认创建聚集索引，因此会造成每次插入新数据，都会数据库重排。
            //因此我们取消主键的默认的聚集索引
            builder.HasKey(m => m.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差 
            builder.Property(m => m.order_Name).IsRequired();
            builder.Property(m => m.order_RiderName).IsRequired();
            builder.Property(m => m.order_RiderPhone).IsRequired();
            builder.Property(m => m.order_ReceiverName).IsRequired();
            builder.Property(m => m.order_ReceiverPhone).IsRequired();
            builder.Property(m => m.order_GoodsName).IsRequired();
            builder.Property(m => m.order_GoodsType).IsRequired();
            builder.Property(m => m.order_GoddsNums).IsRequired();
            //builder.Property(m => m.order_BenginLat).IsRequired();
            //builder.Property(m => m.order_BenginLng).IsRequired();
            //builder.Property(m => m.order_EndLat).IsRequired();
            //builder.Property(m => m.order_EndLng).IsRequired();
            builder.Property(m => m.order_GoodsPrice).IsRequired();
            builder.Property(m => m.order_GoodsDelivery).IsRequired();
            builder.Property(m => m.order_PathDistance).IsRequired();
            builder.Property(m => m.order_PayIdentity).IsRequired();
            builder.Property(m => m.order_Status).IsRequired();
            builder.Property(m => m.order_CreateDeptId).IsRequired();
            builder.Property(m => m.order_CreateDeptName).IsRequired();
            builder.HasIndex(m => new { m.del_Status });//索引不要忘了加上IsDeleted，否则会影响性能

            //一个订单对应多个订单标签信息
            builder.HasMany(m => m.order_TagOrReportInfos).WithOne(l => l.orderInfo).HasForeignKey(l => l.order_Id).IsRequired();
        }
    }
}
