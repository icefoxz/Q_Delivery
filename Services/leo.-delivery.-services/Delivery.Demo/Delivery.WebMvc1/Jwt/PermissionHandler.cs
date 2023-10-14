using CZGL.Auth.Models;
using Microsoft.AspNetCore.Authorization;

namespace Delivery.WebMvc1.Jwt
{
    /// <summary>
    /// 验证用户信息，进行权限授权Handler
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       PermissionRequirement requirement)
        {
            List<PermissionRequirement> requirements = new List<PermissionRequirement>();
            foreach (var item in context.Requirements)
            {
                requirements.Add((PermissionRequirement)item);
            }
            foreach (var item in requirements)
            {
                //// 校验 颁发和接收对象
                //if (!(item.Issuer == AuthConfig.Issuer ?
                //    item.Audience == AuthConfig.Audience ?
                //    true : false : false))
                //{
                //    context.Fail();
                //}
                // 校验过期时间
                var nowTime = DateTimeOffset.Now.ToUnixTimeSeconds();
                var issued = item.IssuedTime + Convert.ToInt64(item.Expiration.TotalSeconds);
                if (issued < nowTime)
                    context.Fail();



                // 是否有访问此 API 的权限
                var resource = ((Microsoft.AspNetCore.Routing.RouteEndpoint)context.Resource).RoutePattern;
                //var permissions = item.Roles.Permissions.ToList();
                //var apis = permissions.Any(x => x.Name.ToLower() == item.Roles.Name.ToLower() && x.Url.ToLower() == resource.RawText.ToLower());
                //if (!apis)
                //    context.Fail();

                //context.Succeed(requirement);
                // 无权限时跳转到某个页面
                //var httpcontext = new HttpContextAccessor();
                //httpcontext.HttpContext.Response.Redirect(item.DeniedAction);
            }

            //context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

}
