using Delivery.Commons.Result;
using Delivery.Domains.Dto.UserServicesDto.LoginDto;
using Delivery.Domains.Dto.UserServicesDto.UserDto;
using Delivery.WebApi.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Authorization;

namespace Delivery.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [IgnoreAttributeFilter]
        [HttpPost]
        public async Task<ResultMessage> LoginOn([FromBody] LoginRequest loginRequest)
        {
            return await _authServices.LoginOnAsync(new UserRequest()
            {
                user_LoginName = loginRequest.loginName,
                user_LoginPwd = loginRequest.loginPwd

            });
        }

        /// <summary>
        /// 获取菜单用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetUserLimitAndMenuInfo")]
        public async Task<ResultMessage> GetUserLimitAndMenuInfo()
        {
            return await _authServices.GetUserLimitAndMenuInfo();
        }
    }
}
