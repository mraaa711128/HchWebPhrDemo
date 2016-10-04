using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    public class ConditionTermInfo
    {
        public string Id { get; set; }
        public DateTime EffectiveDateTime { get; set; }
        public string TwEffectiveDateTime
        {
            get
            {
                var chtDate = string.Format("{0:D3}/{1:MM/dd}", EffectiveDateTime.Year - 1911, EffectiveDateTime);
                var Time = string.Format("{0:HH:mm:ss}", EffectiveDateTime);
                return chtDate + " " + Time;
            }
        }
        public string TermContent { get; set; }
        public DateTime LastModifyDateTime { get; set; }
        public string TwLastModifyDateTime {
            get
            {
                var chtDate = string.Format("{0:D3}/{1:MM/dd}", LastModifyDateTime.Year - 1911, LastModifyDateTime);
                var Time = string.Format("{0:HH:mm:ss}", LastModifyDateTime);
                return chtDate + " " + Time;
            }
        }

    }
}
