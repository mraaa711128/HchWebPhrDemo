using HchWebPhr.Utilities.Helper;
using System.Web.Mvc;

namespace HchWebPhr.Utilities.Filters
{
    public class CustomOutputCacheAttribute : OutputCacheAttribute
    {
        public CustomOutputCacheAttribute()
        {
            this.Duration = ConfigHelper.GetInteger("CACHE_DURATION_SECONDS", 600);
        }
    }
}