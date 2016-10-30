using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    public class MedItemInfo
    {
        public string MedCode { get; set; }
        public string EnglishName { get; set; }
        public string ChineseName { get; set; }
        public string MedName { get; set; }
        public string MethodCode { get; set; }
        public string MethodName { get; set; }
        public string FreqCode { get; set; }
        public string FreqName { get; set; }
        public string DoseQty { get; set; }
        public string DoseUnit { get; set; }
        public string AppearanceCode { get; set; }
        public string AppearanceName { get; set; }
        public string IndicationCode { get; set; }
        public string IndicationName { get; set; }
        public string SideEffectCode { get; set; }
        public string SideEffectName { get; set; }
    }
}
