using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HchWebPhr.Biz;
using HchWebPhr.Controllers.Base;
using HchWebPhr.Models.FormModels;
using HchWebPhr.Models.ViewModels;
using HchWebPhr.Utilities.Filters;
using HchWebPhr.Utilities.Helpers;

namespace HchWebPhr.Controllers
{
    public class PregnancyController : JsonNetController
    {
        // GET: Pregnancy
        [HttpGet]
        public ActionResult PregnancyList(string StartDate = "", string EndDate = "")
        {
            DateTime startDate, endDate;

            var sDate = DateTime.TryParse(StartDate, out startDate);
            var eDate = DateTime.TryParse(EndDate, out endDate);

            var searchModel = new SearchPregnancyModel
            {
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
        public JsonNetResult GetPregnancyList(DateTime StartDate, DateTime EndDate)
        {
            var username = User.Identity.Name;
            var user = FormAuthHelper.GetLoginUser();
            var ChartNo = user.UserInfo.ChartNo;

            IList<PregnancyDetailInfo> pregList = new List<PregnancyDetailInfo>();
            var PregBiz = new PregnancyBiz();
            if (PregBiz.GetPregnancyListByChartNoAndDateRange(ChartNo, StartDate, EndDate, out pregList) == false)
            {
                return new JsonNetResult
                {
                    ContentType = "application/json",
                    Data = new { status = "fail", message = PregBiz.ErrorCode + "-" + PregBiz.ErrorMessage }
                };
            }

            return new JsonNetResult
            {
                ContentType = "application/json",
                Data = new { status = "success", data = pregList }
            };
        }
    }
}