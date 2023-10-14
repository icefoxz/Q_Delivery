using Delivery.Domains.BaseEntitys;
using Delivery.Domains.Dto.OrderServicesDto.OrderDto;
using Delivery.Domains.Dto.SystemServicesDto.SystemLog;
using Delivery.Domains.SystemEntitys;
using Delivery.EntityFramework.Core.DbContexts;
using Delivery.IServices.ISystemServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services.SystemServices
{

    [AutoInject(typeof(ISystemLogServices), InjectTypeEnum.Scope)]
    public class SystemLogServices : ISystemLogServices
    {
        private readonly SystemDbContext _systemDbContext;

        public SystemLogServices(SystemDbContext systemDbContext)
        {
            _systemDbContext = systemDbContext;
        }

        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="systemLog"></param>
        /// <returns></returns>
        public async Task SystemLogAddAsync(SystemLog systemLog = null)
        {
            if (systemLog != null)
            {
                await _systemDbContext.AddAsync(systemLog);
                var result = await _systemDbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="systemLogRequest"></param>
        /// <returns></returns>
        public async Task<List<SystemLog>> SystemLogListAsync(SystemLogRequest systemLogRequest = null)
        {
            IQueryable<SystemLog> logQuery = _systemDbContext.SystemLogs.AsNoTracking();

            // 提交方式
            if (string.IsNullOrWhiteSpace(systemLogRequest?.log_ApiMethod) == false)
                logQuery = logQuery.Where(item => item.log_ApiMethod.Contains(systemLogRequest.log_ApiMethod));
            // IP
            if (string.IsNullOrWhiteSpace(systemLogRequest?.log_OptIP) == false)
                logQuery = logQuery.Where(item => item.log_OptIP.Contains(systemLogRequest.log_OptIP));
            // 操作人
            if (string.IsNullOrWhiteSpace(systemLogRequest?.log_OptUser) == false)
                logQuery = logQuery.Where(item => item.log_OptUser.Contains(systemLogRequest.log_OptUser));
            // 日志类型
            if (string.IsNullOrWhiteSpace(systemLogRequest?.log_Type) == false)
                logQuery = logQuery.Where(item => item.log_Type.Contains(systemLogRequest.log_Type));
            // 日志内容
            if (string.IsNullOrWhiteSpace(systemLogRequest?.log_Message) == false)
                logQuery = logQuery.Where(item => item.log_Message.Contains(systemLogRequest.log_Message));
            // 请求方式
            //if (string.IsNullOrWhiteSpace(systemLogRequest?.tag_Type) == false)
            //    logQuery = logQuery.Where(item => item.tag_Type.Contains(systemLogRequest.tag_Type));

            // 开始时间
            if (systemLogRequest.begin_Time != null)
                logQuery = logQuery.Where(item => item.create_Time > systemLogRequest.begin_Time);
            // 结束时间
            if (systemLogRequest.end_Time != null)
                logQuery = logQuery.Where(item => item.create_Time < systemLogRequest.end_Time);

            var logs = await logQuery.OrderBy(item => item.create_Time).Take(50).ToListAsync();

            return logs;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="systemLog"></param>
        /// <returns></returns>
        public async Task<PageList<SystemLog>> SystemLogPageListAsync(SystemLogRequest systemLog)
        {
            var totalCount = await _systemDbContext.SystemLogs.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)systemLog.page_Size);

            IQueryable<SystemLog> pagListQuery = _systemDbContext.SystemLogs
           .AsNoTracking();

            // 提交方式
            if (string.IsNullOrWhiteSpace(systemLog?.log_ApiMethod) == false)
                pagListQuery = pagListQuery.Where(item => item.log_ApiMethod.Contains(systemLog.log_ApiMethod));
            // IP
            if (string.IsNullOrWhiteSpace(systemLog?.log_OptIP) == false)
                pagListQuery = pagListQuery.Where(item => item.log_OptIP.Contains(systemLog.log_OptIP));
            // 操作人
            if (string.IsNullOrWhiteSpace(systemLog?.log_OptUser) == false)
                pagListQuery = pagListQuery.Where(item => item.log_OptUser.Contains(systemLog.log_OptUser));
            // 日志类型
            if (string.IsNullOrWhiteSpace(systemLog?.log_Type) == false)
                pagListQuery = pagListQuery.Where(item => item.log_Type.Contains(systemLog.log_Type));
            // 日志内容
            if (string.IsNullOrWhiteSpace(systemLog?.log_Message) == false)
                pagListQuery = pagListQuery.Where(item => item.log_Message.Contains(systemLog.log_Message));
            // 开始时间
            if (systemLog.begin_Time != null)
                pagListQuery = pagListQuery.Where(item => item.create_Time > systemLog.begin_Time);
            // 结束时间
            if (systemLog.end_Time != null)
                pagListQuery = pagListQuery.Where(item => item.create_Time < systemLog.end_Time);

            var pagList = await pagListQuery.OrderByDescending(item => item.create_Time)
           .Skip(systemLog.current_PageIndex * systemLog.page_Size)
           .Take(systemLog.page_Size)
           .ToListAsync();

            return new PageList<SystemLog>()
            {
                current_PageIndex = systemLog.current_PageIndex,// 当前页
                page_Size = systemLog.page_Size,// 当前页数量
                CurrentPageData = pagList,// 当前页数据
                total = totalCount,// 总数量
                totalPages = totalPages,// 总页数
            };
        }

    }
}
