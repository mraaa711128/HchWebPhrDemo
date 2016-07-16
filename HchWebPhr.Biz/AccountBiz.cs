using HchWebPhr.Biz.Base;
using HchWebPhr.Data.Models;
using HchWebPhr.Data.Repositories;
using HchWebPhr.Models.FormModels;
using HchWebPhr.Models.ViewModels;
using HchWebPhr.Service;
using HchWebPhr.Utilities;
using HchWebPhr.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using static ShareDll.PHR;

namespace HchWebPhr.Biz
{
    public class AccountBiz : BaseBiz
    {
        public bool CheckAuthentication(String UserName, String EncryptPasswd)
        {
            if (String.IsNullOrEmpty(UserName) || String.IsNullOrEmpty(EncryptPasswd))
            {
                ErrorCode = "401";
                ErrorMessage = "帳號或密碼錯誤！";
                return false;
            }
            var usr = new UserRepository();
            var user = usr.Get(x => x.UserName.Equals(UserName));
            if (user == null)
            {
                ErrorCode = "404";
                ErrorMessage = "找不到使用者帳號資料！";
                return false;
            }
            if (user.IsActive == false || EncryptPasswd != user.Password)
            {
                ErrorCode = "401";
                ErrorMessage = "帳號或密碼錯誤！";
                return false;
            }
            return true;
        }
        public bool CheckPatientIdentity(string EMail, DateTime BirthDate, out IList<SignUpPatient> PatientList)
        {
            PatientList = null;
            HchService svc = new HchService();
            IList<PT> PtList = svc.GetPatientByEMailAndBirthDate(EMail, BirthDate);
            if (PtList != null && PtList.Count > 0)
            {
                PatientList = PtList.Select(x => new SignUpPatient {
                    IdNo = x.IdNo,
                    ChartNo = x.ChartNo,
                    PatientName = x.PtName,
                    BirthDate = x.BirthDate.toDateTime(),
                    Sex = x.Sex,
                    BloodType = x.BloodType,
                    MobileNo = x.TelMobile,
                    Address = x.Address,
                    EMail = EMail
                }).ToList();
                return true;
            } else
            {
                ErrorCode = "404";
                ErrorMessage = "沒有符合的病人資料！";
                return false;
            }
        }
        
        public bool CheckUserNameAvailability(string UserName)
        {
            var user = this.GetUser(x => x.UserName.Equals(UserName));
            return (user == null);
        }

        public bool SignUpUser(SignUpPatient patientUser, out string ActivateToken) 
        {
            ActivateToken = Encrypt.desEncryptUrlSafeBase64(patientUser.EMail + "+" + DateTime.Now.ToString());

            var user = new User {
                Email = patientUser.EMail,
                ActiveToken = ActivateToken,
                LastLoginTime = DateTime.Now
            };
            
            var userInfo = new UserInfo {
                ChartNo = patientUser.ChartNo,
                IdNo = patientUser.IdNo,
                Name = patientUser.PatientName,
                BirthDate = patientUser.BirthDate,
                Sex = patientUser.Sex,
                BloodType = patientUser.BloodType,
                PhoneNo = patientUser.MobileNo,
                Address = patientUser.Address
            };

            user.UserInfo = userInfo;

            var usr = new UserRepository();
            var exist_user = usr.Get(x => x.Email.Equals(patientUser.EMail));
            if (exist_user != null)
            {
                if (patientUser.BirthDate == exist_user.UserInfo.BirthDate)
                {
                    exist_user.ActiveToken = ActivateToken;
                    exist_user.LastLoginTime = DateTime.Now;
                    var rtn = usr.Update(exist_user);
                    if (rtn <= 0)
                    {
                        this.ErrorCode = "900";
                        this.ErrorMessage = "Database Update Fail ! Please Notify System Administrator !";
                        return false;
                    }
                }
            } 
            var rtn2 = usr.Create(user);
            if (rtn2 <= 0)
            {
                this.ErrorCode = "900";
                this.ErrorMessage = "Database Insert Fail ! Please Notify System Administrator !";
                return false;
            }
            return true;
        }

