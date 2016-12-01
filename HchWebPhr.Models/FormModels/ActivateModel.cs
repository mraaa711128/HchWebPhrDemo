using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.FormModels
{
    public class ActivateModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "必填欄位!")]
        [StringLength(60, ErrorMessage = "輸入限制：60 碼英數字、底線、破折號、小數點或小老鼠。")]
        [RegularExpression(@"(?:\d|[a-z]|[A-Z]|[-_.@])+", ErrorMessage = "輸入限制：60 碼英數字、底線、破折號、小數點或小老鼠。")]
        [Display(Name = "使用者帳號")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "必填欄位。")]
        [StringLength(30, ErrorMessage = "輸入限制：6 ~ 30 碼英數字且其中需包含英文及數字。")]
        [RegularExpression(@"^(?=.+([a-z]|[A-Z]|\d)).{6,30}$", ErrorMessage = "輸入限制：6 ~ 30 碼英數字且其中需包含英文及數字。")]
        [Display(Name = "登入密碼")]
        public string Password { get; set; }
        [Required(ErrorMessage = "必填欄位")]
        [Compare("Password", ErrorMessage = "必須輸入相同的密碼！")]
        [Display(Name = "重複輸入密碼")]
        public string RePassword { get; set; }
        [Display(Name = "電子郵件")]
        public string Email { get; set; }
        [Display(Name = "真實姓名")]
        public string Name { get; set; }
        [Display(Name = "身分證號")]
        public string IdNo { get; set; }
        [Display(Name = "出生年月日")]
        public DateTime BirthDate { get; set; }
    }
}
