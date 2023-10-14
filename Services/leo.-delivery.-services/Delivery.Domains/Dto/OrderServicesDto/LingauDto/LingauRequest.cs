using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.OrderServicesDto.LingauDto
{
    public class LingauRequest
    {

        /// <summary>
        /// 标识
        /// </summary>
        public Guid? Id { get; set; }


        /// <summary>
        /// 令凹币余额
        /// </summary>
        public decimal? lingau_Balance { get; set; }

        /// <summary>
        /// 所属人员
        /// </summary>
        public Guid? person_Id { get; set; }


        /// <summary>
        /// 所属人员
        /// </summary>
        public string? person_Name { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? create_User { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string? create_Time { get; set; }
    }
}
