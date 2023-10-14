using Delivery.Domains.SystemEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.EntityFramework.Core.EntityConfigs.SystemConfigs
{
    public class SystemFileConfig : IEntityTypeConfiguration<SystemFile>
    {
        public void Configure(EntityTypeBuilder<SystemFile> builder)
        {
            builder.ToTable("sys_File");
            builder.Property(m => m.file_Name).IsRequired(true);
            builder.Property(m => m.file_Path).IsRequired(true);
            builder.Property(m => m.data_Id).IsRequired(false);
            builder.Property(m => m.data_Type).IsRequired(false);

            //因为SQLServer对于Guid主键默认创建聚集索引，因此会造成每次插入新数据，都会数据库重排。
            //因此我们取消主键的默认的聚集索引
            builder.HasKey(m => m.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差
            builder.HasIndex(m => new { m.del_Status });//索引不要忘了加上IsDeleted，否则会影响性能
            builder.HasQueryFilter(m => !(bool)m.del_Status);  // 全局过滤器
        }
    }
}
