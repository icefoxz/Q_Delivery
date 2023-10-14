using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.EntityFramework.Core.EntityConfigs.UserConfigs
{
    public class Limit_MenuConfig : IEntityTypeConfiguration<Limit_Menu>
    {
        public void Configure(EntityTypeBuilder<Limit_Menu> builder)
        {
            builder.ToTable("sys_Limit_Menu");
            builder.HasKey(l => l.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差
            builder.Property(m => m.del_Status).HasDefaultValue(false);
            builder.HasOne(l => l.limitInfo).WithMany(l => l.limit_Menus).HasForeignKey(l => l.limit_Id).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(l => l.menuInfo).WithMany().HasForeignKey(l => l.menu_Id).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(l => new { l.del_Status, l.limit_Id, l.menu_Id });//索引不要忘了加上IsDeleted，否则会影响性能
            builder.HasQueryFilter(m => !(bool)m.del_Status);  // 全局过滤器
        }
    }
}
