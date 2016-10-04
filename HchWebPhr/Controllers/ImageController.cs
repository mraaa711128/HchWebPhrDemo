using HchWebPhr.Biz;
using HchWebPhr.Controllers.Base;
using HchWebPhr.Models.FormModels;
using HchWebPhr.Models.ViewModels;
using HchWebPhr.Utilities.Filters;
using HchWebPhr.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HchWebPhr.Controllers
{
    [Authorize]
    public class ImageController : JsonNetController
    {
        // GET: Image
        //[HttpGet]
        //public ActionResult Albums()
        //{
        //    var username = User.Identity.Name;
        //    var acc = new AccountBiz();
        //    var user = acc.GetUser(x => x.UserName.Equals(username));
        //    if (user == null)
        //    {
        //        return View("Error", new ErrorContext
        //        {
        //            ErrorCode = "404",
        //            ErrorMessage = "使用者資料遺失，請重新登入！"
        //        });
        //    }
        //    var img = new ImageBiz();
        //    IList<PhotoAlbumInfo> albums = null;
        //    var endDate = DateTime.Today;
        //    var startDate = endDate - new TimeSpan(180, 0, 0, 0, 0);
        //    if (img.getAlbums(user.UserInfo.ChartNo, startDate, endDate, out albums) == false)
        //    {
        //        albums = new List<PhotoAlbumInfo>();
        //    }
        //    return View(albums);
        //}
        [HttpGet]
        public ActionResult Albums(string StartDate = "", string EndDate = "")
        {
            DateTime startDate, endDate;

            var sDate = DateTime.TryParse(StartDate, out startDate);
            var eDate = DateTime.TryParse(EndDate, out endDate);

            var searchModel = new SearchAlbumModel {
                StartDate = new DateTime(2016, 8, 1),
                EndDate = DateTime.Today
            };

            if (sDate) { searchModel.StartDate = startDate; }
            if (eDate) { searchModel.EndDate = endDate; }

            ViewBag.Remark = "";
            return View(searchModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetAlbums(DateTime StartDate, DateTime EndDate)
        {
            var username = User.Identity.Name;
            var user = FormAuthHelper.GetLoginUser();
            var ChartNo = user.UserInfo.ChartNo;
            var img = new ImageBiz();
            IList<PhotoAlbumInfo> AlbumList = null;
            if (StartDate > EndDate)
            {
                var tmpDate = StartDate;
                StartDate = EndDate;
                EndDate = tmpDate;
            }
            if (img.GetAlbums(ChartNo, StartDate, EndDate, out AlbumList) == false)
            {
                AlbumList = new List<PhotoAlbumInfo>();
            }
            return PartialView("_AlbumList", AlbumList);
        }

        [HttpGet]
        public ActionResult Images(string ApplyNo)
        {
            var user = FormAuthHelper.GetLoginUser();
            var ChartNo = user.UserInfo.ChartNo;
            var img = new ImageBiz();
            PhotoAlbumInfo ImageAlbum = null;
            if (img.GetAlbum(ChartNo, ApplyNo, out ImageAlbum) == false)
            {
                ViewBag.ErrorCode = img.ErrorCode;
                ViewBag.ErrorMessage = img.ErrorMessage;
            }
            return View(ImageAlbum);
        }
    }
}