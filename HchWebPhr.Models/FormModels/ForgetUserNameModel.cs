using HchWebPhr.CaptchaLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.FormModels
{
    public class ForgetUserNameModel
    {
        [Display(Name = "電子郵件")]
        [Required(ErrorMessage = "必填欄位！")]
        [EmailAddress(ErrorMessage = "請輸入符合格式的電子郵件地址！")]
        public string EMail { get; set; }

        [Display(Name = "出生年月日")]
        [Required(ErrorMessage = "必填欄位！")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "出生年月日")]
        [Required(ErrorMessage = "必填欄位")]
        public string TwBirthDate
        {
            get
            {
                return string.Format("{0:000}/{1: MM/dd}", BirthDate.Year - 1911, BirthDate);
            }
        }

        [Display(Name = "身分證號")]
        [Required(ErrorMessage = "必填欄位！")]
        public string IdNo { get; set; }

        [Required(ErrorMessage = "必填欄位!")]
        [Display(Name = "驗證碼")]
        [ValidateCaptcha(ErrorMessage = "驗證碼輸入錯誤!")]
        public string ValidCode { get; set; }

    }
}
