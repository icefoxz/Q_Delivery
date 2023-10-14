using Delivery.Commons.Result;
using Delivery.Domains.Dto.UserServicesDto.DeptDto;
using Delivery.Domains.UserEntitys;

namespace Delivery.IServices.IUserServices
{
    public interface IDeptServices
    {
        Task<ResultMessage> DeptSaveAsync(DeptRequest deptRequest=null);
        Task<ResultMessage> DeptDeleteAsync(DeptRequest deptRequest = null);
        Task<IEnumerable<Dept>> DeptListAsync(DeptRequest deptRequest = null);

        /// <summary>
        /// 获取上级单位列表
        /// </summary>
        /// <param name="deptRequest"></param>
        /// <returns></returns>
        Task<ResultMessage> DeptParentListAsync(DeptRequest deptRequest = null);
        Task<ResultMessage> DeptFullListAsync(DeptRequest deptRequest = null);
    }
}