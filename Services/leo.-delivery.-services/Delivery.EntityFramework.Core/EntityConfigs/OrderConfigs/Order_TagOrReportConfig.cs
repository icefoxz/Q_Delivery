using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.EntityFramework.Core.EntityConfigs.OrderConfigs
{
    public class Order_TagOrReportConfig : IEntityTypeConfiguration<Order_TagOrReport>
    {
        public void Configure(EntityTypeBuilder<Order_TagOrReport> builder)
        {
            builder.ToTable("sys_Order_TagOrReport");
            //因为SQLServer对于Guid主键默认创建聚集索引，因此会造成每次插入新数据，都会数据库重排。
            //因此我们取消主键的默认的聚集索引
            builder.HasKey(m => m.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差
            builder.Property(m =>   m.order_TagOrReport_Name ).IsRequired();
            builder.Property(m => m.isEnable).IsRequired().HasDefaultValue(true);
            builder.Property(m =>  m.order_Id).IsRequired();
            builder.Property(m =>  m.tagManager_Id).IsRequired();
            builder.HasIndex(m => new { m.order_TagOrReport_Name,m.order_Id, m.del_Status });//索引不要忘了加上IsDeleted，否则会影响性能
        }
    }
}
