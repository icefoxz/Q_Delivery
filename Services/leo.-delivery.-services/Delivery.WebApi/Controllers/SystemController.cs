using Delivery.Commons.Result;
using Delivery.Domains.Dto.SystemServicesDto.SystemDict;
using Delivery.Domains.Dto.SystemServicesDto.SystemFile;
using Delivery.Domains.Dto.SystemServicesDto.SystemLog;
using Delivery.IServices.ISystemServices;
using Delivery.WebApi.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : BaseApiController
    {
        private readonly ISystemLogServices _systemLogServices;
        private readonly ISystemDictServices _systemDictServices;
        private readonly ISystemFileServices _systemFileServices;

        public SystemController(ISystemLogServices systemLogServices, ISystemDictServices systemDictServices, ISystemFileServices systemFileServices)
        {
            _systemLogServices = systemLogServices;
            _systemDictServices = systemDictServices;
            _systemFileServices = systemFileServices;
        }

        #region 日志信息

        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="systemLogRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getLogList")]
        public async Task<ResultMessage> GetLogList([FromQuery] SystemLogRequest systemLogRequest) => new ResultMessage(true, await _systemLogServices.SystemLogListAsync(systemLogRequest));

        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="systemLogRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getLogPageList")]
        public async Task<ResultMessage> GetLogPageList([FromQuery] SystemLogRequest systemLogRequest) => new ResultMessage(true, await _systemLogServices.SystemLogPageListAsync(systemLogRequest));

        #endregion

        #region 字典信息

        /// <summary>
        /// 获取systemDict列表
        /// </summary>
        /// <param name="systemDictRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getSystemDictList")]
        public async Task<ResultMessage> getSystemDictList([FromQuery] SystemDictRequest systemDictRequest) => new ResultMessage(true, await _systemDictServices.SystemDictFullListAsync(systemDictRequest));

        /// <summary>
        /// 保存systemDict
        /// </summary>
        /// <param name="systemDictRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("saveSystemDict")]
        public async Task<ResultMessage> SaveSystemDicAsync([FromBody] SystemDictRequest systemDictRequest) => await _systemDictServices.SystemDictSaveAsync(systemDictRequest);

        /// <summary>
        /// 删除systemDict
        /// </summary>
        /// <param name="systemDictRequest"></param>
        /// <returns></returns>
        [HttpDelete, Route("deleteSystemDic")]
        public async Task<ResultMessage> DeleteSystemDicAsync([FromQuery] SystemDictRequest systemDictRequest) => await _systemDictServices.SystemDictDeleteAsync(systemDictRequest);

        #endregion

        #region 文件信息

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="systemFileRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("saveFile")]
        [IgnoreAttributeFilter]
        public async Task<ResultMessage> SaveFile([FromForm] SystemFileRequest systemFileRequest) => new ResultMessage(true, await _systemFileServices.SystemFileSaveAsync(systemFileRequest));

        #endregion

    }
}
