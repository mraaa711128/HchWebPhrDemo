using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.FormModels
{
    public class SearchMedModel
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

        [Display(Name = "看診科別")]
        public string DivNo { get; set; }
    }
}
