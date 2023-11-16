using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.SystemServicesDto.SystemDict
{
    public class SystemDictResponse : SystemDictRequest
    {
        /// <summary>
        /// 上级Key
        /// </summary>
        public string dict_ParentKey { get; set; }
    }
}
