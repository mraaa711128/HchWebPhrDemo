using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    public class PatientChildInfo
    {
        public string ChartNo { get; set; }
        public string PtName { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string JsonBirthDate {
            get {
                return this.BirthDate.ToString("yyyy/MM/dd");
            }
        }
        public string TwBirthDate {
            get {
                return string.Format("{0:D3}/{1:MM/dd}", this.BirthDate.Year - 1911, this.BirthDate);
            }
        }
    }
}
