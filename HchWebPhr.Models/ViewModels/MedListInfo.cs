using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    public class MedListInfo
    {
        public DateTime ClinicDate { get; set; }
        public string JsonClinicDate
        {
            get
            {
                return this.ClinicDate.ToString("yyyy/MM/dd");
            }
        }
        public string TwClinicDate
        {
            get
            {
                return string.Format("{0:D3}/{1:MM/dd}", this.ClinicDate.Year - 1911, this.ClinicDate);
            }
        }

        public string DivNo { get; set; }
        public string DivName { get; set; }
        public string Id
        {
            get
            {
                return ClinicDate.ToString("yyyyMMdd") + "_" + DivNo;
            }
        }
    }
}
