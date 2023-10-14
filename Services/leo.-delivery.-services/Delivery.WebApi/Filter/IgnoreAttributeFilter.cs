using Microsoft.AspNetCore.Mvc.Filters;

namespace Delivery.WebApi.Filter
{
    public class IgnoreAttributeFilter : Attribute, IFilterMetadata
    {
        ///允许未通过身份验证也可以访问的特性
    }
}
