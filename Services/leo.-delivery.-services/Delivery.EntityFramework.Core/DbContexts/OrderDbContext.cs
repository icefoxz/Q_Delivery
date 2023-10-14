 
namespace Delivery.EntityFramework.Core.DbContexts
{
    public class OrderDbContext : BaseDbContext
    {
        /// <summary>
        /// 订单表
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        // 标签表
        public DbSet<Tag> Tags { get; set; }

        // 标签所属类型表
        public DbSet<Tag_Manager> Tag_Managers { get; set; }

        /// <summary>
        /// 订单报告或标签表
        /// </summary>
        public DbSet<Order_TagOrReport> Order_TagOrReports { get; set; }


        /// <summary>
        /// 令澳币
        /// </summary>
        /// <param name="options"></param>
        public DbSet<Lingau> Lingaus { get; set; }

        /// <summary>
        /// 令澳币操作日志
        /// </summary>
        /// <param name="options"></param>
        public DbSet<Lingau_OptLog> Lingau_OptLogs { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        // 其他实体 DbSet 和特定的配置...
    }
}
