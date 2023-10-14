
using Delivery.Commons.Result;
using Delivery.Domains.Dto.UserServicesDto.UserDto;

namespace Delivery.IServices.IUserServices
{
    /// <summary>
    /// 鉴权服务
    /// </summary>
    public interface IAuthServices
    {
        Task<ResultMessage> LoginOnAsync(UserRequest userRequest = null);
        Task<ResultMessage> GetUserLimitAndMenuInfo(UserRequest userRequest = null);
    }
}
