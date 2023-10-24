
using Delivery.Domains.Dto.Vo;

namespace Delivery.Services.OrderServices
{
    [AutoInject(typeof(ITagManagerServices), InjectTypeEnum.Scope)]
    public class TagManagerServices : ITagManagerServices
    {
        private readonly OrderDbContext _orderDbContext;
        private readonly ITagServices _tagServices;

        public TagManagerServices(OrderDbContext orderDbContext, ITagServices tagServices)
        {
            _orderDbContext = orderDbContext;
            _tagServices = tagServices;
        }

        public async Task<IEnumerable<TagManagerResponse>> TagManagerFullListAsync(TagManagerRequest tagManagerRequest = null)
        {
            var tagManagerList = await TagManagerListAsync(tagManagerRequest);
            var tagIdArray = tagManagerList.Select(item => item.tag_Id).ToList();
            var tagList = await _tagServices.TagListAsync(new TagRequest() { IdList = tagIdArray });

            var resultList = tagManagerList.toVo(tagList);
            if (tagManagerRequest.isAll == false)
                resultList = resultList.Where(item => item.isEnable == true);

            return resultList;
        }

        public async Task<List<Tag_Manager>> TagManagerListAsync(TagManagerRequest tagManagerRequest = null)
        {
            IQueryable<Tag_Manager> tagManagerQuery = _orderDbContext.Tag_Managers.AsNoTracking();

            //if (string.IsNullOrEmpty(tagManagerRequest?.tag_Name) == false)
            //    tagManagerQuery = tagManagerQuery.Where(item => item.tag_Id == tagManagerRequest!.tag_Id);

            if (tagManagerRequest?.tag_Id.Guid_IsEmpty() == false)
                tagManagerQuery = tagManagerQuery.Where(item => item.tag_Id == tagManagerRequest!.tag_Id);

            if (tagManagerRequest?.tag_Type > 0 && tagManagerRequest?.tag_Type != null)
                tagManagerQuery = tagManagerQuery.Where(item => item.tag_Type == tagManagerRequest.tag_Type.ToString());

            if (tagManagerRequest?.IdList?.Any() ?? false)
                tagManagerQuery = tagManagerQuery.Where(item => tagManagerRequest.IdList.Contains(item.Id));

            if (string.IsNullOrEmpty(tagManagerRequest?.expand_Desc) == false)
                tagManagerQuery = tagManagerQuery.Where(item => item.expand_Desc.Contains(tagManagerRequest.expand_Desc));

            var tagManagers = await tagManagerQuery.OrderBy(item => item.expand_Order).OrderBy(item => item.create_Time).ToListAsync();

            return tagManagers;
        }

        public async Task<ResultMessage> TagManagerSaveAsync(TagManagerRequest tagManagerRequest = null)
        {
            var result = true;
            var tagManagerList = await _orderDbContext.Tag_Managers.AsNoTracking().ToListAsync();

            if (tagManagerList.Any(item => item.tag_Id == tagManagerRequest?.tag_Id && item.tag_Type == tagManagerRequest.tag_Type.ToString()))
                return new ResultMessage(false, $"已存在重复的标签所属信息，操作失败！");

            var tagManager = tagManagerList.FirstOrDefault(item => item.Id == (Guid)(tagManagerRequest.Id ?? default)) ?? new Tag_Manager();
            tagManager.tag_Id = tagManagerRequest.tag_Id;
            tagManager.tag_Type = tagManagerRequest.tag_Type?.ToString();
            tagManager.expand_Desc = tagManagerRequest.expand_Desc;

            if (tagManagerRequest.Id.Guid_IsEmpty())
                await _orderDbContext.AddAsync(tagManager);
            else
                _orderDbContext.Update(tagManager);

            result &= await _orderDbContext.SaveChangesAsync() > 0;

            return new ResultMessage(result, result ? "保存成功" : "保存失败");
        }

        public async Task<ResultMessage> TagManagerDeleteAsync(TagManagerRequest tagManagerRequest = null)
        {
            var result = true;
            //// 验证是否绑定过订单
            //if ((await _orderDbContext.jo.Where(item => item.dept_Id == jobRequest.Id).ToListAsync())?.Any() ?? false)
            //    return new ResultMessage(false, "该单位已绑定人员不允许删除");

            var tagManagerInfo = await _orderDbContext.Tag_Managers.SingleOrDefaultAsync(item => item.Id == tagManagerRequest.Id);
            if (tagManagerInfo != null)
            {
                _orderDbContext.Remove(tagManagerInfo);
                result &= (await _orderDbContext.SaveChangesAsync()) > 0;
            }
            return new ResultMessage(result, result ? "删除成功！" : "删除失败！");
        }

    }
}
