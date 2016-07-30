using HchWebPhr.Biz;
using HchWebPhr.Controllers.Base;
using HchWebPhr.Data.Models;
using HchWebPhr.Data.Repositories;
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
    public class AuthController : JsonNetController
    {
        //[HttpGet]
        //[AdminOnly]
        //public ActionResult Users()
        //{

        //}

        //[HttpPost]
        //[AdminOnly]
        //public ActionResult Users(IList<User> UserList)
        //{

        //}

        [HttpGet]
        [AdminOnly]
        public ActionResult Features()
        {
            var ft = FeatureRepository.GetRepository();
            var featureList = ft.GetAll();
            var result = featureList.Select(x => new FeatureModel
            {
                FeatureId = x.FeatureId,
                FeatureName = x.FeatureName,
                Remark = x.Remark
            }).ToList();
            return View(result);
        }

        //[HttpPost]
        //[AdminOnly]
        //public ActionResult Features(IList<Feature> FeatureList)
        //{

        //}

        [HttpGet]
        [AdminOnly]
        public ActionResult Feature(string FeatureId)
        {
            bool IsAddFeature = string.IsNullOrEmpty(FeatureId);
            FeatureModel feature = new FeatureModel();
            if (IsAddFeature == false)
            {
                var ft = FeatureRepository.GetRepository();
                var result = ft.Get(x => x.FeatureId.Equals(FeatureId));
                if (result == null)
                {
                    return View("Error", new ErrorContext {
                        ErrorCode = "404",
                        ErrorMessage = "找不到符合的功能！"
                    });
                }
                feature.FeatureId = result.FeatureId;
                feature.FeatureName = result.FeatureName;
                feature.Remark = result.Remark;
            }
            var featureList = this.FeatureList();
            ViewBag.FeatureList = featureList.Select(x => new SelectListItem
            {
                Value = x.FeatureId.ToString(),
                Text = x.FeatureName
            }).ToList();
            return View(feature);
        }

        [HttpPost]
        [AdminOnly]
        public ActionResult Feature(FeatureModel FeatureObj)
        {
            var decodeRemark = HttpUtility.HtmlDecode(FeatureObj.Remark);
            if (ModelState.IsValid == false)
            {
                FeatureObj.Remark = decodeRemark;
                return View(FeatureObj);
            }
            var auth = new AuthBiz();
            if (auth.SaveFeature(FeatureObj) == false)
            {
                FeatureObj.Remark = decodeRemark;
                return View("Error", new ErrorContext
                {
                    ErrorCode = auth.ErrorCode,
                    ErrorMessage = auth.ErrorMessage
                });
            }
            CacheHelper.SetCache("UI_MENU_FEATURES", null);
            return RedirectToAction("Features", "Auth");
        }

        [HttpGet]
        [AdminOnly]
        public ActionResult DeleteFeature(string FeatureId)
        {
            if (string.IsNullOrEmpty(FeatureId) || FeatureId.IsNumeric())
            {
                return View("Error", new ErrorContext
                {
                    ErrorCode = "502",
                    ErrorMessage = "刪除失敗！錯誤的功能代碼！"
                });
            }
            var auth = new AuthBiz();
            if (auth.DeleteFeature(int.Parse(FeatureId)) == false)
            {
                return View("Error", new ErrorContext {
                    ErrorCode = auth.ErrorCode,
                    ErrorMessage = auth.ErrorMessage
                });
            }
            CacheHelper.SetCache("UI_MENU_FEATURES", null);
            return RedirectToAction("Features", "Auth");
        }

        private IList<FeatureModel> FeatureList()
        {
            IList<FeatureModel> mFeatures = null;
#if DEBUG
#else
            mFeatures = CacheHelper.GetCache("UI_MENU_FEATURES") as IList<FeatureModel>;
#endif
            if (mFeatures == null)
            {
                AuthBiz acc = new AuthBiz();
                var Features = acc.GetAllFeatures();
                if (Features == null)
                {
                    return new List<FeatureModel>();
                }
                mFeatures = Features.Select(x => new FeatureModel
                {
                    FeatureId = x.FeatureId,
                    FeatureName = x.FeatureName,
                    Remark = HttpUtility.HtmlDecode(x.Remark)
                }).ToList();
                CacheHelper.SetCache("UI_MENU_FEATURES", mFeatures);
            }
            return mFeatures;
        }
    }
}