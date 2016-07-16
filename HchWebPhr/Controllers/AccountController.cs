using HchWebPhr.Biz;
using HchWebPhr.CaptchaLib;
using HchWebPhr.Controllers.Base;
using HchWebPhr.Data.Repositories;
using HchWebPhr.Models.FormModels;
using HchWebPhr.Models.ViewModels;
using HchWebPhr.Utilities;
using HchWebPhr.Utilities.Filters;
using HchWebPhr.Utilities.Helper;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;

namespace HchWebPhr.Controllers
{
    [Authorize]
    public class AccountController : JsonNetController
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetCaptcha()
        {
            CaptchaImage captcha = new CaptchaImage(5);
            captcha.FontSize = 24;
            captcha.XShift = new Point(-5, 5);
            captcha.YShift = new Point(0, 0);
            captcha.XDistortion = new Point(0, 0);
            captcha.YDistortion = new Point(0, 0);
            captcha.Angle = new Point(-5, 5);
            captcha.NoiseCount = 0;
            return this.Captcha(captcha, 100, 150, 35);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var login = new LoginModel();
            ViewBag.returnUrl = returnUrl;
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid == false)
            {
                ViewBag.ErrorCode = "400";
                ViewBag.ErrorMessage = "Login Fail !";
                return View(login);
            }
            else
            {
                AccountBiz acc = new AccountBiz();
                string encryptPwd = Encrypt.Password(login.Password);
                if (acc.CheckAuthentication(login.UserName, encryptPwd) == false)
                {
                    ViewBag.ErrorCode = acc.ErrorCode;
                    ViewBag.ErrorMessage = acc.ErrorMessage;
                    return View(login);
                }
                else
                {
                    if (acc.LoginUser(login.UserName, IP.GetAddress()) == false)
                    {
                        ViewBag.ErrorCode = acc.ErrorCode;
                        ViewBag.ErrorMessage = acc.ErrorMessage;
                        return View(login);
                    }
                    var loginUser = acc.GetUser(x => x.UserName.Equals(login.UserName));
                    FormAuthHelper.SetLoginUser(loginUser);
                }
                if (String.IsNullOrEmpty(returnUrl) == false)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    return RedirectToAction("Dashboard", "User");
                }
            }
        }

        [HttpGet]
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            SignUpModel mSignUp = new SignUpModel();
            mSignUp.BirthDate = DateTime.Today;
            return View(mSignUp);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpModel mSignUp)
        {
            //SignupModel
            if (ModelState.IsValid == false) { return View(mSignUp); }
            var acc = new AccountBiz();
            IList<SignUpPatient> PtList = null;
            if (acc.CheckPatientIdentity(mSignUp.EMail,mSignUp.BirthDate,out PtList))
            {
                Session.Add("SIGNUP_PATIENT_LIST", PtList);
                return RedirectToAction("SignUpConfirm");
            }
            ViewBag.ErrorCode = acc.ErrorCode;
            ViewBag.ErrorMessage = acc.ErrorMessage;
            return View(mSignUp);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignUpConfirm()
        {
            var PtList = Session["SIGNUP_PATIENT_LIST"];
            if (PtList == null)
            {
                // Create Fail Model !
                var error = new ErrorContext
                {
                    ErrorCode = "500",
                    ErrorMessage = "Session Has Failed ! Please Notify Administrator !"
                };
                return View("Error",error);
            }
            return View(PtList);
        }

        [HttpPost]
        [AjaxOnly]
        [AllowAnonymous]
        public ActionResult SignUpConfirm(SignUpPatient ConfirmPatient)
        {
            string ActivateToken = "";
            var acc = new AccountBiz();
            if (acc.SignUpUser(ConfirmPatient, out ActivateToken) == false)
            {
                var error = new ErrorContext
                {
                    ErrorCode = acc.ErrorCode,
                    ErrorMessage = acc.ErrorMessage
                };
                return View("Error", error);
            }
            if (this.SendActivateMail(ConfirmPatient,ActivateToken) == false)
            {
                var error = new ErrorContext
                {
                    ErrorCode = "900",
                    ErrorMessage = "確認信寄送失敗，請聯絡系統管理員！"
                };
                return View("Error", error);
            }

            return View("Success", new SuccessContext
            {
                SuccessCode = "200",
                SuccessDescription = "確認信已寄至您的電子郵件信箱，請依照電子郵件完成後續步驟!"
            });
        }

        private bool SendActivateMail(SignUpPatient patient, string ActiveToken)
        {
            var mailbody = new SignUpMailContext
            {
                IdNo = patient.IdNo,
                PatientName = patient.PatientName,
                EMail = patient.EMail,
                BirthDate = patient.BirthDate,
                ActiveToken = ActiveToken
            };
            try
            {
                var mailTemp = System.IO.File.ReadAllText(Server.MapPath("~/Views/Account/ActivateMail2.cshtml"));
                DynamicViewBag vbag = new DynamicViewBag(new Dictionary<string, object> {
                    { "ActiveBaseUrl", Url.Action("Activate", "Account", null, Request.Url.Scheme) },
                });
                var htmlMailBody = Razor.Parse<SignUpMailContext>(mailTemp, mailbody,vbag,"");
                SmtpClient mailClient = new SmtpClient();
                MailMessage mailmsg = new MailMessage
                {
                    Subject = "宏其婦幼醫院個人化電子病歷註冊通知",
                    Body = htmlMailBody,
                    IsBodyHtml = true,
                };
                var BetaTest = ConfigHelper.GetBoolean("BETA_TEST");
                if (BetaTest)
                {
                    var DebugMailList = ConfigHelper.Get("BETA_TEST_EMAIL_SENDTO_LIST");
                    if (string.IsNullOrEmpty(DebugMailList) == false)
                    {
                        var DebugSendTos = DebugMailList.Split(';');
                        foreach (var debugSendTo in DebugSendTos)
                        {
                            mailmsg.To.Add(debugSendTo);
                        }
                    }
                    else
                    {
                        mailmsg.To.Add("93013@hch.org.tw");
                        mailmsg.To.Add("mraaa711128@gmail.com");
                    }
                }
                else
                {
                    mailmsg.To.Add(patient.EMail);
                }
                mailClient.Send(mailmsg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Activate(string Token)
        {
            var decryptToken = Encrypt.desDecryptUrlSafeBase64(Token);
            var EMail = decryptToken.Split('+')[0];
            var strSignUpDateTime = decryptToken.Split('+')[1];
            DateTime SignUpDateTime;
            if (DateTime.TryParse(strSignUpDateTime,out SignUpDateTime) == false)
            {
                var error = new ErrorContext
                {
                    ErrorCode = "500",
                    ErrorMessage = "啟動連結錯誤，請聯絡系統管理員。"
                };
                return View("Error", error);
            }
            if (DateTime.Now - SignUpDateTime > new TimeSpan(3,0,0,0))
            {
                var error = new ErrorContext
                {
                    ErrorCode = "501",
                    ErrorMessage = "啟動連結已經失效，請重新註冊！"
                };
                return View("Error", error);
            }
            var resUser = new UserRepository();
            var user = resUser.Get(x => x.Email.Equals(EMail) && x.ActiveToken.Equals(Token));
            if (user == null)
            {
                var error = new ErrorContext
                {
                    ErrorCode = "501",
                    ErrorMessage = "啟動連結錯誤，請重新註冊！"
                };
                return View("Error", error);
            }
            if (user.IsActive)
            {
                return View("Success", new SuccessContext
                {
                    SuccessCode = "200",
                    SuccessDescription = "此帳號已經啟用，請由首頁登入使用。"
                });
            }
            var activateUser = new ActivateModel
            {
                Id = user.UserId,
                UserName = user.UserName,
                Name = user.UserInfo.Name,
                IdNo = user.UserInfo.IdNo,
                Email = EMail,
                BirthDate = user.UserInfo.BirthDate,
            };
            return View(activateUser);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Activate(ActivateModel activateUser)
        {
            if (ModelState.IsValid == false) { return View(activateUser); }
            var acc = new AccountBiz();
            if (acc.CheckUserNameAvailability(activateUser.UserName) == false)
            {
                ModelState.AddModelError("UserName", "使用者名稱已被使用，請重新輸入！");
                return View(activateUser);
            }
            if (acc.ActivateUser(activateUser) == false)
            {
                return View("Error", new ErrorContext
                {
                    ErrorCode = acc.ErrorCode,
                    ErrorMessage = acc.ErrorMessage
                });
            }
            return View("Success", new SuccessContext
            {
                SuccessCode = "200",
                SuccessDescription = "啟用帳號成功！請回到首頁登入帳號！"
            });
        }
        //[HttpGet]
        //public ActionResult Deactivate()
        //{

        //}

        //[HttpPost]
        //public ActionResult Deactivate()
        //{
        //    //DeactivateModel
        //}

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgetPassword()
        {
            var forgetpwd = new ForgetPasswordModel();
            return View(forgetpwd);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(ForgetPasswordModel ForgetPwd)
        {
            if (ModelState.IsValid == false) { return View(ForgetPwd); }
            string ForgetPwdToken = "";
            var acc = new AccountBiz();
            var user = acc.GetUser(x => x.UserName.Equals(ForgetPwd.UserName) && x.Email.Equals(ForgetPwd.EMail));
            if (user == null)
            {
                ViewBag.ErrorCode = "404";
                ViewBag.ErrorMessage = "找不到符合的帳號資料！";
                return View(ForgetPwd);
            }
            if (acc.SetUserForgetPassword(user, out ForgetPwdToken) == false)
            {
                return View("Error", new ErrorContext
                {
                    ErrorCode = acc.ErrorCode,
                    ErrorMessage = acc.ErrorMessage
                });
            }
            if (this.SendForgetPasswordMail(ForgetPwd, ForgetPwdToken) == false)
            {
                return View("Error", new ErrorContext
                {
                    ErrorCode = "900",
                    ErrorMessage = "忘記密碼確認信寄送失敗，請聯絡系統管理員！"
                });
            }
            return View("Success", new SuccessContext
            {
                SuccessCode = "200",
                SuccessDescription = "忘記密碼確認信已寄至您的電子郵件信箱，請依照電子郵件完成後續步驟!"
            });
        }

        private bool SendForgetPasswordMail(ForgetPasswordModel ForgetPwd, string ForgetPwdToken) 
        {
            var mailbody = new ForgetPasswordMailContext
            {
                UserName = ForgetPwd.UserName,
                ForgetPasswordToken = ForgetPwdToken
            };
            try
            {
                var mailTemp = System.IO.File.ReadAllText(Server.MapPath("~/Views/Account/ForgetPasswordMail2.cshtml"));
                DynamicViewBag vbag = new DynamicViewBag(new Dictionary<string, object> {
                    { "ResetPasswordBaseUrl", Url.Action("ResetPassword", "Account", null, Request.Url.Scheme) },
                });
                var htmlMailBody = Razor.Parse<ForgetPasswordMailContext>(mailTemp, mailbody,vbag,"");
                SmtpClient mailClient = new SmtpClient();
                MailMessage mailmsg = new MailMessage
                {
                    Subject = "宏其婦幼醫院個人化電子病歷忘記密碼通知",
                    Body = htmlMailBody,
                    IsBodyHtml = true,
                };
                var BetaTest = ConfigHelper.GetBoolean("BETA_TEST");
                if (BetaTest)
                {
                    var DebugMailList = ConfigHelper.Get("BETA_TEST_EMAIL_SENDTO_LIST");
                    if (string.IsNullOrEmpty(DebugMailList) == false)
                    {
                        var DebugSendTos = DebugMailList.Split(';');
                        foreach (var debugSendTo in DebugSendTos)
                        {
                            mailmsg.To.Add(debugSendTo);
                        }
                    }
                    else
                    {
                        mailmsg.To.Add("93013@hch.org.tw");
                        mailmsg.To.Add("mraaa711128@gmail.com");
                    }
                }
                else
                {
                    mailmsg.To.Add(ForgetPwd.EMail);
                }
                mailClient.Send(mailmsg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string Token)
        {
            var tokenString = Encrypt.desDecryptUrlSafeBase64(Token);
            var TokenArray = tokenString.Split('+');
            var UserName = TokenArray[0];
            var Password = TokenArray[1];
            var stringResetDateTime = TokenArray[2];
            DateTime ResetDateTime;
            if (DateTime.TryParse(stringResetDateTime,out ResetDateTime) == false)
            {
                return View("Error", new ErrorContext
                {
                    ErrorCode = "500",
                    ErrorMessage = "重設密碼連結錯誤，請聯絡系統管理員！"
                });
            }
            if (DateTime.Now - ResetDateTime > new TimeSpan(3,0,0,0))
            {
                return View("Error", new ErrorContext {
                    ErrorCode = "501",
                    ErrorMessage = "重設密碼連結失效，請重新使用忘記密碼！"
                });
            }
            var acc = new AccountBiz();
            var user = acc.GetUser(x => x.UserName.Equals(UserName) && x.Password.Equals(Password));
            if (user == null)
            {
                return View("Error", new ErrorContext {
                    ErrorCode = "500",
                    ErrorMessage = "重設密碼連結錯誤，請聯絡系統管理員！"
                });
            }
            var ResetPwd = new UpdatePasswordModel
            {
                Password = Password
            };
            ViewBag.IsResetPassword = true;
            ViewBag.UserName = UserName;
            return View("UpdatePassword", ResetPwd);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string UserName, UpdatePasswordModel ResetPassword)
        {
            if (ModelState.IsValid == false) {
                ViewBag.IsResetPassword = true;
                ViewBag.UserName = UserName;
                return View(ResetPassword);
            }
            var acc = new AccountBiz();
            if (acc.CheckAuthentication(UserName,ResetPassword.Password) == false)
            {
                return View("Error", new ErrorContext
                {
                    ErrorCode = acc.ErrorCode,
                    ErrorMessage = acc.ErrorMessage
                });
            }
            if (acc.UpdatePassword(UserName, Encrypt.Password(ResetPassword.NewPassword)) == false)
            {
                return View("Error", new ErrorContext
                {
                    ErrorCode = acc.ErrorCode,
                    ErrorMessage = acc.ErrorMessage
                });
            }
            return View("Success", new SuccessContext
            {
                SuccessCode = "200",
                SuccessDescription = "密碼重設成功，請由首頁重新登入！"
            });
        }

        [HttpGet]
        public ActionResult UpdatePassword()
        {
            var UpdatePwd = new UpdatePasswordModel();
            ViewBag.IsResetPassword = false;
            return View(UpdatePwd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePassword(UpdatePasswordModel UpdatePassword)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.IsResetPassword = false;
                return View(UpdatePassword);
            }
            var acc = new AccountBiz();
            if (acc.CheckAuthentication(User.Identity.Name,Encrypt.Password(UpdatePassword.Password)) == false)
            {
                return View("Error", new ErrorContext
                {
                    ErrorCode = acc.ErrorCode,
                    ErrorMessage = acc.ErrorMessage
                });
            }
            if (acc.UpdatePassword(User.Identity.Name, Encrypt.Password(UpdatePassword.NewPassword)) == false)
            {
                return View("Error", new ErrorContext
                {
                    ErrorCode = acc.ErrorCode,
                    ErrorMessage = acc.ErrorMessage
                });
            }
            return RedirectToAction("Logoff", "Account");
        }

        //[HttpGet]
        //public ActionResult UpdateProfile()
        //{

        //}

        //[HttpPost]
        //public ActionResult UpdateProfile()
        //{
        //    //UpdateProfileModel
        //}

#region Private Function
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

#endregion
    }
}