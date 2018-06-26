using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HchWebPhr.Biz;
using HchWebPhr.Controllers.Base;
using HchWebPhr.Models.ViewModels;
using HchWebPhr.Utilities.Filters;
using HchWebPhr.Utilities.Helpers;

namespace HchWebPhr.Controllers
{
    [Authorize]
    public class VaccineController : JsonNetController
    {
        // GET: Vaccine
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult VaccineList()
        {
            var user = FormAuthHelper.GetLoginUser();
            var chartNo = user.UserInfo.ChartNo;

            IList<VaccineChildInfo> childrenList = null;
            var vacBiz = new VaccineBiz();

            if (vacBiz.GetChildrenList(chartNo, out childrenList) == false)
            {
                childrenList = new List<VaccineChildInfo>();
            }

            return View(childrenList);
        }

        [HttpGet]
        [AjaxOnly]
        public JsonNetResult GetChildVaccineList(string ChartNo)
        {
            IList<VaccineDetailInfo> vaccineList = null;
            var vacBiz = new VaccineBiz();

            if (vacBiz.GetChildVaccineList(ChartNo, out vaccineList) == false)
            {
                return new JsonNetResult
                {
                    ContentType = "application/json",
                    Data = new { status = "fail", message = vacBiz.ErrorCode + "-" + vacBiz.ErrorMessage }
                };
            }
            vaccineList = vaccineList.OrderByDescending(v => v.InjectDate).ThenBy(v => v.VaccineCode).ToList();
            return new JsonNetResult
            {
                ContentType = "application/json",
                Data = new { status = "success", data = vaccineList }
            };
        }
    }
}