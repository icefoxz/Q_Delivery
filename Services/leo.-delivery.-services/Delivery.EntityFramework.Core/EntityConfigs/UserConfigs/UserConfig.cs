using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.EntityFramework.Core.EntityConfigs.UserConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("sys_User");
            builder.HasKey(m => m.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差
            builder.Property(m => m.del_Status).HasDefaultValue(false);
            builder.Property(m => m.isEnable).IsRequired().HasDefaultValue(true);
            builder.HasOne(m => m.deptInfo).WithMany(d => d.userInfos).HasForeignKey(l=> l.dept_Id).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
            builder.HasOne(m => m.limitInfo).WithMany(d => d.userInfos).HasForeignKey(l => l.limit_Id).OnDelete(DeleteBehavior.Restrict).IsRequired(false);// 一个用户只能绑定一个权限组，一个权限组可以绑定给多个用户
            builder.HasOne(m => m.personInfo).WithMany(d => d.userInfos).HasForeignKey(l => l.person_Id).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
            builder.HasIndex(m => new { m.del_Status, m.limit_Id, m.person_Id });//索引不要忘了加上IsDeleted，否则会影响性能
            builder.HasQueryFilter(m => !(bool)m.del_Status);  // 全局过滤器
        }
    }
}
