using System;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;

namespace HchWebPhr.Utilities.Helpers
{
    public class CacheHelper
    {
        #region 取得 Cache 資料
        /// <summary>
        /// 取得 Cache 資料
        /// </summary>
        /// <returns></returns>
        public static object GetCache(string CacheId)
        {
            object _Cache = HttpRuntime.Cache.Get(CacheId);

            // 判斷 Cache 是否啟用
            if (WebConfigurationManager.AppSettings["ENABLE_CACHE"] == null || !Convert.ToBoolean(WebConfigurationManager.AppSettings["ENABLE_CACHE"]))
            {
                _Cache = null;
                HttpRuntime.Cache.Remove(CacheId);
            }

            return _Cache;
        }
        #endregion

        #region 寫入 Cache 資料
        /// <summary>
        /// 寫入 Cache 資料 ( 預設 60 秒 或 由 web.config 指定 )
        /// </summary>
        public static void SetCache(string CacheId, object _Cache)
        {
            if (WebConfigurationManager.AppSettings["CACHE_DURATION_SECONDS"] != null)
            {
                SetCache(CacheId, _Cache, Convert.ToInt32(WebConfigurationManager.AppSettings["CACHE_DURATION_SECONDS"]));
            }
            else
            {
                SetCache(CacheId, _Cache, 60);
            }
        }

        /// <summary>
        /// 寫入 Cache 資料 ( 自行指定秒數 )
        /// </summary>
        public static void SetCache(string CacheId, object _Cache, int cacheDurationSeconds)
        {
            if (_Cache != null)
            {
                System.Web.HttpRuntime.Cache.Insert(
                    CacheId,
                    _Cache,
                    null,
                    Cache.NoAbsoluteExpiration,
                    new TimeSpan(0, 0, cacheDurationSeconds),
                    System.Web.Caching.CacheItemPriority.High,
                    null);
            } else
            {
                HttpRuntime.Cache.Remove(CacheId);
            }
        }
        #endregion
    }
}