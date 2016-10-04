using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.FormModels
{
    public class SearchLabModel
    {
        [Display(Name = "起始日期")]
        public DateTime StartDate { get; set; }
        public string TwStartDate
        {
            get
            {
                return string.Format("{0:000}/{1:MM/dd}", StartDate.Year - 1911, StartDate);
            }
        }

        [Display(Name = "結束日期")]
        public DateTime EndDate { get; set; }
        public string TwEndDate
        {
            get
            {
                return string.Format("{0:000}/{1:MM/dd}", EndDate.Year - 1911, EndDate);
            }
        }
        [Display(Name = "檢驗類型")]
        public string LabType { get; set; }
        [Display(Name = "檢驗群組")]
        public string LabGroupCode { get; set; }
    }
}
