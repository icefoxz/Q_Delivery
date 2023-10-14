using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Commons.Cookie
{
    public static class HttpContextUtil
    {
        public static string str { get; set; }
        public static IServiceProvider ServiceProvider;


        public static void SetServices(IServiceProvider services)
        {
            ServiceProvider = services;
        }

        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                object factory = ServiceProvider.GetService(typeof(Microsoft.AspNetCore.Http.IHttpContextAccessor));
                Microsoft.AspNetCore.Http.HttpContext context = ((Microsoft.AspNetCore.Http.HttpContextAccessor)factory).HttpContext;
                return context;
            }
        }



    }

}
