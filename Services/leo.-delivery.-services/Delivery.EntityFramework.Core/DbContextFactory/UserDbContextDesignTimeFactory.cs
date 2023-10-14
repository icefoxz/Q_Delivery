using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.EntityFramework.Core.DbContexts;

namespace Delivery.EntityFramework.Core.DbContextFactory
{
    /// <summary>
    /// 1.写一个类
    /// 2.实现IDesignTimeContextFactory接口
    /// 3.返回Dbcontext类就行了
    /// </summary>
    public class UserDbContextDesignTimeFactory : IDesignTimeDbContextFactory<OrderDbContext>
    {
        public OrderDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<OrderDbContext> builder = new DbContextOptionsBuilder<OrderDbContext>();
            builder.UseSqlServer("Data Source=.;Initial Catalog=efCoreTest3;Integrated Security=True;Encrypt = Optional");
            return new OrderDbContext(builder.Options);
        }
    }
}
