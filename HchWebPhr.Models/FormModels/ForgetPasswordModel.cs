using HchWebPhr.CaptchaLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.FormModels
{
    public class ForgetPasswordModel
    {
        [Required(ErrorMessage = "必填欄位!")]
        [Display(Name = "使用者帳號")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "必填欄位!")]
        [EmailAddress(ErrorMessage = "請輸入符合格式的EMail地址")]
        [Display(Name = "電子郵件")]
        public string EMail { get; set; }
        [Required(ErrorMessage = "必填欄位。")]
        [ValidateCaptcha(ErrorMessage = "認證碼輸入錯誤！")]
        [Display(Name = "認證碼")]
        public string ValidCode { get; set; }
    }
}
