using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.FormModels
{
    public class NewEditTermModel
    {
        public int Id { get; set; }
        [Display(Name = "生效日期")]
        [Required(ErrorMessage = "必填欄位")]
        public DateTime EffectiveDateTime { get; set; }

        public string TwEffectiveDateTime
        {
            get
            {
                return string.Format("{0:D3}/{1:MM/dd}", EffectiveDateTime.Year - 1911, EffectiveDateTime);
            }
        }

        [Display(Name = "同意書內容")]
        [Required(ErrorMessage = "必填欄位")]
        public string ConditionTerm { get; set; }

    }
}
