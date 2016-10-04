using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Utilities.Extensions
{
    public static class EnumerableExtension
    {
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) { return true; }
            return (source.Count() <= 0);
        }
    }
}
