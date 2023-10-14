using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.BaseEntitys
{
    public class BasePageEntity  
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int current_PageIndex { get; set; }

        /// <summary>
        /// 当前页数据量
        /// </summary>
        public int page_Size { get; set; }

    }
}
