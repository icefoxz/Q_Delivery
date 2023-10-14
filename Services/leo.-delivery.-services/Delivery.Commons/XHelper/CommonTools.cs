using Delivery.Commons.Cookie;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Delivery.Commons.XHelper
{
    public class CommonTools
    {

        #region 根据配置对指定字符串进行 MD5 加密
        /// <summary>
        /// 根据配置对指定字符串进行 MD5 加密
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string GetMD5(string inputValue)
        {
            using (var md5 = MD5.Create())
            {
                var data = md5.ComputeHash(Encoding.UTF8.GetBytes(inputValue));
                StringBuilder builder = new StringBuilder();
                // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串 
                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("X2"));
                }
                var result = builder.ToString().Substring(8, 16);
                return result;
            }
        }
        #endregion

        #region 得到字符串长度，一个汉字长度为2
        /// <summary>
        /// 得到字符串长度，一个汉字长度为2
        /// </summary>
        /// <param name="inputString">参数字符串</param>
        /// <returns></returns>
        public static int StrLength(string inputString)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }
            return tempLen;
        }
        #endregion

        #region string TO Bytes

        /// <summary>
        /// 字符串转二进制
        /// </summary>
        /// <returns></returns>
        public static byte[] StringToBytes(string content)
        {
            if (string.IsNullOrWhiteSpace(content)) return null;
            return System.Text.Encoding.UTF8.GetBytes(content);
        }
        #endregion

        #region Bytes TO string

        /// <summary>
        /// 字符串转二进制
        /// </summary>
        /// <returns></returns>
        public static string BytesToString(byte[] byteArray)
        {
            if (byteArray == null) return string.Empty;
            return System.Text.Encoding.UTF8.GetString(byteArray);
        }
        #endregion

        #region 截取指定长度字符串
        /// <summary>
        /// 截取指定长度字符串
        /// </summary>
        /// <param name="inputString">要处理的字符串</param>
        /// <param name="len">指定长度</param>
        /// <returns>返回处理后的字符串</returns>
        public static string ClipString(string inputString, int len)
        {
            bool isShowFix = false;
            if (len % 2 == 1)
            {
                isShowFix = true;
                len--;
            }
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }

            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (isShowFix && mybyte.Length > len)
                tempString += "…";
            return tempString;
        }
        #endregion

        #region HTML转行成TEXT
        /// <summary>
        /// HTML转行成TEXT
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlToTxt(string strHtml)
        {
            string[] aryReg ={
            @"<script[^>]*?>.*?</script>",
            @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
            @"([\r\n])[\s]+",
            @"&(quot|#34);",
            @"&(amp|#38);",
            @"&(lt|#60);",
            @"&(gt|#62);",
            @"&(nbsp|#160);",
            @"&(iexcl|#161);",
            @"&(cent|#162);",
            @"&(pound|#163);",
            @"&(copy|#169);",
            @"&#(\d+);",
            @"-->",
            @"<!--.*\n"
            };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, string.Empty);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");


            return strOutput;
        }
        #endregion

        #region 判断对象是否为空
        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>        
        public static bool IsNullOrEmpty<T>(T data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }

        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty(object data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }
        #endregion

        #region  验证日期是否合法
        /// <summary>
        /// 验证日期是否合法
        /// </summary>
        /// <param name="dateTime">要验证的dateTime</param>
        public static bool IsDateTime(string dateTime)
        {
            //如果为空，认为验证不合格
            if (IsNullOrEmpty(dateTime))
            {
                return false;
            }

            //清除要验证字符串中的空格
            dateTime = dateTime.Trim();

            var dateTimeObj = DateTime.Now;

            //验证
            return DateTime.TryParse(dateTime, out dateTimeObj);
        }

        #endregion

        #region 是否财务公司银行卡规则【只能是数字和-】

        /// <summary>
        /// 是否财务公司银行卡规则【只能是数字和-】
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static bool IsCsairFinanceBankCard(string txt)
        {
            Regex objReg = new Regex(@"^[0-9-]{1,100}$");
            return objReg.IsMatch(txt);
        }
        #endregion

        #region 检测客户的输入中是否有危险字符串
        /// <summary>
        /// 检测客户输入的字符串是否有效,并将原始字符串修改为有效字符串或空字符串。
        /// 当检测到客户的输入中有攻击性危险字符串,则返回false,有效返回true。
        /// </summary>
        /// <param name="input">要检测的字符串</param>
        public static bool IsValidInput(ref string input)
        {
            try
            {
                if (IsNullOrEmpty(input))
                {
                    //如果是空值,则跳出
                    return true;
                }
                else
                {
                    //替换单引号
                    input = input.Replace("'", "''").Trim();

                    //检测攻击性危险字符串
                    string testString = "and |or |exec |insert |select |delete |update |count |chr |mid |master |truncate |char |declare ";
                    string[] testArray = testString.Split('|');
                    foreach (string testStr in testArray)
                    {
                        if (input.ToLower().IndexOf(testStr) != -1)
                        {
                            //检测到攻击字符串,清空传入的值
                            input = "";
                            return false;
                        }
                    }

                    //未检测到攻击字符串
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 序列化处理

        /// <summary>
        /// 序列化处理
        /// </summary>
        public static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,//设置忽略值为 null 的属性    
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static SortedDictionary<string, object> KeySort(JObject obj)
        {
            var res = new SortedDictionary<string, object>();
            foreach (var x in obj)
            {
                if (x.Value is JValue) res.Add(x.Key, x.Value);
                else if (x.Value is JObject) res.Add(x.Key, KeySort((JObject)x.Value));
                else if (x.Value is JArray)
                {
                    var tmp = new SortedDictionary<string, object>[x.Value.Count()];
                    for (var i = 0; i < x.Value.Count(); i++)
                    {
                        tmp[i] = KeySort((JObject)x.Value[i]);
                    }
                    res.Add(x.Key, tmp);
                }
            }
            return res;
        }

        /// <summary>
        /// 序列化Object为JSON
        /// </summary>
        /// <param name="o">Object</param>
        /// <returns>忽略值为 null 的属性、驼峰命名的JSON字符串</returns>
        public static string SerializeObject(object o)
        {
            string json = JsonConvert.SerializeObject(o, SerializerSettings);
            return json;
        }
        #endregion

        #region 自动分页匹配

        /// <summary>
        /// 自动分页匹配
        /// </summary>
        /// <param name="itemCount">总条数</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        public static int GetPageCount(int itemCount, int pageSize)
        {
            return (int)Math.Ceiling(itemCount / (double)pageSize);
        }
        #endregion

        #region 增加百分比

        /// <summary>
        /// 增加百分比
        /// </summary>
        /// <param name="val">总条数</param>
        /// <returns></returns>
        public static string ToIncreasePercentage(string val)
        {
            return val + "%";
        }


        #endregion

        #region json格式清除

        /// <summary>
        /// json格式清除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetJsonClear(string input)
        {
            input = input.Replace("\r\n", "");
            input = input.Replace(" ", "");
            input = input.Replace("'", "");
            input = Regex.Replace(input, @"\s", "");
            input = input.Replace("↵", "");
            input = input.Replace("\n", "");
            input = input.Replace("[", "");
            input = input.Replace("~", "");
            input = input.Replace("]", "");
            input = input.Replace(":", "");
            //input = input.Replace(":null,", ":,");
            input = input.Replace("\"", "");
            string pattern = "\"(\\w+)\"(\\s*:\\s*)";
            string replacement = "$1$2";
            System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(pattern);
            input = rgx.Replace(input, replacement);

            return input;
        }

        /// <summary>
        /// json格式清楚
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetJsonKeyClear(string input)
        {
            input = input.Replace(" ", "");
            input = input.Replace("[", "");
            input = input.Replace("]", "");
            return input;
        }
        #endregion

        #region url parms 转字典

        /// <summary>
        /// url 转字典
        /// </summary>
        /// <param name="urlParms"></param>
        /// <returns></returns>
        public static Dictionary<string, object> UriToDictionary(string urlParms)
        {
            var dict = new Dictionary<string, object>();
            var urlParmsArray = urlParms.Split('&');

            foreach (var parm in urlParmsArray)
            {
                if (parm.Contains("="))
                {
                    var parmArray = parm.Split('=');
                    if (parmArray.Length == 2 && !string.IsNullOrWhiteSpace(parmArray[0]))
                        dict.Add(parmArray[0], HttpUtility.UrlDecode(parmArray[1]));
                }
            }

            return dict;
        }
        #endregion

        #region 获取随机数

        private static readonly char[] constant =
        {
        '0','1','2','3','4','5','6','7','8','9',
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        };

        private static readonly char[] constantInt =
        {
        '0','1','2','3','4','5','6','7','8','9'
        };

        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string RandomCode(int Length, bool onlyInt = true)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);

            var randomConstant = onlyInt ? constantInt : constant;

            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(randomConstant[rd.Next(randomConstant.Length - 1)]);
            }

            return newRandom.ToString();
        }


        /// <summary>  
        /// 该方法是将生成的随机数写入图像文件  
        /// </summary>  
        /// <param name="code">code是一个随机数</param>
        /// <param name="numbers">生成位数（默认4位）</param>  
        //public static MemoryStream CreateValidate(out string code, int numbers = 4)
        //{
        //    code = RandomCode(numbers);
        //    Bitmap Img = null;
        //    Graphics g = null;
        //    MemoryStream ms = null;
        //    Random random = new Random();
        //    //验证码颜色集合  
        //    Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

        //    //验证码字体集合
        //    string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };


        //    //定义图像的大小，生成图像的实例  
        //    Img = new Bitmap((int)code.Length * 18, 32);

        //    g = Graphics.FromImage(Img);//从Img对象生成新的Graphics对象    

        //    g.Clear(Color.White);//背景设为白色  

        //    //在随机位置画背景点  
        //    for (int i = 0; i < 100; i++)
        //    {
        //        int x = random.Next(Img.Width);
        //        int y = random.Next(Img.Height);
        //        g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
        //    }
        //    //验证码绘制在g中  
        //    for (int i = 0; i < code.Length; i++)
        //    {
        //        int cindex = random.Next(7);//随机颜色索引值  
        //        int findex = random.Next(5);//随机字体索引值  
        //        Font f = new Font(fonts[findex], 15, FontStyle.Bold);//字体  
        //        Brush b = new SolidBrush(c[cindex]);//颜色  
        //        int ii = 4;
        //        if ((i + 1) % 2 == 0)//控制验证码不在同一高度  
        //        {
        //            ii = 2;
        //        }
        //        g.DrawString(code.Substring(i, 1), f, b, 3 + (i * 12), ii);//绘制一个验证字符  
        //    }
        //    ms = new MemoryStream();//生成内存流对象  
        //    Img.Save(ms, ImageFormat.Jpeg);//将此图像以Png图像文件的格式保存到流中  

        //    //回收资源  
        //    g.Dispose();
        //    Img.Dispose();
        //    return ms;
        //}
        #endregion

        #region Model对象转换为uri网址参数形式

        /// <summary>
        /// Model对象转换为uri网址参数形式
        /// </summary>
        /// <param name="obj">Model对象</param>
        /// <param name="url">前部分网址</param>
        /// <returns></returns>
        public static string ModelToUriParam(object obj, string url = "")
        {
            PropertyInfo[] propertis = obj.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Append(url);
            sb.Append("?");
            foreach (var p in propertis)
            {
                var v = p.GetValue(obj, null);
                if (v == null)
                    continue;

                sb.Append(p.Name);
                sb.Append("=");
                sb.Append(HttpUtility.UrlEncode(v.ToString()));
                sb.Append("&");
            }
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }
        #endregion

        #region 获取最大识别码

        /// <summary>
        /// 获取最大识别码
        /// </summary>
        /// <param name="maxNo"></param>
        /// <param name="noLength"></param>
        /// <returns></returns>
        public static string MaxNo(ref string maxNo, int noLength = 5)
        {
            var maxNoInt = 0;
            if (maxNo != null && maxNo.Length >= 13)
            {
                var ztNo = maxNo.Substring(noLength - 4, 4);
                var upperNo = maxNo.Substring(0, noLength - 4);

                int.TryParse(ztNo, out maxNoInt);

                maxNo = (maxNoInt + 1).ToString($"D{4}");

                var retMaxNo = upperNo + maxNo;

                maxNo = retMaxNo;
            }
            else
            {
                int.TryParse(maxNo, out maxNoInt);
                maxNo = (maxNoInt + 1).ToString($"D{noLength}");
            }

            return maxNo;
        }

        /// <summary>
        /// 获取后几位数
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="num">返回的具体位数</param>
        /// <returns>返回结果的字符串</returns>
        public static string GetLastStr(string str, int num)
        {
            int count = 0;
            if (str.Length > num)
            {
                count = str.Length - num;
                str = str.Substring(count, num);
            }
            return str;
        }
        /// <summary>
        /// 获取识别码
        /// </summary>
        /// <returns></returns>
        public static string MaxNo(string maxNo, int noLength = 5)
        {
            var maxNoInt = 0;
            int.TryParse(maxNo, out maxNoInt);

            return MaxNo(maxNoInt, noLength: noLength);
        }

        /// <summary>
        /// 获取识别码
        /// </summary>
        /// <returns></returns>
        public static string MaxNo(int maxNo, int noLength = 5)
        {
            var maxNoFormat = maxNo.ToString($"D{noLength}");
            return maxNoFormat;
        }
        #endregion

        #region 获取DateTime 的相差月份
        /// <summary>
        /// 获取DateTime 的相差月份
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static int GetTimeSpanMonth(DateTime after, DateTime before)
        {
            return ((after.Year - before.Year) * 12) + after.Month - before.Month + 1;
        }

        #endregion

        #region 将英文双引号转换成中文
        /// <summary>
        /// 将英文双引号转换成中文
        /// </summary>
        /// <param name="pStr"></param>
        /// <returns></returns>
        public static string StrReplace(string pStr)
        {
            if (string.IsNullOrEmpty(pStr))
                return "";
            //把字符串按照双引号截成数组
            string[] str = pStr.Split('\"');
            //替换后的字符串
            string Newstr = "";
            for (int i = 1; i <= str.Length; i++)
            {
                if (i % 2 == 0)
                    Newstr += str[i - 1] + "”";
                else
                    Newstr += str[i - 1] + "“";
            }
            return Newstr.Substring(0, Newstr.Length - 1);

        }
        #endregion

    }
}
