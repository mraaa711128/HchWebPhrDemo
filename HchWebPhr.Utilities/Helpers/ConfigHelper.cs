using System;
using System.Configuration;
using System.Web;

namespace HchWebPhr.Utilities.Helper
{
    public class ConfigHelper
    {
        #region 取得 Config 資料
        /// <summary>
        /// 取得 Config 資料
        /// </summary>
        /// <returns></returns>
        public static string Get(string ConfigKey)
        {
            var config = ConfigurationManager.AppSettings.Get(ConfigKey);
            if (config != string.Empty)
            {
                return HttpUtility.HtmlDecode(config);
            }

            return string.Empty;
        }

        /// <summary>
        /// 取得 Config 資料(數值)
        /// </summary>
        /// <param name="ConfigKey"></param>
        /// <returns></returns>
        public static int GetInteger(string ConfigKey, int output = 0)
        {
            var value = Get(ConfigKey);

            var isNumeric = int.TryParse(value, out output);

            return output;
        }

        /// <summary>
        /// 取得 Config 資料(布林值)
        /// </summary>
        /// <param name="ConfigKey"></param>
        /// <returns></returns>
        public static bool GetBoolean(string ConfigKey, bool output = false)
        {
            var value = Get(ConfigKey);

            if (!string.IsNullOrEmpty(value))
            {
                return Convert.ToBoolean(value);
            }
            else
            {
                return output;
            }
        }

        /// <summary>
        /// 取得 Config 資料(路徑)
        /// </summary>
        /// <param name="ConfigKey"></param>
        /// <returns></returns>
        public static string GetPath(string ConfigKey)
        {
            var value = Get(ConfigKey);

            if (!string.IsNullOrEmpty(value))
            {
                return VirtualPathUtility.ToAbsolute(value);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 取得 Config 資料(加密)
        /// </summary>
        /// <param name="ConfigKey"></param>
        /// <returns></returns>
        public static string GetEncrypt(string ConfigKey)
        {
            var value = Get(ConfigKey);

            if (!string.IsNullOrEmpty(value))
            {
                return Encrypt.desDecryptBase64(value);
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
