using Delivery.Domains.UserEntitys;
using Delivery.EntityFramework.Core.DbContexts.BaseDbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.EntityFramework.Core.DbContexts
{
    public class UserDbContext : BaseDbContext
    {
        public DbSet<Dept> Depts { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Limit> Limits { get; set; }
        public DbSet<Limit_Menu> Limit_Menus { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Person> Persons { get; set; }

        public DbSet<User> Users { get; set; } 

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }
    }
}
