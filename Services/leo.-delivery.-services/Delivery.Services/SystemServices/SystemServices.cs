using Delivery.Domains.Dto.OrderServicesDto.LingauDto;
using Delivery.Domains.Dto.SystemServicesDto.SystemDict;
using Delivery.Domains.SystemEntitys;
using Delivery.EntityFramework.Core.DbContexts;
using Delivery.IServices.ISystemServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services.SystemServices
{

    [AutoInject(typeof(ISystemDictServices), InjectTypeEnum.Scope)]
    public class SystemServices : ISystemDictServices
    {
        private readonly SystemDbContext _systemDbContext;
        private readonly IMapper _mapper;

        public SystemServices(SystemDbContext systemDbContext, IMapper mapper)
        {
            _systemDbContext = systemDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SystemDictResponse>> SystemDictFullListAsync(SystemDictRequest systemDictRequest = null)
        {
            var dictList = await SystemDictListAsync(systemDictRequest);
            // 获取层级
            var tempList = dictList.Select(item => _mapper.Map<SystemDictResponse>(item));
            //_mapper.Map<List<SystemDictResponse>>(dictList);
            if (systemDictRequest.isTree)
                return GetDictChildren(tempList, Guid.Empty);
            else
                return tempList;
        }

        public async Task<List<SystemDict>> SystemDictListAsync(SystemDictRequest systemDictRequest = null)
        {
            IQueryable<SystemDict> dictQuery = _systemDbContext.SystemDicts.AsNoTracking();

            if (string.IsNullOrEmpty(systemDictRequest?.dict_Key) == false)
                dictQuery = dictQuery.Where(item => item.dict_Key == systemDictRequest.dict_Key);

            if (string.IsNullOrEmpty(systemDictRequest?.dict_Name) == false)
                dictQuery = dictQuery.Where(item => item.dict_Name == systemDictRequest.dict_Name);

            if (systemDictRequest.ParentId.Guid_IsEmpty() == false)
                dictQuery = dictQuery.Where(item => item.ParentId == systemDictRequest.ParentId || item.Id == systemDictRequest.ParentId);

            if (systemDictRequest?.isSystemBuilt != null)
                dictQuery = dictQuery.Where(item => item.isSystemBuilt == systemDictRequest.isSystemBuilt);

            var dicts = await dictQuery.OrderBy(item => item.expand_Order).OrderBy(item => item.create_Time).ToListAsync();

            return dicts;
        }

        public async Task<ResultMessage> SystemDictSaveAsync(SystemDictRequest systemDictRequest = null)
        {
            var result = true;
            var dict = new SystemDict();
            var dictList = await _systemDbContext.SystemDicts.ToListAsync();
            var dictItem = dictList.Where(item => item.dict_Key == systemDictRequest.dict_Key);
            //&& lingauRequest.Id.Guid_NoEmpty()
            if (dictItem.FirstOrDefault(item => systemDictRequest.Id.Guid_IsEmpty() && item.dict_Key == systemDictRequest.dict_Key && item.ParentId == systemDictRequest.ParentId) != null)
                return new ResultMessage(false, $"保存失败，{systemDictRequest.dict_Name}该字典类型已存在，不允许重复添加");

            //if (dictList.FirstOrDefault(item => item.dict_Name == systemDictRequest.dict_Name && systemDictRequest.ParentId == item.ParentId) != null)
            //    return new ResultMessage(false, $"保存失败，{systemDictRequest.dict_Name}该字典项已存在，不允许重复添加");

            if (systemDictRequest.Id.Guid_IsEmpty())
            {
                dict = _mapper.Map<SystemDict>(systemDictRequest);
                await _systemDbContext.AddAsync(dict);
            }
            else
            {
                dict = dictList.FirstOrDefault(item => item.Id == systemDictRequest.Id);

                dict.ParentId = systemDictRequest.ParentId;
                dict.expand_Order = systemDictRequest.expand_Order;
                dict.expand_Desc = systemDictRequest.expand_Desc;
                dict.dict_Key = systemDictRequest.dict_Key ?? "";
                dict.dict_Name = systemDictRequest.dict_Name ?? "";
                dict.dict_Value = systemDictRequest.dict_Value ?? "";
                dict.dict_JsonValue = systemDictRequest.dict_JsonValue ?? "";
                dict.isSystemBuilt = systemDictRequest.isSystemBuilt ?? false;
                _systemDbContext.Update(dict);

            }
            result &= await _systemDbContext.SaveChangesAsync() > 0;

            return new ResultMessage(result, result ? "保存成功" : "保存失败");

        }

        public async Task<ResultMessage> SystemDictDeleteAsync(SystemDictRequest systemDictRequest = null)
        {
            var result = true;

            var dictList = await _systemDbContext.SystemDicts.Where(item => item.Id == systemDictRequest.Id || item.ParentId == systemDictRequest.Id).ToListAsync();

            if (dictList?.Any() ?? false)
            {
                var dictInfo = dictList.FirstOrDefault(item => item.Id == systemDictRequest.Id);
                if (dictInfo is null)
                    return new ResultMessage(false, "该字典在数据库中不存在！");

                if (dictInfo?.isSystemBuilt ?? false)
                    return new ResultMessage(false, "内置系统字典不允许删除！");

                if (dictList?.Any(item => item.ParentId == systemDictRequest.Id) ?? false)
                    return new ResultMessage(false, "内置系统字典不允许删除！");

                _systemDbContext.Remove(dictInfo);
                result &= (await _systemDbContext.SaveChangesAsync()) > 0;
            }
            return new ResultMessage(result, result ? "删除成功！" : "删除失败！");
        }

        public List<SystemDictResponse> GetDictChildren(IEnumerable<SystemDictResponse> dictList, Guid parentId)
        {
            var resultList = new List<SystemDictResponse>();
            if (dictList?.Any() ?? false)
            {
                var dicts = new List<SystemDictResponse>();

                if (parentId == Guid.Empty)
                    dicts = dictList.Where(item => item.ParentId == parentId || item.ParentId is null).ToList();
                else
                    dicts = dictList.Where(item => item.ParentId == parentId).ToList();

                dictList.Where(item => item.ParentId == parentId);
                if (dicts?.Any() ?? false)
                {
                    foreach (var dict in dicts)
                    {
                        var childrenList = GetDictChildren(dictList, (Guid)dict.Id);
                        if (childrenList?.Any() ?? false)
                            dict.chilren = childrenList;
                        resultList.Add(dict);
                    }
                }
            }
            return resultList;
        }
    }
}
