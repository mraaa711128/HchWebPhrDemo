using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    public class PatientChildrenListInfo
    {
        public IList<PatientChildInfo> VaccineChildrenList { get; set; }
        public IList<PatientChildInfo> LabChildrenList { get; set; }
    }
}
