using HchWebPhr.Biz;
using HchWebPhr.Controllers.Base;
using HchWebPhr.Models.FormModels;
using HchWebPhr.Models.ViewModels;
using HchWebPhr.Utilities.Filters;
using HchWebPhr.Utilities.Helpers;
using HchWebPhr.Utilities.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HchWebPhr.Controllers
{
    [Authorize]
    public class LabController : JsonNetController
    {
        // GET: Lab
        [HttpGet]
        public ActionResult LabList(string StartDate = "", string EndDate = "")
        {
            DateTime startDate, endDate;

            var sDate = DateTime.TryParse(StartDate, out startDate);
            var eDate = DateTime.TryParse(EndDate, out endDate);

            var searchModel = new SearchLabModel
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
        public JsonNetResult GetLabList(DateTime StartDate, DateTime EndDate)
        {
            var username = User.Identity.Name;
            var user = FormAuthHelper.GetLoginUser();
            var ChartNo = user.UserInfo.ChartNo;

            IList<LabListInfo> labList = null;
            var labBiz = new LabBiz();
            if (labBiz.GetLabList(ChartNo, StartDate, EndDate, out labList) == false)
            {
                return new JsonNetResult
                {
                    ContentType = "application/json",
                    Data = new { status = "fail", message = labBiz.ErrorCode + "-" + labBiz.ErrorMessage }
                };
            }
            return new Base.JsonNetResult
            {
                ContentType = "application/json",
                Data = new { status = "success", data = labList }
            };
        }

        [HttpGet]
        public ActionResult LabResult(string LabNo, string LabType)
        {
            var labBiz = new LabBiz();
            switch (LabType)
            {
                case Utilities.Types.LabType.Normal:
                    IList<LabItemXInfo> resultModelX = null;
                    if (labBiz.GetLabResultX(LabNo, out resultModelX) == false)
                    {
                        //NLog
                    }
                    return View("LabResultX", resultModelX);
                case Utilities.Types.LabType.Diagnosis:
                    LabItemGInfo resultModelG = null;
                    if (labBiz.GetLabResultG(LabNo, out resultModelG) == false)
                    {
                        //NLog
                    }
                    return View("LabResultG", resultModelG);
                case Utilities.Types.LabType.Pdf:
                    LabItemNInfo resultModelN = null;
                    if (labBiz.GetLabResultN(LabNo, out resultModelN) == false)
                    {
                        //NLog
                    }
                    return View("LabResultN", resultModelN);
                default:
                    return new EmptyResult();
            }
        }
    }
}