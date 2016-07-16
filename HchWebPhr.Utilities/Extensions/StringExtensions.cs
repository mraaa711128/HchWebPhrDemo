using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNumeric(this String input)
        {
            float output;
            return float.TryParse(input, out output);
        }

        public static bool IsTaiwanDate(this String input)
        {
            if (input.IsNumeric())
            {
                if (input.Length >= 5 && input.Length <= 7)
                {
                    DateTime output;
                    var chtYear = input.Substring(0, input.Length - 4);
                    var chtMonth = input.Substring(input.Length - 4, 2);
                    var chtDate = input.Substring(chtYear.Length + chtMonth.Length, 2);
                    return DateTime.TryParse(String.Format("{0:0000}/{1:00}/{2:00}",int.Parse(chtYear) + 1911,int.Parse(chtMonth),int.Parse(chtDate)), out output);
                } else
                {
                    return false;
                }
            } else
            {
                return false;
            }
        }

        public static DateTime toDateTime(this String input)
        {
            if (input.IsTaiwanDate())
            {
                var chtYear = input.Substring(0, input.Length - 4);
                var chtMonth = input.Substring(input.Length - 4, 2);
                var chtDate = input.Substring(chtYear.Length + chtMonth.Length, 2);
                return DateTime.Parse(String.Format("{0:0000}/{1:00}/{2:00}", int.Parse(chtYear) + 1911, int.Parse(chtMonth), int.Parse(chtDate)));
            } else
            {
                return new DateTime(1,1,1);
            }
        }
    }
}
