using CZGL.Auth.Models;
using Delivery.WebMvc1.Jwt;
using Delivery.WebMvc1.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Security.Claims;

namespace Delivery.WebMvc1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var user = UserModel.Users.FirstOrDefault(x => x.UserName == username && x.UserPossword == password);
            //if (user == null)
            //    return new JsonResult(
            //        new ResponseModel
            //        {
            //            Code = 0,
            //            Message = "登陆失败!"
            //        });


            // 配置用户标识
            var userClaims = new Claim[]
            {
                new Claim(ClaimTypes.Name,"asdfsa"),
                new Claim(ClaimTypes.Role,"asdfsa"), 
            }; 
            // 生成用户标识
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identity.AddClaims(userClaims);

            var token = JwtTokenHelper.BuildJwtToken(userClaims);

     
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}