        public bool ActivateUser(ActivateModel activateUser)
        {
            var resUser = new UserRepository();
            var user = resUser.Get(x => x.UserId == activateUser.Id);
            if (user == null) {
                ErrorCode = "404";
                ErrorMessage = "帳號啟用失敗，請聯絡系統管理員！";
                return false;
            }
            user.UserName = activateUser.UserName;
            user.Password = Encrypt.Password(activateUser.RePassword);
            user.Email = activateUser.Email;
            user.IsActive = true;
            user.ActiveToken = "";
            user.LastLoginTime = DateTime.Now;
            user.LastLoginIp = IP.GetAddress();
            try
            {
                var rtn = resUser.Update(user);
                return (rtn > 0);
            }
            catch (Exception ex)
            {
                this.ErrorException = ex;
                this.ErrorCode = "500";
                this.ErrorMessage = ex.InnerException.Message;
                return false;
            }
        }

        public bool LoginUser(string UserName, string IpAddress)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                ErrorCode = "400";
                ErrorMessage = "找不到使用者帳號資料！";
                return false;
            }
            else
            {
                var usr = new UserRepository();
                var user = usr.Get(x => x.UserName.Equals(UserName));
                if (user == null)
                {
                    ErrorCode = "400";
                    ErrorMessage = "找不到使用者帳號資料！";
                    return false;
                }
                else
                {
                    user.LastLoginTime = DateTime.Now;
                    user.LastLoginIp = IpAddress;
                    if (usr.Update(user) <= 0)
                    {
                        ErrorCode = "500";
                        ErrorMessage = "更新使用者資料失敗，請聯絡系統管理員！";
                        return false;
                    }
                }
            }
            return true;
        }

        public bool SetUserForgetPassword(User ForgetPwdUser, out string ForgetPasswordToken)
        {
            ForgetPasswordToken = "";
            ForgetPasswordToken = Encrypt.desEncryptUrlSafeBase64(ForgetPwdUser.UserName + "+" + ForgetPwdUser.Password + "+" + DateTime.Now.ToString());
            ForgetPwdUser.ForgetPasswordToken = ForgetPasswordToken;
            var resUser = new UserRepository();
            if (resUser.Update(ForgetPwdUser) <= 0)
            {
                ErrorCode = "500";
                ErrorMessage = "無法正確產生忘記密碼驗證連結，請聯絡系統管理員！";
                return false;
            }
            return true;
        }

        public bool UpdatePassword(string UserName, string EncryptNewPassword)
        {
            var resUser = new UserRepository();
            var user = resUser.Get(x => x.UserName.Equals(UserName));
            if (user == null)
            {
                ErrorCode = "400";
                ErrorMessage = "找不到使用者帳號資料！";
                return false;
            }
            user.Password = EncryptNewPassword;
            var rtn = resUser.Update(user);
            if (rtn <=0)
            {
                ErrorCode = "500";
                ErrorMessage = "資料庫更新密碼失敗！";
                return false;
            }
            return true;
        }

        public bool UpdateProfileImage(string UserName, string ImageUrl)
        {
            var resUser = new UserRepository();
            var user = resUser.Get(x => x.UserName.Equals(UserName));
            if (user == null)
            {
                ErrorCode = "404";
                ErrorMessage = "找不到使用者帳號資料！！";
                return false;
            }
            user.UserInfo.ProfileImage = ImageUrl;
            var rtn = resUser.Update(user);
            if (rtn <=0)
            {
                ErrorCode = "500";
                ErrorMessage = "資料庫更新大頭貼失敗！";
                return false;
            }
            return true;
        }
        public User GetUser(Expression<Func<User, bool>> predicate)
        {
            var resUser = new UserRepository();
            return resUser.Get(predicate);
        }
    }
}
