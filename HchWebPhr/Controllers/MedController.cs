using HchWebPhr.Biz;
using HchWebPhr.Controllers.Base;
using HchWebPhr.Models.FormModels;
using HchWebPhr.Models.ViewModels;
using HchWebPhr.Utilities.Extensions;
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
    public class MedController : JsonNetController
    {
        // GET: Med
        public ActionResult MedList(string StartDate, string EndDate)
        {
            DateTime startDate, endDate;

            var sDate = DateTime.TryParse(StartDate, out startDate);
            var eDate = DateTime.TryParse(EndDate, out endDate);

            var searchModel = new SearchMedModel
            {
                StartDate = new DateTime(2016, 9, 1),
                EndDate = DateTime.Today
            };

            if (sDate) { searchModel.StartDate = startDate; }
            if (eDate) { searchModel.EndDate = endDate; }

            ViewBag.ClinicDivs = this.Divs();
            return View(searchModel);
        }

        [HttpGet]
        [AjaxOnly]
        public JsonNetResult GetMedList(DateTime StartDate, DateTime EndDate, string DivNo)
        {
            var username = User.Identity.Name;
            var user = FormAuthHelper.GetLoginUser();
            var ChartNo = user.UserInfo.ChartNo;

            IList<MedListInfo> medList = null;
            var medBiz = new MedBiz();
            if (medBiz.GetMedListByChartNoAndDateRange(ChartNo, StartDate, EndDate, out medList) == false)
            {
                return new JsonNetResult
                {
                    ContentType = "application/json",
                    Data = new { status = "fail", message = medBiz.ErrorCode + "-" + medBiz.ErrorMessage }
                };
            }
            medList = medList.OrderByDescending(x => x.ClinicDate).ThenBy(x => x.DivNo).ToList();
            if (DivNo.Equals("*") == false)
            {
                medList = medList.Where(x => x.DivNo.Equals(DivNo)).ToList();
            }
            return new JsonNetResult
            {
                ContentType = "application/json",
                Data = new { status = "success", data = medList}
            };
        }

        [HttpGet]
        [AjaxOnly]
        public JsonNetResult GetMedItems(DateTime ClinicDate, string DivNo)
        {
            var username = User.Identity.Name;
            var user = FormAuthHelper.GetLoginUser();
            var ChartNo = user.UserInfo.ChartNo;

            IList<MedItemInfo> medItems = null;
            var medBiz = new MedBiz();
            if (medBiz.GetMedDetailByChartNoClinicDateAndDivNo(ChartNo, ClinicDate, DivNo, out medItems) == false)
            {
                return new JsonNetResult
                {
                    ContentType = "application/json",
                    Data = new { status = "fail", message = medBiz.ErrorCode + "-" + medBiz.ErrorMessage }
                };
            }
            return new JsonNetResult
            {
                ContentType = "application/json",
                Data = new { status = "success", data = medItems }
            };
        }

        private IList<SelectListItem> Divs()
        {
            IList<SelectListItem> data = null;
#if DEBUG
#else
            data = CacheHelper.GetCache("UI_CLINIC_DIVS") as List<SelectListItem>;
#endif

            if (data == null)
            {
                var medBiz = new MedBiz();

                data = medBiz.GetClinicDivs().Select(x => new SelectListItem
                {
                    Value = x.Value,
                    Text = x.Text,
                    Selected = x.Selected
                }).ToList();
                CacheHelper.SetCache("UI_CLINIC_DIVS", data);
            }
            return data;
        }
    }
}