using HchWebPhr.Biz;
using HchWebPhr.Models.ViewModels;
using HchWebPhr.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HchWebPhr.Controllers
{
    [Authorize]
    public class BabyController : Controller
    {
        // GET: Baby
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult BabyList()
        {
            var user = FormAuthHelper.GetLoginUser();
            var chartNo = user.UserInfo.ChartNo;

            IList<PatientChildInfo> childrenList = null;
            var babyBiz = new BabyBiz();

            if (babyBiz.GetChildrenList(chartNo, out childrenList) == false)
            {
                childrenList = new List<PatientChildInfo>();
            }

            return View(childrenList);
        }
    }
}