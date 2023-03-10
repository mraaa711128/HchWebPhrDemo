using HchWebPhr.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HchWebPhr.Controllers
{
    public class HomeController : JsonNetController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}