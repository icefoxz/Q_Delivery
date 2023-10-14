using Delivery.Domains.Dto.OrderServicesDto.TagDto;
using Delivery.Domains.Dto.OrderServicesDto.TagManagerDto;
using Delivery.IServices.IOrderServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagManagerController : ControllerBase
    {

        private readonly ITagServices _tagServices;
        private readonly ITagManagerServices _tagManagerServices;

        public TagManagerController(ITagServices tagServices, ITagManagerServices tagManagerServices)
        {
            _tagServices = tagServices;
            _tagManagerServices = tagManagerServices;
        }

        #region 订单标签

        /// <summary>
        /// 获取标签信息
        /// </summary>
        /// <param name="tagRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getTagList")]
        public async Task<ResultMessage> GetTagListAsync([FromQuery] TagRequest tagRequest) => new ResultMessage(true, await _tagServices.TagFullListAsync(tagRequest));

        /// <summary>
        /// 保存标签
        /// </summary>
        /// <param name="tagRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("saveTag")]
        public async Task<ResultMessage> SaveTagAsync([FromBody] TagRequest tagRequest) => await _tagServices.TagSaveAsync(tagRequest);

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="tagRequest"></param>
        /// <returns></returns>
        [HttpDelete, Route("deleteTag")]
        public async Task<ResultMessage> DeleteTagAsync([FromQuery] TagRequest tagRequest) => await _tagServices.TagDeleteAsync(tagRequest);

        /// <summary>
        /// 禁用启用标签
        /// </summary>
        /// <param name="tagRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("disableTag")]
        public async Task<ResultMessage> DisableTagAsync([FromQuery] TagRequest tagRequest) => await _tagServices.TagDisableAsync(tagRequest);

        #endregion

        #region 标签所属类型管理

        /// <summary>
        /// 获取标签所属类型信息
        /// </summary>
        /// <param name="tagManagerRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getTagManagerList")]
        public async Task<ResultMessage> GetTagManagerListAsync([FromQuery] TagManagerRequest tagManagerRequest) => new ResultMessage(true, await _tagManagerServices.TagManagerFullListAsync(tagManagerRequest));

        /// <summary>
        /// 保存标签所属类型
        /// </summary>
        /// <param name="tagManagerRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("saveTagManager")]
        public async Task<ResultMessage> SaveTagManagerAsync([FromBody] TagManagerRequest tagManagerRequest) => await _tagManagerServices.TagManagerSaveAsync(tagManagerRequest);

        /// <summary>
        /// 删除标签所属类型
        /// </summary>
        /// <param name="tagManagerRequest"></param>
        /// <returns></returns>
        [HttpDelete, Route("deleteTagManager")]
        public async Task<ResultMessage> DeleteTagManagerAsync([FromQuery] TagManagerRequest tagManagerRequest) => await _tagManagerServices.TagManagerDeleteAsync(tagManagerRequest);


        #endregion

    }
}
