using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Commons.XHelper
{
    /// <summary>
    /// 汉字转换拼音类
    /// </summary> 
    public static class SpellHelper
    {
            /// <summary>
            /// 获取全拼
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static string GetFull(string str)
            {
                string PYstr = "";
                foreach (char item in str.ToCharArray())
                {
                    if (Microsoft.International.Converters.PinYinConverter.ChineseChar.IsValidChar(item))
                    {
                        Microsoft.International.Converters.PinYinConverter.ChineseChar cc = new Microsoft.International.Converters.PinYinConverter.ChineseChar(item);
                        PYstr += cc.Pinyins[0].Substring(0, cc.Pinyins[0].Length - 1);
                    }
                    else
                    {
                        PYstr += item.ToString();
                    }
                }
                return PYstr;
            }

            /// <summary>
            /// 获取首字符
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static string GetFrist(string str)
            {
                string PYstr = "";
                foreach (char item in str.ToCharArray())
                {
                    if (Microsoft.International.Converters.PinYinConverter.ChineseChar.IsValidChar(item))
                    {
                        Microsoft.International.Converters.PinYinConverter.ChineseChar cc = new Microsoft.International.Converters.PinYinConverter.ChineseChar(item);
                        PYstr += cc.Pinyins[0][0];
                    }
                    else
                    {
                        PYstr += item.ToString()[0];
                    }
                }
                return PYstr;
            }

        }

}
