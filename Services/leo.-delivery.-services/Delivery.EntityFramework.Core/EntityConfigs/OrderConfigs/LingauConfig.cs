using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.EntityFramework.Core.EntityConfigs.OrderConfigs
{
    internal class LingauConfig : IEntityTypeConfiguration<Lingau>
    {
        public void Configure(EntityTypeBuilder<Lingau> builder)
        {
            builder.ToTable("sys_Lingau");
            //因为SQLServer对于Guid主键默认创建聚集索引，因此会造成每次插入新数据，都会数据库重排。
            //因此我们取消主键的默认的聚集索引
            builder.HasKey(m => m.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差
            builder.Property(m => m.person_Id).IsRequired();
            builder.HasIndex(m => new { m.person_Id, m.del_Status });//索引不要忘了加上IsDeleted，否则会影响性能

        }
    }
}
