using HchWebPhr.Biz;
using HchWebPhr.Controllers.Base;
using HchWebPhr.Data.Models;
using HchWebPhr.Models.FormModels;
using HchWebPhr.Utilities.Filters;
using HchWebPhr.Utilities.Helper;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace HchWebPhr.Controllers
{
    [Authorize]
    public class UserController : JsonNetController
    {
        // GET: User
        [HttpGet]
        public ActionResult Dashboard()
        {
            User loginUser = FormAuthHelper.GetLoginUser();

            return View(loginUser.UserInfo);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult UpdateProfileImage()
        {
            var username = User.Identity.Name;
            var acc = new AccountBiz();
            var userInfo = acc.GetUser(x => x.UserName.Equals(username)).UserInfo;
            return PartialView("_UpdateProfileImage", userInfo);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult UpdateProfileImage(string ImageDataUrl)
        {
            var ImageUrl = "";
            var username = User.Identity.Name;
            if (this.SaveImageDataUrlToFile(ImageDataUrl, "~/Content/img/avatars/" + username,"avatar",out ImageUrl) == true)
            {
                var acc = new AccountBiz();
                if (acc.UpdateProfileImage(username, ImageUrl) == false)
                {
                    return new JsonNetResult
                    {
                        Data = new { status = "fail", code = acc.ErrorCode, message = acc.ErrorMessage },
                        ContentType = "application/json"
                    };
                }
                else
                {
                    var user = acc.GetUser(x => x.UserName.Equals(username));
                    if (user == null)
                    {
                        return new JsonNetResult
                        {
                            Data = new { status = "fail", code = acc.ErrorCode, message = acc.ErrorMessage },
                            ContentType = "application/json"
                        };
                    }
                    FormAuthHelper.SetLoginUser(user);
                    return new JsonNetResult
                    {
                        Data = new { status = "success", code = "200", message = "" },
                        ContentType = "application/json"
                    };
                }
            } else
            {
                return new JsonNetResult
                {
                    Data = new { status = "fail", code = "500", message = "伺服器檔案儲存失敗！" },
                    ContentType = "application/json"
                };
            }
        }

        private bool SaveImageDataUrlToFile(string imgDataUrl, string virtualPath, string fileName, out string virtualFilePath)
        {
            virtualFilePath = "";
            try
            {
                var ImgMatch = Regex.Match(imgDataUrl, @"data:image/(?<type>.+?);(?<encoding>.+?),(?<data>.+)");
                if (ImgMatch == null)
                {
                    return false;
                }

                var base64ImgData = ImgMatch.Groups["data"].Value;
                var typeImgData = ImgMatch.Groups["type"].Value;
                var encodeImgData = ImgMatch.Groups["encoding"].Value;
                var binImgData = Convert.FromBase64String(base64ImgData);
                if (Directory.Exists(Server.MapPath(virtualPath)) == false)
                {
                    Directory.CreateDirectory(Server.MapPath(virtualPath));
                }

                var finalFileName = fileName + "_" + DateTime.Now.Millisecond + "." + typeImgData;
                System.IO.File.WriteAllBytes(Server.MapPath(virtualPath + "/" + finalFileName), binImgData);
                virtualFilePath = virtualPath + "/" + finalFileName;
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

    }
}