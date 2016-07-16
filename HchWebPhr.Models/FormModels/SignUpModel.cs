using HchWebPhr.CaptchaLib;
using System;
using System.ComponentModel.DataAnnotations;

namespace HchWebPhr.Models.FormModels
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "必填欄位!")]
        [EmailAddress(ErrorMessage = "電子郵件格式錯誤!")]
        [Display(Name = "電子郵件")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "必填欄位!")]
        [Display(Name = "出生年月日")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "必填欄位!")]
        [Display(Name = "驗證碼")]
        [ValidateCaptcha(ErrorMessage = "驗證碼輸入錯誤!")]
        public string ValidCode { get; set; }

        //[Display(Name = "出生年月日")]
        public string TwBirthDate
        {
            get
            {
                return string.Format("{0:000}/{1:MM/dd}",BirthDate.Year - 1911 ,BirthDate);
            }
        }
    }
}
