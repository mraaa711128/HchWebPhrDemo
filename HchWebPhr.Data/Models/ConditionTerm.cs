using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Data.Models
{
    public class ConditionTerm
    {
        public int ConditionTermId { get; set; }
        public DateTime EffectiveDateTime { get; set; }
        public string TermContent { get; set; }
        public DateTime LastModifyDateTime { get; set; }
    }
}
