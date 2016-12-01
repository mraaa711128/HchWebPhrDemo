using HchWebPhr.CaptchaLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.FormModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "必填欄位。")]
        [StringLength(60, ErrorMessage = "輸入限制：60 碼英數字、底線、破折號、小數點或小老鼠。")]
        [RegularExpression(@"(?:\d|[a-z]|[A-Z]|[-_.@])+", ErrorMessage = "輸入限制：60 碼英數字、底線、破折號、小數點或小老鼠。")]
        [Display(Name = "使用者帳號")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "必填欄位。")]
        [Display(Name = "密碼")]
        public string Password { get; set; }
        [Required(ErrorMessage = "必填欄位。")]
        [ValidateCaptcha(ErrorMessage = "認證碼輸入錯誤！")]
        [Display(Name = "認證碼")]
        public string ValidCode { get; set; }

    }
}
