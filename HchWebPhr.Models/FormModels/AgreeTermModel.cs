using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.FormModels
{
    public class AgreeTermModel
    {
        [Required(ErrorMessage = "必填欄位")]
        [Display(Name = "授權同意內容")]
        public string ConditionTerm { get; set; }
        [Required(ErrorMessage = "必填欄位")]
        [Display(Name = "我同意")]
        public bool Agree { get; set; }
    }
}
