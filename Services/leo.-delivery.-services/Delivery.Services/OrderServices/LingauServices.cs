

using Delivery.Commons.Cookie;
using Delivery.Domains.OrderEntitys;

namespace Delivery.Services.OrderServices
{

    [AutoInject(typeof(ILingauServices), InjectTypeEnum.Scope)]
    public class LingauServices : ILingauServices
    {
        private readonly IPersonServices _personServices;
        private readonly OrderDbContext _orderDbContext;
        private readonly IMapper _mapper;

        public LingauServices(OrderDbContext dbContext, IPersonServices personServices, IMapper mapper)
        {
            _personServices = personServices;
            _orderDbContext = dbContext;
            _mapper = mapper;
        }

        #region Lingau

        /// <summary>
        /// Lingau
        /// </summary>
        /// <param name="lingauRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LingauResponse>> LingauFullListAsync(LingauRequest lingauRequest = null)
        {
            var resultList = new List<LingauResponse>();
            var list = await LingauListAsync(lingauRequest);
            if (list?.Any() ?? false)
            {
                var personIdList = list.Select(item => (Guid?)item.person_Id).ToList();

                var personList = await _personServices.PersonListAsync(new PersonRequest() { IdList = personIdList });

                resultList = list?.toVo(personList);
            }
            return resultList;
        }

        public async Task<List<Lingau>> LingauListAsync(LingauRequest lingauRequest = null)
        {
            IQueryable<Lingau> lingauQuery = _orderDbContext.Lingaus.AsNoTracking();

            if (lingauRequest?.person_Id.Guid_IsEmpty() == false)
                lingauQuery = lingauQuery.Where(item => item.person_Id == lingauRequest.person_Id);

            var lingaus = await lingauQuery.OrderBy(item => item.expand_Order).OrderBy(item => item.create_Time).ToListAsync();

            return lingaus;
        }

        public async Task<ResultMessage> LingauSaveAsync(LingauRequest lingauRequest = null)
        {
            var result = true;
            var lingau = new Lingau();
            var lingauList = await _orderDbContext.Lingaus.Where(item => item.person_Id == lingauRequest.person_Id || item.Id == lingauRequest.Id).ToListAsync();

            if (lingauList.FirstOrDefault(item => item.person_Id == lingauRequest.person_Id) != null && lingauRequest.Id.Guid_IsEmpty())
                return new ResultMessage(false, $"保存失败，{lingauRequest.person_Name}该人员已存在，不允许重复添加");

            if (lingauRequest.Id.Guid_IsEmpty())
            {
                lingau = _mapper.Map<Lingau>(lingauRequest);
                await _orderDbContext.AddAsync(lingau);

                await _orderDbContext.AddAsync(new Lingau_OptLog()
                {
                    opt_BeginAmount = 0,
                    opt_EndAmount = lingau.lingau_Balance,
                    opt_BeUser = lingauRequest.person_Name ?? "",
                    create_User = "默认",// 读取Session
                    opt_User = "默认",// 读取Session
                    opt_Type = "Insert",
                });
            }
            else
            {
                lingau = lingauList.FirstOrDefault(item => item.Id == lingauRequest.Id);

                await _orderDbContext.AddAsync(new Lingau_OptLog()
                {
                    opt_BeginAmount = lingau.lingau_Balance,
                    opt_EndAmount = lingauRequest.lingau_Balance ?? default,
                    opt_BeUser = lingauRequest.person_Name ?? "",
                    create_User = "默认",// 读取Session
                    opt_User = "默认",// 读取Session
                    opt_Type = "Edit",
                });

                lingau.person_Id = lingauRequest.person_Id ?? default;
                lingau.lingau_Balance = lingauRequest.lingau_Balance ?? default;
                _orderDbContext.Update(lingau);

            }

            result &= await _orderDbContext.SaveChangesAsync() > 0;

            return new ResultMessage(result, result ? "保存成功" : "保存失败");

        }

        public async Task<ResultMessage> LingauDeleteAsync(LingauRequest lingauRequest = null)
        {
            var result = true;

            var lingauInfo = await _orderDbContext.Lingaus.SingleOrDefaultAsync(item => item.Id == lingauRequest.Id);
            if (lingauInfo != null)
            {
                _orderDbContext.Remove(lingauInfo);
                await _orderDbContext.AddAsync(new Lingau_OptLog()
                {
                    opt_BeginAmount = lingauInfo.lingau_Balance,
                    opt_EndAmount = 0,
                    opt_BeUser = "暂未处理",
                    create_User = UserInfoCookie.user_Name,
                    opt_User = UserInfoCookie.user_Name,
                    opt_Type = "Delete",
                });

                result &= (await _orderDbContext.SaveChangesAsync()) > 0;
            }
            return new ResultMessage(result, result ? "删除成功！" : "删除失败！");
        }



        #endregion

        #region LingauOptLog

        public async Task<IEnumerable<LingauOptLogResponse>> LingauOptLogFullListAsync(LingauOptLogRequest lingauOptLogRequest = null)
        {
            var list = await LingauOptLogListAsync(lingauOptLogRequest);

            return list?.Select(l => _mapper.Map<LingauOptLogResponse>(l)) ?? new List<LingauOptLogResponse>();
        }

        public async Task<List<Lingau_OptLog>> LingauOptLogListAsync(LingauOptLogRequest lingauOptLogRequest = null)
        {
            IQueryable<Lingau_OptLog> lingauOptQuery = _orderDbContext.Lingau_OptLogs.AsNoTracking();

            if (!string.IsNullOrEmpty(lingauOptLogRequest?.opt_BeUser))
                lingauOptQuery = lingauOptQuery.Where(item => item.opt_BeUser.Contains(lingauOptLogRequest.opt_BeUser));

            if (!string.IsNullOrEmpty(lingauOptLogRequest?.opt_User))
                lingauOptQuery = lingauOptQuery.Where(item => item.opt_User.Contains(lingauOptLogRequest.opt_User));

            if (lingauOptLogRequest.begin_Time != null)
                lingauOptQuery = lingauOptQuery.Where(item => item.create_Time > lingauOptLogRequest.begin_Time);

            if (lingauOptLogRequest.end_Time != null)
                lingauOptQuery = lingauOptQuery.Where(item => item.create_Time < lingauOptLogRequest.end_Time);

            if (lingauOptLogRequest.opt_Type != null)
                lingauOptQuery = lingauOptQuery.Where(item => item.opt_Type == lingauOptLogRequest.opt_Type);

            var lingauOpts = await lingauOptQuery.OrderBy(item => item.expand_Order).OrderBy(item => item.create_Time).ToListAsync();

            return lingauOpts;
        }

        public async Task<bool> LingauOptLogSaveAsync(LingauOptLogRequest lingauOptLogRequest = null)
        {
            var result = true;
            var lingauOpt = _mapper.Map<Lingau_OptLog>(lingauOptLogRequest);
            if (lingauOpt != null)
            {
                await _orderDbContext.AddAsync(lingauOpt);
            }

            result &= await _orderDbContext.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<ResultMessage> LingauOptLogDeleteAsync(LingauOptLogRequest lingauOptLogRequest = null)
        {
            var result = true;

            var lingauOptInfo = await _orderDbContext.Lingau_OptLogs.SingleOrDefaultAsync(item => item.Id == lingauOptLogRequest.Id);
            if (lingauOptInfo != null)
            {
                _orderDbContext.Remove(lingauOptInfo);
                result &= (await _orderDbContext.SaveChangesAsync()) > 0;
            }
            return new ResultMessage(result, result ? "删除成功！" : "删除失败！");
        }


        #endregion
    }
}
