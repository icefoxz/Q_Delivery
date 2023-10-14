using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domains.UserEntitys
{
    /// <summary>
    /// 职位表
    /// </summary>
    public class Job : BaseEntity
    {
        /// <summary>
        /// 职位名称
        /// </summary>
        public string job_Name { get; set; }

        /// <summary>
        /// 职位所属单位
        /// </summary>
        public Guid? dept_Id { get; set; }

        /// <summary>
        /// 职位所属单位
        /// </summary>
        public Dept deptInfo { get; set; }

        /// <summary>
        /// 该单位的所有人员
        /// </summary>
        public List<Person> personInfos { get; set; } = new List<Person>();

        public Job() { }

        public Job(string job_Name)
        {
            this.job_Name = job_Name;
        }
    }
}
