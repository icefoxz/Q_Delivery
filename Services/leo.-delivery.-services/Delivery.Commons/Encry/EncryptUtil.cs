using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Delivery.Commons.Encry
{
    /// <summary>
    /// 加密解密工具类
    /// </summary>
    public static class EncryptUtil
    {
        #region Fields

        private static readonly byte[] _DESKeyBytes = { 0xa1, 0xd2, 0xf3, 0xb4, 0xc5, 0xe6, 0x4a, 0x02 };
        private static readonly byte[] _DESIVBytes = { 0xf3, 0x91, 0xd6, 0xff, 0x32, 0x1f, 0x4c, 0x12 };
        private static readonly byte[] _AESKeyBytes = { 0xf3, 0x91, 0xd6, 0xff, 0x32, 0x1f, 0x4a, 0x02, 0xe4, 0x88, 0x25, 0x90, 0x72, 0xb3, 0xa7, 0x11 };
        private static readonly byte[] _AESIVBytes = { 0x15, 0x33, 0x21, 0x9f, 0xb6, 0xdc, 0xaf, 0xd8, 0xd4, 0x37, 0x5f, 0x95, 0x13, 0xe4, 0x72, 0xdd };
        private const int SALT_BYTES_LENGTH = 16;   // 盐值长度

        #endregion

        #region DES 加密/解密

        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DESEncrypt(string input)
        {
            return HttpUtility.UrlEncode(DESEncrypt(input, _DESKeyBytes, _DESIVBytes));
        }

        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DESEncrypt(string input, byte[] key, byte[] iv)
        {
            DESCryptoServiceProvider csp = new DESCryptoServiceProvider();
            using (ICryptoTransform ct = csp.CreateEncryptor(key, iv))
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                cs.Write(bytes, 0, bytes.Length);
                cs.Write(GetSaltBytes(), 0, SALT_BYTES_LENGTH); // 增加随即生成的盐值，保证每次加密的结果不同
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// DES 解密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DESDecrypt(string input)
        {
            input = HttpUtility.UrlDecode(input);
            return DESDecrypt(input, _DESKeyBytes, _DESIVBytes);
        }

        /// <summary>
        /// DES 解密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DESDecrypt(string input, byte[] key, byte[] iv)
        {
            try
            {
                DESCryptoServiceProvider csp = new DESCryptoServiceProvider();
                using (ICryptoTransform ct = csp.CreateDecryptor(key, iv))
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                {
                    byte[] bytes = Convert.FromBase64String(input);
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();

                    // 去除盐值部分
                    bytes = ms.ToArray();
                    byte[] result = new byte[bytes.Length - SALT_BYTES_LENGTH];
                    Buffer.BlockCopy(bytes, 0, result, 0, result.Length);

                    return Encoding.UTF8.GetString(result);
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }


        /// <summary>
        ///3DES加密
        /// </summary>
        /// <param name="originalValue">加密数据</param>
        /// <param name="key">24位字符的密钥字符串</param>
        /// <param name="IV">8位字符的初始化向量字符串</param>
        /// <returns></returns>
        public static string DESEncrypt(string originalValue, string key)
        {
            #region CBC模式代码 需要IV
            //SymmetricAlgorithm sa;
            //ICryptoTransform ct;
            //MemoryStream ms;
            //CryptoStream cs;
            //byte[] byt;
            //sa = new TripleDESCryptoServiceProvider();
            //sa.Key = Encoding.UTF8.GetBytes(key);
            //sa.IV = Encoding.UTF8.GetBytes(IV);
            //ct = sa.CreateEncryptor();
            //byt = Encoding.UTF8.GetBytes(originalValue);
            //ms = new MemoryStream();
            //cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            //cs.Write(byt, 0, byt.Length);
            //cs.FlushFinalBlock();
            //cs.Close();
            //return Convert.ToBase64String(ms.ToArray());
            #endregion

            #region ECB模式代码1，IV可有可无
            //MemoryStream mStream = new MemoryStream();
            //TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            //tdsp.Mode = CipherMode.ECB;
            //tdsp.Padding = PaddingMode.PKCS7;

            //byte[] iv = Encoding.UTF8.GetBytes(IV);
            //byte[] keys = Encoding.UTF8.GetBytes(key);
            //byte[] data = Encoding.UTF8.GetBytes(originalValue);

            //CryptoStream cStream = new CryptoStream(mStream, tdsp.CreateEncryptor(keys, iv), CryptoStreamMode.Write);
            //cStream.Write(data, 0, data.Length);
            //cStream.FlushFinalBlock();
            //byte[] ret = mStream.ToArray();
            //// Close the streams.  
            //cStream.Close();
            //mStream.Close();
            //// Return the encrypted buffer.  
            //return Convert.ToBase64String(ret.ToArray());
            #endregion

            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            DES.Key = Encoding.Default.GetBytes(key);
            DES.Mode = CipherMode.ECB;
            DES.Padding = PaddingMode.PKCS7;

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();

            byte[] Buffer = UTF8Encoding.UTF8.GetBytes(originalValue);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="data">解密数据</param>
        /// <param name="key">24位字符的密钥字符串(需要和加密时相同)</param>
        /// <param name="iv">8位字符的初始化向量字符串(需要和加密时相同)</param>
        /// <returns></returns>
        public static string DESDecrypst(string data, string key)
        {
            #region CBC模式代码
            //SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
            //mCSP.Key = Encoding.UTF8.GetBytes(key);
            //mCSP.IV = Encoding.UTF8.GetBytes(IV);
            //ICryptoTransform ct;
            //MemoryStream ms;
            //CryptoStream cs;
            //byte[] byt;
            //ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);
            //byt = Convert.FromBase64String(data);
            //ms = new MemoryStream();
            //cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            //cs.Write(byt, 0, byt.Length);
            //cs.FlushFinalBlock();
            //cs.Close();
            //return Encoding.UTF8.GetString(ms.ToArray());
            #endregion

            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            DES.Key = Encoding.Default.GetBytes(key);
            DES.Mode = CipherMode.ECB;
            ICryptoTransform DESDecrypt = DES.CreateDecryptor();
            string result = "";
            try
            {
                byte[] Buffer = Convert.FromBase64String(data);
                result = UTF8Encoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (System.Exception e)
            {
                result = String.Empty;
            }
            return result;
        }
        #endregion

        #region MD5

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">待加密字串</param>
        /// <returns>加密后的字串</returns>
        public static string MD5Encrypt(string input)
        {
            var firstMD5 = MD5Encrypt(input, 0);
            var saltValue = "Delivery";
            if (input.Length >= 5)
                saltValue = input.Substring(0, 5);

            var secondMD5 = MD5Encrypt($"{firstMD5}{saltValue}", 32);
            return secondMD5;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">待加密字串</param>
        /// <param name="length">16或32值之一，其它则采用.net默认MD5加密算法</param>
        /// <returns>加密后的字串</returns>
        public static string MD5Encrypt(string input, int length)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(input);
            byte[] hashValue = MD5.Create().ComputeHash(buffer);

            string result;
            switch (length)
            {
                case 16:
                    result = BitConverter.ToString(hashValue, 4, 8).Replace("-", "");
                    break;
                case 32:
                    result = BitConverter.ToString(hashValue, 0, 16).Replace("-", "");
                    break;
                default:
                    result = BitConverter.ToString(hashValue).Replace("-", "");
                    break;
            }

            return result;
        }


        /// <summary>
        /// 获取MD5值（小写形式）
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string MD5_Lower(string input, Encoding encoding)
        {
            //input = input.Replace("null", "");
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(encoding.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 获取MD5哈希值
        /// </summary>
        /// <param name="file">文件（注意，调用本函数不释放文件）</param>
        /// <param name="format">"x2"结果为32位,"x3"结果为48位,"x4"结果为64位</param>
        /// <returns></returns>
        public static string GetFileMd5(this Stream file, string format = "x2")
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] hash = md5.ComputeHash(file);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString(format));
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMd5Hash fail,error:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取MD5哈希值
        /// </summary>
        /// <param name="file">文件（注意，调用本函数不释放文件）</param>
        /// <param name="format">"x2"结果为32位,"x3"结果为48位,"x4"结果为64位</param>
        /// <returns></returns>
        public static string GetFileMd5(this byte[] file, string format = "x2")
        {
            return new MemoryStream(file).GetFileMd5(format);
        }

        /// <summary>
        /// 获取MD5值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="format">"x2"结果为32位,"x3"结果为48位,"x4"结果为64位</param>
        /// <returns></returns>
        public static string GetMd5(this string input, string format = "x2")
        {
            MD5 md5Hash = MD5.Create();
            // 将输入字符串转换为字节数组并计算哈希数据  
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // 创建一个 Stringbuilder 来收集字节并创建字符串  
            StringBuilder str = new StringBuilder();
            // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串  
            for (int i = 0; i < data.Length; i++)
            {
                str.Append(data[i].ToString(format));//加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位
            }

            return str.ToString();
        }

        #endregion

        #region Common

        private static byte[] GetSaltBytes()
        {
            byte[] bytes = new byte[SALT_BYTES_LENGTH];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);

            return bytes;
        }

        #endregion

        /// <summary>
        ///// 密码加密
        ///// </summary>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //public static string PasswordEncry(string password)
        //{
        //    return ShaUtil.SHA256Encrypt(EncryptUtil.MD5Encrypt(password));
        //}


        #region 密码加密

        // 解密密码  
        public static string DecryptPassword(string encryptedPassword, string secretKey)
        {
            //string secretKey = secretKey; // 用于解密的密钥  
            byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);
            byte[] decryptedBytes = DecryptAES(encryptedBytes, secretKey);
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        // AES 解密方法  
        private static byte[] DecryptAES(byte[] cipherText, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = keyBytes;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.Zeros;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
            return decryptedBytes;
        }


        // 解密方法  
        public static string Decrypt(string cipherText, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = keyBytes;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.Zeros;
                using (ICryptoTransform decryptor = aes.CreateDecryptor()) 
                {
                    byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                    return Encoding.UTF8.GetString(plainBytes);
                }
            }
        }

        #endregion
    }

}
