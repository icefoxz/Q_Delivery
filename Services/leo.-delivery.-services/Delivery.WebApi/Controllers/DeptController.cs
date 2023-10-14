using Delivery.Commons.Cookie;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeptController : BaseApiController
    {
        private readonly IDeptServices _deptServices;

        public DeptController(IDeptServices deptServices)
        {
            _deptServices = deptServices;
        }

        #region 单位管理

        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="deptRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getParentDeptList")]
        public async Task<ResultMessage> DeptParentListAsync([FromQuery] DeptRequest deptRequest) => await _deptServices.DeptParentListAsync(deptRequest);

        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="deptRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getDeptList")]
        public async Task<ResultMessage> DeptListAsync([FromQuery] DeptRequest deptRequest) => await _deptServices.DeptFullListAsync(deptRequest);

        /// <summary>
        /// 保存单位
        /// </summary>
        /// <param name="deptRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("saveDept")]
        public async Task<ResultMessage> SaveDeptAsync(DeptRequest deptRequest) => await _deptServices.DeptSaveAsync(deptRequest);

        /// <summary>
        /// 删除单位
        /// </summary>
        /// <param name="deptRequest"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultMessage> DeleteDeptAsync([FromQuery] DeptRequest deptRequest) => await _deptServices.DeptDeleteAsync(deptRequest);


        #endregion

    }
}
