using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.Dto.SystemServicesDto.SystemFile
{
    public class SystemFileRequest
    {
        public List<Guid?>? idList { get; set; }

        /// <summary>
        /// 文件信息
        /// </summary>
        public IFormFile? file { get; set; }

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
        /// 关联数据
        /// </summary>
        public List<string>? data_IdArray { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string? data_Type { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? create_User { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? create_Time { get; set; }
    }
}
