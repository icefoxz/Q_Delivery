using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace Delivery.Commons.XHelper
{
    public class FileOperateHelper
    {
        private const int CREATEMONTHFOLDERDAYS = 31;

        /// <summary>
        /// 程序根目录
        /// </summary>
        public static string BasePath => _basePath;

        /// <summary>
        /// 程序根目录
        /// </summary>
        public static string _basePath;

        /// <summary>
        /// 设置根目录
        /// </summary>
        /// <param name="basePath"></param>
        public static void SetBasePath(string basePath)
        {
            _basePath = basePath;
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath"></param>
        public static string ReadAllText(string filePath, string jsonNode = "")
        {
            var jObject = JsonConvert.DeserializeObject<JObject>(ReadAllText(filePath));
            if (jObject.Property(jsonNode) != null) return jObject[jsonNode].ToString();
            return string.Empty;
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath"></param>
        public static string ReadAllText(string filePath, bool encode = true)
        {
            if (encode) return ReadAllText(filePath);
            else return File.ReadAllText(CombinePath(filePath));
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath"></param>
        public static string ReadAllText(string filePath)
        {
            return ReadAllText(CombinePath(filePath), encoding: Encoding.UTF8);
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath"></param>
        public static string ReadAllText(string filePath, Encoding encoding)
        {
            return File.ReadAllText(CombinePath(filePath), encoding: encoding);
        }

        /// <summary>
        /// 拼接完整路径
        /// </summary>
        /// <returns></returns>
        public static string CombinePath(string filePath)
        {
            if (FileOperateHelper.BasePath is null)
                return Path.Combine(filePath);
            else
                return Path.Combine(FileOperateHelper.BasePath, filePath);

        }

        /// <summary>
        /// 拼接完整路径
        /// </summary>
        /// <returns></returns>
        public static string CombinesPath(string filePath)
        {
            return $"{FileOperateHelper.BasePath}{filePath}";
        }

        /// <summary>
        /// 连续创建日期天数 默认31天
        /// </summary>
        /// <param name="upFileFolder"></param>
        /// <param name="date"></param>
        /// <param name="days"></param>
        public static void CreateMonthFolder(string upFileFolder, DateTime? date = null, int days = CREATEMONTHFOLDERDAYS)
        {
            if (days == 0) days = CREATEMONTHFOLDERDAYS;

            var beginDate = date.HasValue ? date.Value : DateTime.Now;

            var createDate = beginDate;

            for (int i = 0; i <= days; i++)
            {
                //var dirPath = createDate.ToString(@"/yyyy/MM/dd/");
                var fullFileFolder = Path.Combine(upFileFolder, createDate.ToString("yyyy"), createDate.ToString("MM"), createDate.ToString("dd"));
                CreateFolder(fullFileFolder);
                createDate = createDate.AddDays(1);
            }
        }

        public static void CreateFolder(string fullFileFolder)
        {
            string newFilePath = CombinePath(fullFileFolder);
            // 如果UploadFiles文件夹不存在则先创建

            if (newFilePath != null && !System.IO.Directory.Exists(newFilePath))
            {
                System.IO.Directory.CreateDirectory(newFilePath);
            }
        }

        #region 写文件

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        /// <param name="gzip"></param>
        /// <param name="isBytes"></param>
        public static void WriteFile(
            string fileName,
            string content,
            string encoding = "utf-8",
            bool gzip = false,
            bool isBytes = false)
        {
            #region 创建文件夹
            var directory = string.Empty;

            if (fileName.Contains("/")) directory = fileName.Substring(0, fileName.LastIndexOf("/"));
            if (fileName.Contains(@"\")) directory = fileName.Substring(0, fileName.LastIndexOf(@"\"));

            if (!string.IsNullOrWhiteSpace(directory)) CreateFolder(directory);

            #endregion

            #region gzip处理
            if (gzip) content = GZipHelper.GZipCompressString(content);

            #endregion

            var fullFileName = CombinePath($"{fileName}"); //保存文件的路径
            var encodeContent = Encoding.GetEncoding(encoding);

            if (isBytes)
            {
                using (FileStream fs = new FileStream(fullFileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var bytes = encodeContent.GetBytes(content);

                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            else
            {

                using (var stream = new System.IO.FileStream(fullFileName, System.IO.FileMode.OpenOrCreate))
                {
                    byte[] info = encodeContent.GetBytes(content);
                    stream.Write(info, 0, info.Length);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool WriteJson(JObject content, string url)
        {
            try
            {
                var filePath = CombinePath(url);

                using (var writer = new StreamWriter(filePath))
                {
                    using (JsonTextWriter jsonWriter = new JsonTextWriter(writer)
                    {
                        Formatting = Formatting.Indented,//格式化缩进
                        Indentation = 4,  //缩进四个字符
                        IndentChar = ' '  //缩进的字符是空格
                    })
                    {
                        content.WriteTo(jsonWriter);
                        return true;
                    }
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool WriteJson(JArray content, string url)
        {
            try
            {
                var filePath = CombinePath(url);

                using (var writer = new StreamWriter(filePath))
                {
                    using (JsonTextWriter jsonWriter = new JsonTextWriter(writer)
                    {
                        Formatting = Formatting.Indented,//格式化缩进
                        Indentation = 4,  //缩进四个字符
                        IndentChar = ' '  //缩进的字符是空格
                    })
                    {
                        content.WriteTo(jsonWriter);
                        return true;
                    }
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        /// <param name="gzip"></param>
        /// <param name="isBytes"></param>
        public static void WriteFile<T>(
            string fileName,
            string path,
            T content)
        {
            #region 创建文件夹
            var directory = string.Empty;

            if (fileName.Contains("/"))
                directory = fileName.Substring(0, fileName.LastIndexOf("/"));
            if (fileName.Contains(@"\"))
                directory = fileName.Substring(0, fileName.LastIndexOf(@"\"));

            if (!string.IsNullOrWhiteSpace(directory)) CreateFolder(directory);

            #endregion

            var fullFileName = string.IsNullOrWhiteSpace(path) ? CombinePath($"{fileName}") : $"{path}/{fileName}"; //保存文件的路径

            using (FileStream fs = new FileStream(fullFileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, content);
            }
        }
        #endregion

        #region 取得文件后缀名
        /****************************************
         * 函数名称：GetPostfixStr
         * 功能说明：取得文件后缀名
         * 参    数：filename:文件名称
         * 调用示列：
         *           string filename = "aaa.aspx";        
         *           string s = DotNet.Utilities.FileOperate.GetPostfixStr(filename);         
        *****************************************/
        /// <summary>
        /// 取后缀名
        /// </summary>
        /// <returns>.gif|.html格式</returns>
        public static string GetPostfixStr(string fileName, string contentType = "", bool setDefault = false)
        {
            var fileSuffix = string.Empty;
            int start = fileName.LastIndexOf(".");

            if (start == -1)
            {
                fileSuffix = GetFilePostfix(contentType);
            }
            else
            {
                int length = fileName.Length;
                fileSuffix = fileName.Substring(start, length - start);
            }

            if (string.IsNullOrWhiteSpace(fileSuffix) && setDefault) fileSuffix = "universal";

            return fileSuffix;
        }

        /// <summary>
        /// 根据文件类型转换至后缀
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string GetFilePostfix(string contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType)) return string.Empty;

            var postfixList = GetFilePostfixList();
            var fileSuffixDomain = postfixList.FirstOrDefault(fix => fix.ContentType.ToLower() == contentType.ToLower());

            return fileSuffixDomain?.FileSuffix ?? string.Empty;
        }

        /// <summary>
        /// 文件后缀格式列表
        /// </summary>
        /// <returns></returns>
        public static List<FilePostfix> GetFilePostfixList()
        {
            return new List<FilePostfix>()
            {
                new FilePostfix(){ ContentType="image/jpg",FileSuffix=".jpg"},
                new FilePostfix(){ ContentType="image/jpeg",FileSuffix=".jpg"},
                new FilePostfix(){ ContentType="application/pdf",FileSuffix=".pdf"},
                new FilePostfix(){ ContentType="image/gif",FileSuffix=".gif"},
                new FilePostfix(){ ContentType="image/png",FileSuffix=".png"},
            };
        }

        #region 删除文件
        /****************************************
         * 函数名称：FileDel
         * 功能说明：删除文件
         * 参    数：Path:文件路径
         * 调用示列：
         *           string Path = Server.MapPath("Default3.aspx");    
         *           DotNet.Utilities.FileOperate.FileDel(Path);
        *****************************************/
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Path">路径</param>
        public static void FileDel(string Path)
        {
            if (File.Exists(Path))
                File.Delete(Path);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Path">路径</param>
        public static void FileDelete(string Path)
        {
            if (Directory.Exists(Path))
            {
                DirectoryInfo di = new DirectoryInfo(Path);
                di.Delete(true);
            }
        }



        #endregion

        #endregion


        /// <summary>
        /// 文件类型格式封装
        /// </summary>
        public class FilePostfix
        {

            /// <summary>
            /// 文件类型
            /// </summary>
            public string ContentType { get; set; }

            /// <summary>
            /// 后缀
            /// </summary>
            public string FileSuffix { get; set; }
        }

        /// <summary>
        /// 根据报表模板名称获取模板文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetReportGrfValue(string filePath)
        {
            if (!File.Exists(filePath)) return "";

            var grfText = File.ReadAllText(filePath, encoding: Encoding.UTF8);

            return grfText;
        }

        /// <summary>
        /// 根据报表模板名称获取模板文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetReportXlsxValue(string filePath)
        {
            var fileFullPath = FileOperateHelper.BasePath + "/ReportToExcle/" + filePath;

            if (!File.Exists(fileFullPath))
                return "";

            var grfText = File.ReadAllText(fileFullPath, encoding: Encoding.UTF8);

            return grfText;
        }


        /// <summary>
        /// 获取文件的MD5值
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string CheckFileMd5(Stream stream)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(stream);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            var fileMD5 = sb.ToString();
            return fileMD5;
        }
    }

}
