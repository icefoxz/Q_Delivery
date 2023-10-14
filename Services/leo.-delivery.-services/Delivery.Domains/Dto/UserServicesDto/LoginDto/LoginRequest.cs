using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.UserServicesDto.LoginDto
{
    public class LoginRequest
    {
        public string loginName { get; set; }
        public string loginPwd { get; set; }
    }
}
