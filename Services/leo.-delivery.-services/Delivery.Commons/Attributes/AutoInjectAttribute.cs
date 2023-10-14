

namespace Delivery.Commons.Attributes
{
    /// <summary>
    /// 注入特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AutoInjectAttribute : Attribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="interfaceType">接口类型</param>
        /// <param name="injectType">注入类型</param>
        public AutoInjectAttribute(Type interfaceType, InjectTypeEnum injectType)
        {
            Type = interfaceType;
            InjectType = injectType;
        }

        public Type Type { get; set; }

        /// <summary>
        /// 注入类型
        /// </summary>
        public InjectTypeEnum InjectType { get; set; }
      
    }
}
