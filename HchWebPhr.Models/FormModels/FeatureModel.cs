using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.FormModels
{
    public class FeatureModel
    {
        public int FeatureId { get; set; }
        [Display(Name = "功能名稱")]
        public string FeatureName { get; set; }
        //[Display(Name = "Controller Name")]
        //[Required(ErrorMessage = "必填欄位")]
        //public string ControllerName { get; set; }
        //[Display(Name = "Action Name")]
        //[Required(ErrorMessage = "必填欄位")]
        //public string ActionName { get; set; }
        //[Display(Name = "顯示備註")]
        //public bool RemarkDisplay { get; set; }
        [Display(Name = "備註說明")]
        public string Remark { get; set; }
        //[Display(Name = "顯示於功能表")]
        //public bool MenuDisplay { get; set; }
        //[Display(Name = "功能表順序")]
        //public int MenuDisplayOrder { get; set; }
    }
}
