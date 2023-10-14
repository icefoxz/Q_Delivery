using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.SystemEntitys
{
    public class SystemFile : BaseEntity
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string? file_Name { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string? file_Path { get; set; }

        /// <summary>
        /// 关联数据
        /// </summary>
        public string? data_Id { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string? data_Type { get; set; }

    }
}
