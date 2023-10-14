namespace Delivery.WebMvc1.Jwt
{
    /// <summary>
    /// 01-API
    /// </summary>
    public class ApiPermission
    {
        /// <summary>
        /// API名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// API地址
        /// </summary>
        public virtual string Url { get; set; }
    }

}
