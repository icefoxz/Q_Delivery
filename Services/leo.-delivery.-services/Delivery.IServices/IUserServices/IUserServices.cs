using Delivery.Commons.Result;
using Delivery.Domains.Dto.UserServicesDto.UserDto;
using Delivery.Domains.UserEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.IServices.IUserServices
{
    public interface IUserServices
    {
        Task<IEnumerable<User>> UserListAsync(UserRequest UserRequest = null);
        Task<IEnumerable<UserResponse>> UserFullListAsync(UserRequest UserRequest = null);
        Task<ResultMessage> UserSaveAsync(UserRequest UserRequest = null);
        Task<ResultMessage> UserDeleteAsync(UserRequest UserRequest = null);
        Task<ResultMessage> UserDisableAsync(UserRequest UserRequest = null);
        Task<UserResponse> UserAccountVerifictionAsync(UserRequest UserRequest = null);
    }
}
