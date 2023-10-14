using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.EntityFramework.Core.EntityConfigs.OrderConfigs
{
    public class Lingau_OptLogConfig : IEntityTypeConfiguration<Lingau_OptLog>
    {
        public void Configure(EntityTypeBuilder<Lingau_OptLog> builder)
        {
            builder.ToTable("sys_Lingau_OptLog");
            //因为SQLServer对于Guid主键默认创建聚集索引，因此会造成每次插入新数据，都会数据库重排。
            //因此我们取消主键的默认的聚集索引
            builder.HasKey(m => m.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差
            builder.Property(m => m.opt_BeUser).IsRequired();
            builder.Property(m => m.opt_User).IsRequired();
            builder.Property(m => m.opt_Type).IsRequired();
            builder.Property(m => m.opt_BeginAmount).IsRequired();
            builder.Property(m => m.opt_EndAmount).IsRequired();
            builder.HasIndex(m => new { m.opt_User,m.opt_BeUser, m.del_Status });//索引不要忘了加上IsDeleted，否则会影响性能

        }
    }
}
