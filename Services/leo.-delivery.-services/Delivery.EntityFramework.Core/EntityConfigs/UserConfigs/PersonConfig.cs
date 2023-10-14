using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.EntityFramework.Core.EntityConfigs.UserConfigs
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("sys_Person");
            builder.HasKey(e => e.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差
            builder.HasOne(l => l.deptInfo).WithMany(l => l.personInfos).HasForeignKey(l => l.dept_Id).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(l => l.jobInfo).WithMany(l => l.personInfos).HasForeignKey(l => l.job_Id).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(m => new { m.del_Status, m.dept_Id });//索引不要忘了加上IsDeleted，否则会影响性能
            builder.Property(m => m.del_Status).HasDefaultValue(false);
            builder.HasQueryFilter(m => !(bool)m.del_Status);  // 全局过滤器
        }
    }
}
