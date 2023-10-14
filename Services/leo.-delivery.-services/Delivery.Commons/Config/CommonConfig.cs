using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Commons.Config
{
    public class CommonConfig
    {
        /// <summary>
        /// 数据库连接串
        /// </summary>
        public string connString { get; set; }

        /// <summary>
        /// 用户密码解密Key
        /// </summary>
        public string secretKey { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        //public string imgUrl { get; set; }
    }
}
