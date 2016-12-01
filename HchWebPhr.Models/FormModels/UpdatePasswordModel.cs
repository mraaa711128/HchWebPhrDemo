using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.FormModels
{
    public class UpdatePasswordModel
    {
        [Required(ErrorMessage = "必填欄位。")]
        [StringLength(30, ErrorMessage = "輸入限制：6 ~ 30 碼英數字且其中需包含英文及數字。")]
        [Display(Name = "原始密碼")]
        public string Password { get; set; }
        [Required(ErrorMessage = "必填欄位。")]
        [StringLength(30, ErrorMessage = "輸入限制：6 ~ 30 碼英數字且其中需包含英文及數字。")]
        [RegularExpression(@"^(?=(\d)+([a-z]|[A-Z])+(\d)?|(\d)?([a-z]|[A-Z])+(\d)+).{6,30}$", ErrorMessage = "輸入限制：6 ~ 30 碼英數字且其中需包含英文及數字。。")]
        [Display(Name = "新密碼")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "必填欄位")]
        [Compare("NewPassword", ErrorMessage = "必須輸入相同的密碼！")]
        [Display(Name = "重複輸入新密碼")]
        public string ReNewPassword { get; set; }
    }
}
