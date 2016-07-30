using HchWebPhr.Data.Models;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Security;

namespace HchWebPhr.Utilities.Helper
{
    public static class FormAuthHelper
    {
        public static User GetLoginUser()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsIdentity id = HttpContext.Current.User.Identity as FormsIdentity;
                FormsAuthenticationTicket ticket = id.Ticket;
                User LoginUser = JsonConvert.DeserializeObject<User>(ticket.UserData);
                return LoginUser;
            }
            return null;
        }

        public static void SetLoginUser(User loginUser)
        {
            FormsAuthenticationTicket ticket = GenerateTicket(loginUser);
            string encryptTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
            cookie.HttpOnly = true;
            
            var formCookie = HttpContext.Current.Response.Cookies.Get(FormsAuthentication.FormsCookieName);
            if (formCookie == null)
            {
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
        }

        private static FormsAuthenticationTicket GenerateTicket(User loginUser)
        {
            return new FormsAuthenticationTicket(1,
                                                 loginUser.UserName,
                                                 DateTime.Now,
                                                 DateTime.Now.Add(FormsAuthentication.Timeout),
                                                 false,
                                                 JsonConvert.SerializeObject(loginUser),
                                                 FormsAuthentication.FormsCookiePath);
        }
    }
}