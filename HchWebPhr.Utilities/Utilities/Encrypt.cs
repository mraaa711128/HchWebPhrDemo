using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HchWebPhr.Utilities
{
    public static class Encrypt
    {
        private static byte[] key = { 0xCF, 0xF3, 0x33, 0xBA, 0x11, 0xBE, 0x7C, 0xA9 };
        private static byte[] iv = { 0x2E, 0xA2, 0xDC, 0xCB, 0x82, 0x87, 0x0A, 0x2F };

        private static byte[] salt = new byte[] { 0x0A, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0xF1 };

        #region 加密字串
        /// <summary>
        /// 加密字串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string desEncryptBase64(this string source)
        {
            var des = new DESCryptoServiceProvider();
            des.Key = new Rfc2898DeriveBytes(key, salt, 8).GetBytes(8);
            des.IV = new Rfc2898DeriveBytes(iv, salt, 8).GetBytes(8);

            var dataByteArray = Encoding.UTF8.GetBytes(source);

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
        #endregion

        #region 解密字串
        /// <summary>
        /// 解密字串
        /// </summary>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        public static string desDecryptBase64(this string encrypt)
        {
            var des = new DESCryptoServiceProvider();
            des.Key = new Rfc2898DeriveBytes(key, salt, 8).GetBytes(8);
            des.IV = new Rfc2898DeriveBytes(iv, salt, 8).GetBytes(8);

            var dataByteArray = Convert.FromBase64String(encrypt);

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
        #endregion

        #region URL Safe 加密字串
        /// <summary>
        /// URL Safe 加密字串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string desEncryptUrlSafeBase64(this string source)
        {
            var des = new DESCryptoServiceProvider();
            des.Key = new Rfc2898DeriveBytes(key, salt, 8).GetBytes(8);
            des.IV = new Rfc2898DeriveBytes(iv, salt, 8).GetBytes(8);

            var dataByteArray = Encoding.UTF8.GetBytes(source);

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    return HttpServerUtility.UrlTokenEncode((ms.ToArray()));
                }
            }
        }
        #endregion

        #region URL Safe 解密字串
        /// <summary>
        /// URL Safe 解密字串
        /// </summary>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        public static string desDecryptUrlSafeBase64(this string encrypt)
        {
            var des = new DESCryptoServiceProvider();
            des.Key = new Rfc2898DeriveBytes(key, salt, 8).GetBytes(8);
            des.IV = new Rfc2898DeriveBytes(iv, salt, 8).GetBytes(8);

            var dataByteArray = HttpServerUtility.UrlTokenDecode(encrypt);

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
        #endregion

        #region 密碼加密
        /// <summary>
        /// 密碼加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Password(this string source)
        {
            var sourceData = Encoding.UTF8.GetBytes(source);
            var md5 = MD5.Create();
            var crypto = md5.ComputeHash(sourceData);
            return Convert.ToBase64String(crypto);
        }
        #endregion
    }
}