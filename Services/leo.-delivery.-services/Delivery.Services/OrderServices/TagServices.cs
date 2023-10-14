using Delivery.Commons.XHelper;
using Delivery.Domains.Dto.OrderServicesDto.TagDto;
using Delivery.Domains.Dto.OrderServicesDto.TagManagerDto;
using Delivery.Domains.Dto.UserServicesDto.JobDto;
using Delivery.Domains.Dto.Vo;
using Delivery.Domains.OrderEntitys;
using Delivery.EntityFramework.Core.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services.OrderServices
{
    [AutoInject(typeof(ITagServices), InjectTypeEnum.Scope)]
    public class TagServices : ITagServices
    {
        private readonly OrderDbContext _orderDbContext;

        public TagServices(OrderDbContext dbContext)
        {
            _orderDbContext = dbContext;
        }

        /// <summary>
        /// 获取订单标签信息
        /// </summary>
        /// <param name="TagRequest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TagResponse>> TagFullListAsync(TagRequest TagRequest = null)
        {
            var tagList = await TagListAsync(TagRequest);

            return tagList.toVo();
        }

        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <param name="TagRequest"></param>
        /// <returns></returns>
        public async Task<List<Tag>> TagListAsync(TagRequest TagRequest = null)
        {
            IQueryable<Tag> tagQuery = _orderDbContext.Tags.AsNoTracking();

            if (!TagRequest.isAll)
                tagQuery.Where(item => item.isEnable == true);
            //    tagQuery.Where(item => item.isEnable == TagRequest.isAll);
            //else
            //    tagQuery.Where(item => item.isEnable == false);

            if (string.IsNullOrWhiteSpace(TagRequest?.tag_Name) == false)
                tagQuery = tagQuery.Where(item => item.tag_Name.Contains(TagRequest.tag_Name));

            else if (TagRequest?.IdList?.Any() ?? false)
                tagQuery = tagQuery.Where(item => TagRequest.IdList.Contains(item.Id));

            if (string.IsNullOrEmpty(TagRequest?.expand_Desc) == false)
                tagQuery = tagQuery.Where(item => item.expand_Desc!.Contains(TagRequest.expand_Desc));

            var tags = await tagQuery.OrderBy(item => item.expand_Order).OrderBy(item => item.create_Time).ToListAsync();

            return tags;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="TagRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> TagSaveAsync(TagRequest TagRequest = null)
        {
            var result = true;
            var isVerifyDept = false;
            var tag = new Tag();
            var tagList = await _orderDbContext.Tags.ToListAsync();
            if (TagRequest.Id.Guid_NoEmpty())
            {
                if (tagList.Any(item => item.tag_Name == TagRequest.tag_Name))
                    return new ResultMessage(false, $"{TagRequest.tag_Name}已存在该名称相同的标签");
            }
            else
            {
                // Edit
                var tagTemp = tagList.FirstOrDefault(item => item.Id == TagRequest.Id);
                if (tagTemp?.tag_Name != TagRequest.tag_Name)
                    if (tagList.Any(item => (item.Id != TagRequest.Id && item.tag_Name == TagRequest.tag_Name)))
                        return new ResultMessage(false, $"{TagRequest.tag_Name}已存在该名称相同的标签");

                tag = tagTemp;
            }
            tag.tag_Name = TagRequest.tag_Name ?? "";
            tag.expand_Desc = TagRequest.expand_Desc;

            if (TagRequest.Id.Guid_NoEmpty())
                await _orderDbContext.Tags.AddAsync(tag);
            else
                _orderDbContext.Tags.Update(tag);

            result &= await _orderDbContext.SaveChangesAsync() > 0;

            return new ResultMessage(result, result ? "保存成功" : "保存失败");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="TagRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> TagDeleteAsync(TagRequest TagRequest = null)
        {
            var result = true;

            //// 验证是否绑定过职位 
            //if ((await _orderDbContext.jo.Where(item => item.dept_Id == jobRequest.Id).ToListAsync())?.Any() ?? false)
            //    return new ResultMessage(false, "该单位已绑定人员不允许删除");

            var tagInfo = await _orderDbContext.Tags.SingleOrDefaultAsync(item => item.Id == TagRequest.Id);
            if (tagInfo != null)
            {
                _orderDbContext.Remove(tagInfo);
                result &= (await _orderDbContext.SaveChangesAsync()) > 0;
            }
            return new ResultMessage(result, result ? "删除成功！" : "删除失败！");
        }

        /// <summary>
        /// 禁用启用
        /// </summary>
        /// <param name="TagRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> TagDisableAsync(TagRequest TagRequest = null)
        {
            var result = true;
            var tagInfo = await _orderDbContext.Tags.SingleOrDefaultAsync(item => item.Id == TagRequest.Id);
            if (tagInfo != null)
            {
                tagInfo.isEnable = TagRequest.isEnable;
                _orderDbContext.Update(tagInfo);
                result &= (await _orderDbContext.SaveChangesAsync()) > 0;
            }
            return new ResultMessage(result, result ? "操作成功！" : "操作失败！");
        }
    }
}
