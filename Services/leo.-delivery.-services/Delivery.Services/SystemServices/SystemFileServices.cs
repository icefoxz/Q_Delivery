using Delivery.Commons.Config;
using Delivery.Commons.Const;
using Delivery.Domains.Dto.SystemServicesDto.SystemFile;
using Delivery.Domains.SystemEntitys;
using Delivery.IServices.ISystemServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services.SystemServices
{
    [AutoInject(typeof(ISystemFileServices), InjectTypeEnum.Scope)]
    public class SystemFileServices : ISystemFileServices
    {
        private readonly SystemDbContext _systemDbContext;
        private ILogger<SystemFileServices> _logger;

        public SystemFileServices(SystemDbContext systemDbContext, ILogger<SystemFileServices> logger)
        {
            _systemDbContext = systemDbContext;
            _logger = logger;
        }

        public async Task<List<SystemFile>> SystemFileListAsync(SystemFileRequest systemFileRequest = null)
        {
            IQueryable<SystemFile> query = _systemDbContext.SystemFiles.AsNoTracking();

            if (systemFileRequest.idList?.Any() ?? false)
                query = query.Where(item => systemFileRequest.idList.Contains(item.Id));

            if (systemFileRequest.data_Id != null)
                query = query.Where(item => item.data_Id == systemFileRequest.data_Id);
            else if (systemFileRequest.data_IdArray?.Any() ?? false)
                query = query.Where(item => systemFileRequest.data_IdArray.Contains(item.data_Id ?? ""));

            if (string.IsNullOrEmpty(systemFileRequest.data_Type) == false)
                query = query.Where(item => item.data_Type == systemFileRequest.data_Type);

            return await query.ToListAsync();
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="systemFileRequest"></param>
        /// <returns></returns>
        public async Task<ResultMessage> SystemFileSaveAsync(SystemFileRequest systemFileRequest = null)
        {
            if (systemFileRequest.file is null)
                return new ResultMessage(false, "未上传文件"); ;
            //if (string.IsNullOrEmpty(systemFileRequest.data_Id))
            //    return new ResultMessage(false, "未传递数据标识"); ;

            try
            {
                var imgUrl = ConfigHelp.GetString("imgUrl");
                var imgUrlFolder = ConfigHelp.GetString("imgUrlFolder");
                var filePath = await StorageLocal(systemFileRequest.file, imgUrl);
                var file = new SystemFile()
                {
                    data_Id = systemFileRequest.data_Id,
                    file_Name = systemFileRequest.file.FileName,
                    file_Path = filePath,
                    data_Type = SystemFileConst.orderImgUrl,
                };
                await _systemDbContext.AddAsync(file);
                await _systemDbContext.SaveChangesAsync();
                return new ResultMessage(true, "上传成功", new { id = file.Id, filePath = Path.Combine(imgUrlFolder, filePath) });
            }
            catch (Exception ex)
            {
                _logger.LogError($"文件上传失败！{ex.Message}");
                return new ResultMessage(false, "上传失败");
            }
        }
        private async Task<string> StorageLocal(IFormFile file, string imgUrl = "")
        {
            string uploadFileFolder;
            var configKey = string.Empty;

            //var config = ConfigFileHelper.GetConfig<CommonConfig>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CommonConfigConst.systemConfigUrl));
            if (string.IsNullOrEmpty(imgUrl))
                imgUrl = ConfigHelp.GetString("imgUrl");

            //获取路径配置
            //var config = await _configService.GetByConfigKey(CateGoryConst.Config_FILE_LOCAL, configKey);
            if (!string.IsNullOrEmpty(imgUrl))
            {
                uploadFileFolder = imgUrl;//赋值路径
                var now = DateTime.Now.ToString("d");
                var filePath = Path.Combine(uploadFileFolder, now);
                if (!Directory.Exists(filePath))//如果不存在就创建文件夹
                    Directory.CreateDirectory(filePath);

                //var fileSuffix = Path.GetExtension(file.FileName).ToLower();// 文件后缀
                //$"{fileId}{fileSuffix}";//存储后的文件名

                var fileObjectName = file.FileName;
                var fileName = Path.Combine(filePath, fileObjectName);//获取文件全路局
                var saveFileName = Path.Combine(now, fileObjectName);
                fileName = fileName.Replace("\\", "/");//格式化一系
                saveFileName = saveFileName.Replace("\\", "/");                                  //存储文件
                using (var stream = File.Create(Path.Combine(filePath, fileObjectName)))
                {
                    await file.CopyToAsync(stream);
                }
                return saveFileName;
            }
            else
            {

                return "";
            }
        }
    }
}
