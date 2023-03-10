using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UtilitiesLib.Validators
{
    public static class TaiwanIDValidator
    {
        public static bool validTaiwanId(string ID)
        {
            Dictionary<string, int> map = new Dictionary<string, int> {
                { "8", 8 },
                { "9", 9 },
                { "A", 10 },
                { "B", 11 },
                { "C", 12 },
                { "D", 13 },
                { "E", 14 },
                { "F", 15 },
                { "G", 16 },
                { "H", 17 },
                { "I", 34 },
                { "J", 18 },
                { "K", 19 },
                { "L", 20 },
                { "M", 21 },
                { "N", 22 },
                { "O", 35 },
                { "P", 23 },
                { "Q", 24 },
                { "R", 25 },
                { "S", 26 },
                { "T", 27 },
                { "U", 28 },
                { "V", 29 },
                { "W", 32 },
                { "X", 30 },
                { "Y", 31 },
                { "Z", 33 },
            };
            int[] multiplier = new int[] {1,9,8,7,6,5,4,3,2,1 };
            Regex regexpLocal = new Regex("^[A-Z](1|2)[0-9]{8}");
            Regex regexpForiegn = new Regex("^[A-Z](A|B|C|D|8|9)[0-9]{8}");
            int[] idNums = new int[10];
            if (regexpLocal.IsMatch(ID) == true)
            {
                var firstCharNum = map.FirstOrDefault(x => x.Key.Equals(ID.Substring(0, 1))).Value;
                idNums[0] = firstCharNum / 10;
                idNums[1] = firstCharNum % 10;
                for (var i = 1; i < ID.Length - 1; i++) {
                    idNums[i+1] = int.Parse(ID.Substring(i,1));
                }
            } else if (regexpForiegn.IsMatch(ID) == true)
            {
                var firstCharNum = map.FirstOrDefault(x => x.Key.Equals(ID.Substring(0, 1))).Value;
                var secondCharNum = map.FirstOrDefault(x => x.Key.Equals(ID.Substring(1, 1))).Value;
                idNums[0] = firstCharNum / 10;
                idNums[1] = firstCharNum % 10;
                idNums[2] = secondCharNum % 10;
                for (var i = 2; i < ID.Length - 1; i++)
                {
                    idNums[i + 1] = int.Parse(ID.Substring(i, 1));
                }
            }
            else
            {
                return false;
            }
            var totlaNum = 0;
            for (var i = 0; i < idNums.Length; i++)
            {
                totlaNum += idNums[i] * multiplier[i];
            }
            return ((totlaNum + int.Parse(ID.Substring(ID.Length - 1, 1))) % 10 == 0);
        }
    }
}
