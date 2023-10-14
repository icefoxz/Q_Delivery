using Delivery.Commons.Cookie;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.WebApi.Controllers.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
         
        public readonly BaseQuery @base;
        public BaseApiController()
        {
            try
            { 
                //var headers = httpContextAccessor.HttpContext.Request.Headers;

                //var origin = headers["Origin"].FirstOrDefault().ToLowerInvariant();
                //var referer = headers["Referer"].FirstOrDefault().ToLowerInvariant(); 
               // var a11 = HttpContext.Request;
               //var a= HttpContextUtil.Current;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
