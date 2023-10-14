using Delivery.Commons.Result;
using Delivery.Domains.Dto.UserServicesDto.PersonDto;
using Delivery.Domains.UserEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.IServices.IUserServices
{
    public interface IPersonServices
    {
        Task<ResultMessage> PersonSaveAsync(PersonRequest personRequest = null);
        Task<ResultMessage> PersonDeleteAsync(PersonRequest personRequest = null);
        Task<IEnumerable<Person>> PersonListAsync(PersonRequest personRequest = null);
        Task<IEnumerable<PersonResponse>> PersonFullListAsync(PersonRequest personRequest = null);
        Task<IEnumerable<PersonResponse>> PersonFullListByPersonTypeAsync(PersonRequest personRequest = null);
    }
}
