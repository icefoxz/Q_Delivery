
using Delivery.Domains.Dto.UserServicesDto.UserDto;

namespace Delivery.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {

        private readonly IPersonServices _personServices;
        private readonly IUserServices _userServices;

        public UserController(IPersonServices personServices, IUserServices userServices
            )
        {
            _personServices = personServices;
            _userServices = userServices;
        }

        #region 人员管理

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="personRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getPersonList")]
        public async Task<ResultMessage> getPersonListAsync([FromQuery] PersonRequest personRequest) => new ResultMessage(true, await _personServices.PersonFullListAsync(personRequest));

        /// <summary>
        /// 获取指定类型的人员信息
        /// </summary>
        /// <param name="personRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getPersonListByPerson")]
        public async Task<ResultMessage> getPersonListByPersonAsync([FromQuery] PersonRequest personRequest) => new ResultMessage(true, await _personServices.PersonFullListByPersonTypeAsync(personRequest));


        /// <summary>
        /// 保存人员
        /// </summary>
        /// <param name="personRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("savePerson")]
        public async Task<ResultMessage> SavePersonAsync([FromBody] PersonRequest personRequest) => await _personServices.PersonSaveAsync(personRequest);

        /// <summary>
        /// 删除人员
        /// </summary>
        /// <param name="personRequest"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultMessage> DeletePersonAsync([FromQuery] PersonRequest personRequest) => await _personServices.PersonDeleteAsync(personRequest);


        #endregion

        #region 用户管理

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        [HttpGet, Route("getUserList")]
        public async Task<ResultMessage> GetUserListAsync([FromQuery] UserRequest userRequest) => new ResultMessage(true, await _userServices.UserFullListAsync(userRequest));

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("saveUser")]
        public async Task<ResultMessage> SaveUserAsync([FromBody] UserRequest userRequest) => await _userServices.UserSaveAsync(userRequest);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        [HttpDelete, Route("deleteUser")]
        public async Task<ResultMessage> DeleteUserAsync([FromQuery] UserRequest userRequest) => await _userServices.UserDeleteAsync(userRequest);

        /// <summary>
        /// 启用/禁用用户
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("disableUser")]
        public async Task<ResultMessage> DisableUserAsync([FromQuery] UserRequest userRequest) => await _userServices.UserDisableAsync(userRequest);

        #endregion
    }
}
