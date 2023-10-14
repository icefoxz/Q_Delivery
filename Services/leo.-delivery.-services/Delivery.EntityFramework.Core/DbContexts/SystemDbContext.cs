using Delivery.Domains.SystemEntitys;
using Delivery.EntityFramework.Core.DbContexts.BaseDbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.EntityFramework.Core.DbContexts
{
    public class SystemDbContext  : BaseDbContext
    {
        public DbSet<SystemDict> SystemDicts { get; set; }
        public DbSet<SystemFile> SystemFiles { get; set; }
        public DbSet<SystemLog> SystemLogs { get; set; }
        public SystemDbContext(DbContextOptions<SystemDbContext> options)
            : base(options)
        {
        }
    }
}
