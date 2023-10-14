using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace Delivery.WebMvc1.Jwt
{
    //IAuthorizationRequirement 是 Microsoft.AspNetCore.Authorization 接口

    /// <summary>
    /// 用户认证信息必要参数类
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 用户所属角色
        /// </summary>
        public Role Roles { get; set; } = new Role();
        public void SetRolesName(string roleName)
        {
            Roles.Name = roleName;
        }
        /// <summary>
        /// 无权限时跳转到此API
        /// </summary>
        public string DeniedAction { get; set; }

        /// <summary>
        /// 认证授权类型
        /// </summary>
        public string ClaimType { internal get; set; }
        /// <summary>
        /// 未授权时跳转
        /// </summary>
        public string LoginPath { get; set; } = "/Account/Login";
        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 订阅人
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Expiration { get; set; }
        /// <summary>
        /// 颁发时间
        /// </summary>
        public long IssuedTime { get; set; }
        /// <summary>
        /// 签名验证
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="deniedAction">无权限时跳转到此API</param>
        /// <param name="userPermissions">用户权限集合</param>
        /// <param name="deniedAction">拒约请求的url</param>
        /// <param name="permissions">权限集合</param>
        /// <param name="claimType">声明类型</param>
        /// <param name="issuer">发行人</param>
        /// <param name="audience">订阅人</param>
        /// <param name="issusedTime">颁发时间</param>
        /// <param name="signingCredentials">签名验证实体</param>
        public PermissionRequirement(string deniedAction, Role Role, string claimType, string issuer, string audience, SigningCredentials signingCredentials, long issusedTime, TimeSpan expiration)
        {
            ClaimType = claimType;
            DeniedAction = deniedAction;
            Roles = Role;
            Issuer = issuer;
            Audience = audience;
            Expiration = expiration;
            IssuedTime = issusedTime;
            SigningCredentials = signingCredentials;
        }
    }

}
