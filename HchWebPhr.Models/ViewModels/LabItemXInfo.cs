using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    [JsonObject()]
    public class LabItemXInfo
    {
        public string LabCode { get; set; }
        public string LabName { get; set; }
        public string Result { get; set; }
        public string Unit { get; set; }
        public bool IsParent { get; set; }
        public bool NeedCompare { get; set; }
        public string Reference { get; set; }
        public string LowerLimit { get; set; }
        public string UpperLimit { get; set; }
        public string RiskLowerLimit { get; set; }
        public string RiskUpperLimit { get; set; }
        public IList<LabItemXInfo> LabSubItems { get; set; }
    }
}
