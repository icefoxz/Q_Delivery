using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.EntityFramework.Core.EntityConfigs.UserConfigs
{
    public class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("sys_Menu");
            builder.HasKey(m => m.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差
            builder.Property(m => m.menu_Name).IsRequired();
            builder.Property(m => m.isOuterJoin).HasDefaultValue(false);
            //builder.Property(m => m.menu_Type).IsRequired();
            //builder.Property(m => m.menu_Path).IsRequired();
            //builder.Property(m => m.menu_FileName).IsRequired();
            //builder.Property(m => m.menu_ComponentName).IsRequired();
            builder.Property(m => m.del_Status).HasDefaultValue(false);
            builder.HasIndex(m => new { m.del_Status });//索引不要忘了加上IsDeleted，否则会影响性能
            builder.HasQueryFilter(m => !(bool)m.del_Status);  // 全局过滤器
        }
    }
}
