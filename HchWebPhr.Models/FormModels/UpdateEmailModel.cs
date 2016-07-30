using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.FormModels
{
    public class UpdateEmailModel
    {
        [Display(Name = "原電子郵件")]
        [EmailAddress(ErrorMessage = "請輸入正確的電子郵件格式！")]
        public string OriginalEmail { get; set; }

        [Display(Name = "新電子郵件")]
        [EmailAddress(ErrorMessage = "請輸入正確的電子郵件格式！")]
        public string NewEmail { get; set; }

        [Display(Name = "確認新電子郵件")]
        [EmailAddress(ErrorMessage = "請輸入正確的電子郵件格式！")]
        [Compare("NewEmail", ErrorMessage = "請重複輸入新電子郵件")]
        public string ConfirmNewEmail { get; set; }

    }
}
