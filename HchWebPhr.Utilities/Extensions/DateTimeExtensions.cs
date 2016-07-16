using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        public static String toTaiwanDate(this DateTime dt, string delimiter = "/", bool clearDelimiter = false)
        {
            String CHDate = String.Format("{0:000}/{1:MM/dd}", dt.Year - 1911,dt);
            //String CHMMDD = String.Format("{0:MMdd}", dt);
            if (delimiter != "/")
            {
                CHDate = CHDate.Replace("/", delimiter);
            }
            if (clearDelimiter)
            {
                CHDate = CHDate.Replace(delimiter, "");
            }
            return CHDate;
        }

        public static String ChtDayOfWeek(this DateTime dt)
        {
            String rtn = "";

            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    rtn = "日";
                    break;
                case DayOfWeek.Monday:
                    rtn = "一";
                    break;
                case DayOfWeek.Tuesday:
                    rtn = "二";
                    break;
                case DayOfWeek.Wednesday:
                    rtn = "三";
                    break;
                case DayOfWeek.Thursday:
                    rtn = "四";
                    break;
                case DayOfWeek.Friday:
                    rtn = "五";
                    break;
                case DayOfWeek.Saturday:
                    rtn = "六";
                    break;
                default:
                    break;
            }

            return rtn;
        }
    }
}
