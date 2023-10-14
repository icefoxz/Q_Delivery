using Delivery.Commons.Cookie;
using Delivery.Domains.BaseEntitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;

namespace Delivery.EntityFramework.Core.DbContexts.BaseDbContexts
{
    public abstract class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        //指定读取的配置文件
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // 从当前程序集加载所有的IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            // 取消级联删除
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // 配置级联删除规则为 DeleteBehavior.Restrict
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        /// <summary>
        /// 重写SaveChangesAsync
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity)
                {
                    var entity = (BaseEntity)item.Entity;
                    if (item.State == EntityState.Added)
                    {
                        //if (entity.Id == Guid.Empty)
                        //entity.Id = Guid.NewGuid();
                        entity.create_Time = DateTime.Now;
                        entity.create_User = UserInfoCookie.user_Name ?? "默认";
                        entity.del_Status = false;
                    }
                    else if (item.State == EntityState.Modified)
                    {
                        entity.update_Time = DateTime.Now;
                        entity.update_User = UserInfoCookie.user_Name ?? "默认";
                    }
                    else if (item.State == EntityState.Deleted)
                    {
                        entity.del_Time = DateTime.Now;
                        entity.del_User = UserInfoCookie.user_Name ?? "默认";
                        entity.del_Status = true;
                    }
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }

        // 公共的实体 DbSet 和其他共享配置...
    }
}
