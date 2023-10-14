
using Delivery.Domains.Dto.OrderServicesDto.TagDto;
using Delivery.IServices.IOrderServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LingauController : ControllerBase
    {
        private readonly ILingauServices _lingauServices;

        public LingauController(ILingauServices lingauServices)
        {
            _lingauServices = lingauServices;
        }

        /// <summary>
        /// 获取Lingau列表
        /// </summary>
        /// <param name="lingauRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getLingauList")]
        public async Task<ResultMessage> GetLingauListAsync([FromQuery] LingauRequest lingauRequest) => new ResultMessage(true, await _lingauServices.LingauFullListAsync(lingauRequest));

        /// <summary>
        /// 保存Lingau
        /// </summary>
        /// <param name="lingauRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("saveLingau")]
        public async Task<ResultMessage> SaveLingauAsync([FromBody] LingauRequest lingauRequest) => await _lingauServices.LingauSaveAsync(lingauRequest);

        /// <summary>
        /// 删除Lingau
        /// </summary>
        /// <param name="lingauRequest"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultMessage> DeleteLingauAsync([FromQuery] LingauRequest lingauRequest) => await _lingauServices.LingauDeleteAsync(lingauRequest);

        /// <summary>
        /// 获取LingauOptLog列表
        /// </summary>
        /// <param name="lingauOptLogRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getLingauOptLogList")]
        public async Task<ResultMessage> GetLingauOptLogListAsync([FromQuery] LingauOptLogRequest lingauOptLogRequest) => new ResultMessage(true, await _lingauServices.LingauOptLogFullListAsync(lingauOptLogRequest));

    }
}
