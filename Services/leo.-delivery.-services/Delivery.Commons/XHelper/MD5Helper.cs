using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Commons.XHelper
{
    public class MD5Helper
    {
        /// <summary>
        /// 取得输入字符串的MD5哈希值
        /// </summary>
        /// <param name="argInput">输入字符串</param>
        /// <returns>MD5哈希值</returns>
        public static string MD5String(string ConvertString)
        {
            if (string.IsNullOrWhiteSpace(ConvertString))
                return string.Empty;
            string cl = ConvertString;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }

        /// <summary>
        /// 取得输入字符串的MD5哈希值
        /// （因移动检查手持机格式问题，重写方法，仅限移动检查调用）
        /// </summary>
        /// <param name="argInput">输入字符串</param>
        /// <returns>MD5哈希值</returns>
        public static string MD5String_Check(string ConvertString)
        {
            //具体代码如下(写在按钮的Click事件里):
            byte[] result = Encoding.Default.GetBytes(ConvertString);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            ConvertString = BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文本的文本框
            return ConvertString;
        }

    }
}
