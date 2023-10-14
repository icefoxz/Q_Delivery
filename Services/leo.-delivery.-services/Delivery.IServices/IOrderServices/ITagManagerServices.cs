using Delivery.Commons.Result;
using Delivery.Domains.Dto.OrderServicesDto.TagDto;
using Delivery.Domains.Dto.OrderServicesDto.TagManagerDto;
using Delivery.Domains.OrderEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.IServices.IOrderServices
{
    public interface ITagManagerServices
    {
        Task<List<Tag_Manager>> TagManagerListAsync(TagManagerRequest tagManagerRequest = null);
        Task<IEnumerable<TagManagerResponse>> TagManagerFullListAsync(TagManagerRequest tagManagerRequest = null);
        Task<ResultMessage> TagManagerSaveAsync(TagManagerRequest tagManagerRequest = null);
        Task<ResultMessage> TagManagerDeleteAsync(TagManagerRequest tagManagerRequest = null);

    }
}
