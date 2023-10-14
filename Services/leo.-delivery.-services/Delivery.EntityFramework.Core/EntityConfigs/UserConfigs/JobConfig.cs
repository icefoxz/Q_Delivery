using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.EntityFramework.Core.EntityConfigs.UserConfigs
{
    public class JobConfig : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("sys_Job");
            builder.HasKey(m => m.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差
            builder.Property(m => m.job_Name).IsRequired();
            builder.Property(m => m.del_Status).HasDefaultValue(false);
            builder.HasOne(l => l.deptInfo).WithMany(l => l.jobInfos).HasForeignKey(l => l.dept_Id).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(m => new { m.del_Status, m.dept_Id });//索引不要忘了加上IsDeleted，否则会影响性能
            builder.HasQueryFilter(m => !(bool)m.del_Status);  // 全局过滤器

        }
    }
}
