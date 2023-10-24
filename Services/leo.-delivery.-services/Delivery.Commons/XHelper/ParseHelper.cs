using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Commons.XHelper
{
    public static class ParseHelper
    {
        /// <summary>
        /// String转换为Int
        /// </summary>
        /// <param name="def">无效时返回</param>
        /// <param name="str">要转换的String</param>
        /// <returns>Int结果</returns>
        public static int Int_FromString(this string str, int def = 0)
        {
            int i;
            if (int.TryParse(str, out i) == true)
                return i;

            else
                return def > 0 ? def : default;
        }

        /// <summary>
        /// String转换为Long
        /// </summary>
        /// <param name="def">无效时返回</param>
        /// <param name="str">要转换的String</param>
        /// <returns>Long结果</returns>
        public static long Long_FromString(this string str, long def = 0)
        {
            long i;
            if (long.TryParse(str, out i) == true)
                return i;

            else
                return def > 0 ? def : default;
        }

        /// <summary>
        /// String转换为Float
        /// </summary>
        /// <param name="def">无效时返回</param>
        /// <param name="str">要转换的String</param>
        /// <returns>Float结果</returns>
        public static float Float_FromString(this string str, float def = 0)
        {
            float i;
            if (float.TryParse(str, out i) == true)
                return i;

            else
                return def > 0 ? def : default;
        }

        /// <summary>
        /// String转换为Double
        /// </summary>
        /// <param name="def">无效时返回</param>
        /// <param name="str">要转换的String</param>
        /// <returns>Float结果</returns>
        public static double Double_FromString(this string str, double def = 0)
        {
            double i;
            if (double.TryParse(str, out i) == true)
                return i;

            else
                return def > 0 ? def : default;
        }

        /// <summary>
        /// String转换为Decimal
        /// </summary>
        /// <param name="def">无效时返回</param>
        /// <param name="str">要转换的String</param>
        /// <returns>Float结果</returns>
        public static decimal Decimal_FromString(this string str, decimal def = 0)
        {
            decimal i;
            if (decimal.TryParse(str, out i) == true)
                return i;

            else
                return def > 0 ? def : default;
        }

        /// <summary>
        /// String转换为Datetime
        /// </summary>
        /// <param name="def">无效时返回</param>
        /// <param name="str">要转换的String</param>
        /// <returns>Float结果</returns>
        public static DateTime DateTime_FromString(this string str, DateTime def)
        {
            DateTime i;
            if (DateTime.TryParse(str, out i) == true)
                return i;

            else
                return def;
        }

        /// <summary>
        /// 获取本周周一，以周一~周日为基准
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime DateTime_GetMonday(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday)
                return date.AddDays(-6);

            else
                return date.AddDays(1 - Convert.ToInt32(date.DayOfWeek.ToString("d")));
        }
        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime DateTime_FormFormat(this string str)
        {
            string[] formats = { "yyyyMMdd", "yyyy-MM-dd HH:mm:ss", "yyyy/MM/dd", "yyyy/MM/dd HH:mm:ss" };

            DateTime date;
            if (DateTime.TryParseExact(str, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                // 解析第一种格式的日期成功
            }
            else
                date = new DateTime(1770, 01, 01);
            //Console.WriteLine(date);
            return date;
        }

        /// <summary>
        /// Guid
        /// </summary>
        /// <param name="def">无效时返回</param>
        /// <param name="str">要转换的String</param>
        /// <returns>Long结果</returns>
        public static bool Guid_IsEmpty(this Guid? str) => str?.Guid_IsEmpty() ?? true;

        /// <summary>
        /// Guid
        /// </summary>
        /// <param name="def">无效时返回</param>
        /// <param name="str">要转换的String</param>
        /// <returns>Long结果</returns>
        public static bool Guid_IsEmpty(this Guid str) => str == Guid.Empty;

        /// <summary>
        /// 时间戳起始日期
        /// </summary>
        public static DateTime TimestampStart = DateTime.UnixEpoch;//(1970, 1, 1, 0, 0, 0, 0);

        /// <summary>
        /// 转整型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static int ObjToInt(this object thisValue)
        {
            int reval = 0;
            if (thisValue == null) return 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        /// <summary>
        /// 转整型（带错误返回值）
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static int ObjToInt(this object thisValue, int errorValue)
        {
            int reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        /// <summary>
        /// 转双精度类型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static double ObjToMoney(this object thisValue)
        {
            double reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }
        /// <summary>
        /// 转双精度类型（带错误返回值）
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static double ObjToMoney(this object thisValue, double errorValue)
        {
            double reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        /// <summary>
        /// 转字符串型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static string ObjToString(this object thisValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return "";
        }
        /// <summary>
        /// 是否不为空
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool IsNotEmptyOrNull(this object thisValue)
        {
            return ObjToString(thisValue) != "" && ObjToString(thisValue) != "undefined" && ObjToString(thisValue) != "null";
        }
        /// <summary>
        /// 转字符串型（带错误返回值）
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static string ObjToString(this object thisValue, string errorValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return errorValue;
        }
        /// <summary>
        /// 转高精度浮点数
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static Decimal ObjToDecimal(this object thisValue)
        {
            Decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return 0;
        }
        /// <summary>
        /// 转二进制型（带错误返回值）
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static Decimal ObjToDecimal(this object thisValue, decimal errorValue)
        {
            Decimal reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        /// <summary>
        /// 转时间类型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static DateTime ObjToDate(this object thisValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                reval = Convert.ToDateTime(thisValue);
            }
            return reval;
        }
        /// <summary>
        /// 转时间类型（带错误返回值）
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static DateTime ObjToDate(this object thisValue, DateTime errorValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        /// <summary>
        /// 转布尔型
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool ObjToBool(this object thisValue)
        {
            bool reval = false;
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }


        /// <summary>
        /// 获取当前时间的时间戳
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static string DateToTimeStamp(this DateTime thisValue)
        {
            TimeSpan ts = thisValue - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 转换为时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="milliseconds">是否使用毫秒</param>
        /// <returns></returns>
        public static long ObjToTimestamp(this DateTime dateTime, bool milliseconds = false)
        {
            var timestamp = dateTime.ToUniversalTime() - TimestampStart;
            return (long)(milliseconds ? timestamp.TotalMilliseconds : timestamp.TotalSeconds);
        }

        /// <summary>
        /// 转长整型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long ObjToLong(this object thisValue)
        {
            if (thisValue == null || thisValue == DBNull.Value)
                return 0L;

            long.TryParse(thisValue.ToString(), out long result);
            return result;
        }
        #region Json
        public static object ToJson(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject(Json);
        }
        public static string ToJson(this object obj)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }
        public static string ToJson(this object obj, string datetimeformats)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = datetimeformats };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }
        public static T ToObject<T>(this string Json)
        {
            return Json == null ? default(T) : JsonConvert.DeserializeObject<T>(Json);
        }
        public static List<T> ToList<T>(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject<List<T>>(Json);
        }
        public static DataTable ToTable(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject<DataTable>(Json);
        }
        public static JObject ToJObject(this string Json)
        {
            return Json == null ? JObject.Parse("{}") : JObject.Parse(Json.Replace("&nbsp;", ""));
        }
        #endregion

    }
}
