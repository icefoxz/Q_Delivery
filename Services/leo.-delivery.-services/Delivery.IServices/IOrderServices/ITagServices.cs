using Delivery.Commons.Result;
using Delivery.Domains.Dto.OrderServicesDto.TagDto;
using Delivery.Domains.OrderEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.IServices.IOrderServices
{
    public interface ITagServices
    {
        Task<List<Tag>> TagListAsync(TagRequest TagRequest = null);
        Task<IEnumerable<TagResponse>> TagFullListAsync(TagRequest TagRequest = null);
        Task<ResultMessage> TagSaveAsync(TagRequest TagRequest = null);
        Task<ResultMessage> TagDeleteAsync(TagRequest TagRequest = null);
        Task<ResultMessage> TagDisableAsync(TagRequest TagRequest = null);

    }
}
