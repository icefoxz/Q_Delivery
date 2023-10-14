using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.EntityFramework.Core.EntityConfigs.UserConfigs
{
    public class LimitConfig : IEntityTypeConfiguration<Limit>
    {
        public void Configure(EntityTypeBuilder<Limit> builder)
        {
            builder.ToTable("sys_Limit");
            builder.HasKey(m => m.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差
            builder.HasIndex(m => new { m.del_Status });//索引不要忘了加上IsDeleted，否则会影响性能
            builder.Property(m => m.del_Status).HasDefaultValue(false);
            builder.HasQueryFilter(m => !(bool)m.del_Status);  // 全局过滤器
        }
    }
}
