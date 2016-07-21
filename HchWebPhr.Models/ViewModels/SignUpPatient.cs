using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    [Serializable]
    public class SignUpPatient
    {
        public string IdNo { get; set; }
        public string ChartNo { get; set; }
        public string PatientName { get; set; }
        public DateTime BirthDate { get; set; }
        public string JsonBirthDate
        {
            get
            {
                return BirthDate.ToString("yyyy/MM/dd");
            }
        }
        public string TwBirthDate
        {
            get
            {
                return string.Format("{0:000}/{1:MM/dd}", BirthDate.Year - 1911, BirthDate);
            }
        }
        public string BloodType { get; set; }
        public string Sex { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string EMail { get; set; }
    }
}
