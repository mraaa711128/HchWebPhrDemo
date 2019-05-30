using HchWebPhr.Biz;
using HchWebPhr.Models.ViewModels;
using HchWebPhr.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            Task<IList<PatientChildInfo>> vacTask = Task.Run<IList<PatientChildInfo>>(() => {
                return childrenList.ToList();
            });
            Task<IList<PatientChildInfo>> labTask = Task.Run<IList<PatientChildInfo>>(() => {
                //return childrenList.Where(c => c.BirthDate >= new DateTime(2019, 6, 1))
                //                   .ToList();
                return childrenList.ToList();
            });
            Task.WaitAll(vacTask, labTask);
            var result = new PatientChildrenListInfo
            {
                VaccineChildrenList = vacTask.Result,
                LabChildrenList = labTask.Result
            };

            return View(result);
        }
    }
}