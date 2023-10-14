using CZGL.Auth.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Delivery.WebMvc1.Jwt
{
    public static class JwtTokenHelper
    {
        public static TokenValidationParameters GetTokenValidationParameters()
        {
            var tokenValida = new TokenValidationParameters
            {
                // 定义 Token 内容
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConfig.SecurityKey)),
                ValidateIssuer = true,
                ValidIssuer = AuthConfig.,
                ValidateAudience = true,
                ValidAudience = AuthConfig.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true
            };
            return tokenValida;
        }


        /// <summary>
        /// 获取基于JWT的Token
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static dynamic BuildJwtToken(Claim[] claims, PermissionRequirement permissionRequirement=null)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: permissionRequirement?.Issuer,
                audience: permissionRequirement?.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(permissionRequirement?.Expiration??default),
                signingCredentials: permissionRequirement?.SigningCredentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                Status = true,
                access_token = encodedJwt,
                expires_in = permissionRequirement?.Expiration.TotalMilliseconds,
                token_type = "Bearer"
            };
            return response;
        }


    }
}
