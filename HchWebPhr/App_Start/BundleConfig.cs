using HchWebPhr.Utilities.Helpers;
using System.Web.Optimization;
using System;
using System.Web;

namespace HchWebPhr
{
    public class BundleConfig
    {
        // 如需「搭配」的詳細資訊，請瀏覽 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            if (ConfigHelper.GetBoolean("MINIFY"))
            {
                bundles.Clear();
                bundles.ResetAll();
            }

            #region Bundle JS

            bundles.Add(new ScriptBundle("~/Content/js/jquery")
                .Include("~/Content/bower/jquery/dist/jquery.js"));

            bundles.Add(new ScriptBundle("~/Content/js/bootstrap")
                .Include("~/Content/bower/bootstrap/dist/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/Content/js/respond")
                .Include("~/Content/bower/respond/dest/respond.src.js")
                .Include("~/Content/bower/respond/dest/respond.matchmedia.addListener.src.js"));

            bundles.Add(new ScriptBundle("~/Content/js/bootstrap-table")
                .Include("~/Content/bower/bootstrap-table/dist/bootstrap-table.js")
                .Include("~/Content/bower/bootstrap-table/dist/bootstrap-table-locale-all.js")
                .Include("~/Content/bower/bootstrap-table/dist/extensions/mobile/bootstrap-table-mobile.js"));

            bundles.Add(new ScriptBundle("~/Content/js/bootstrap-sidebar")
                .Include("~/Content/component/bootstrap-sidebar/bootstrap-sidebar.js"));

            bundles.Add(new ScriptBundle("~/Content/js/bootstrap-datetimepicker")
                .Include("~/Content/component/bootstrap-datetimepicker/bootstrap-datetimepicker.js")
                .Include("~/Content/component/bootstrap-datetimepicker/locales/bootstrap-datetimepicker.zh-TW.js"));

            bundles.Add(new ScriptBundle("~/Content/js/load-spinner")
                .Include("~/Content/component/load-spinner/js/loading.js"));

            bundles.Add(new ScriptBundle("~/Content/js/photoswipe")
                .Include("~/Content/bower/photoswipe/dist/photoswipe.js")
                .Include("~/Content/bower/photoswipe/dist/photoswipe-ui-default.js"));

            bundles.Add(new ScriptBundle("~/Content/js/cropper")
                .Include("~/Content/bower/cropper/dist/cropper.js"));

            bundles.Add(new ScriptBundle("~/Content/js/ckeditor")
                .Include("~/Content/component/ckeditor/ckeditor.js")
                .Include("~/Content/component/ckeditor/adapters/jquery.js"));

            bundles.Add(new ScriptBundle("~/Content/js/scripts")
                .Include("~/Content/js/captcha.js")
                .Include("~/Content/js/hchwebphr.js"));

            #endregion

            #region Bundle CSS

            bundles.Add(new StyleBundle("~/Content/css/bootstrap")
                .Include("~/Content/bower/bootstrap/dist/css/bootstrap.css", new CssRewriteUrlTransformFixed()));

            bundles.Add(new StyleBundle("~/Content/css/bootstrap-extra")
                .Include("~/Content/component/bootstrap-extra/bootstrap-extra.css"));

            bundles.Add(new StyleBundle("~/Content/css/font-awesome")
                .Include("~/Content/component/font-awesome-4.6.3/css/font-awesome.css", new CssRewriteUrlTransformFixed()));

            bundles.Add(new StyleBundle("~/Content/css/bootstrap-table")
                .Include("~/Content/bower/bootstrap-table/dist/bootstrap-table.css"));

            bundles.Add(new StyleBundle("~/Content/css/bootstrap-sidebar")
                .Include("~/Content/component/bootstrap-sidebar/bootstrap-sidebar.css"));

            bundles.Add(new StyleBundle("~/Content/css/bootstrap-datetimepicker")
                .Include("~/Content/component/bootstrap-datetimepicker/bootstrap-datetimepicker.css"));

            bundles.Add(new StyleBundle("~/Content/css/load-spinner")
                .Include("~/Content/component/load-spinner/css/loading.css", new CssRewriteUrlTransformFixed()));

            bundles.Add(new StyleBundle("~/Content/css/photoswipe")
                .Include("~/Content/bower/photoswipe/dist/photoswipe.css")
                .Include("~/Content/bower/photoswipe/dist/default-skin/default-skin.css", new CssRewriteUrlTransformFixed()));

            bundles.Add(new StyleBundle("~/Content/css/cropper")
                .Include("~/Content/bower/cropper/dist/cropper.css"));

            bundles.Add(new StyleBundle("~/Content/css/styles")
                .Include("~/Content/css/style.css")
                .Include("~/Content/css/dashboard.css"));

            #endregion

            #region 自定忽略清單

            bundles.IgnoreList.Clear();
            bundles.IgnoreList.Ignore("*.min.js", OptimizationMode.WhenEnabled);
            bundles.IgnoreList.Ignore("*.min.css", OptimizationMode.WhenEnabled);
            bundles.IgnoreList.Ignore("*.intellisense.js");
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);

            #endregion

        }
    }

    public class CssRewriteUrlTransformFixed : IItemTransform
    {
        public string Process(string includedVirtualPath, string input)
        {
            return new CssRewriteUrlTransform().Process("~" + VirtualPathUtility.ToAbsolute(includedVirtualPath), input);
        }
    }
}
