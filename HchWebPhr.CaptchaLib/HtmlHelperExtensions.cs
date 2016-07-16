using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace HchWebPhr.CaptchaLib
{
	public static class HtmlHelperExtensions
	{

		public static IHtmlString Captcha(this HtmlHelper htmlHelper, string name, string actionName, string refreshLabel)
		{
			return Captcha(htmlHelper, name, actionName, null /* controllerName */, refreshLabel, null /* routeValue */);
		}

		public static IHtmlString Captcha(this HtmlHelper htmlHelper, string name,
			string actionName, string refreshLabel, object routeValues)
		{
			return Captcha(htmlHelper, name, actionName, null /* controllerName */, refreshLabel, routeValues);
		}

		public static IHtmlString Captcha(this HtmlHelper htmlHelper, string name,
			string actionName, string controllerName, string refreshLabel)
		{
			return Captcha(htmlHelper, name, null /* routeName */, actionName, controllerName, null /* routeValues */, refreshLabel, null);
		}

		public static IHtmlString Captcha(this HtmlHelper htmlHelper, string name,
			string actionName, string controllerName, string refreshLabel, object routeValues)
		{
			return Captcha(htmlHelper, name, null /* routeName */, actionName, controllerName, routeValues, refreshLabel, null);
		}

		public static IHtmlString Captcha(this HtmlHelper htmlHelper, string name,
			string routeName, string actionName, string controllerName,
			object routeValues, string refreshLabel, object htmlAttributes)
		{
			return CaptchaHelper(htmlHelper, name, routeName, actionName, controllerName, routeValues, refreshLabel, null);
		}

		public static IHtmlString CaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
			string actionName, string refreshLabel)
		{
			return CaptchaFor(htmlHelper, expression, actionName, null /* controllerName */, refreshLabel);
		}

		public static IHtmlString CaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
			string actionName, string controllerName, string refreshLabel)
		{
			return CaptchaFor(htmlHelper, expression, actionName, controllerName, null /* routeValues */, refreshLabel, null /* htmlAttributes */);
		}

		public static IHtmlString CaptchaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
			string actionName, string controllerName, object routeValues, string refreshLabel, object htmlAttributes)
		{
			return CaptchaHelper(htmlHelper, ExpressionHelper.GetExpressionText(expression), null /* routeName */, actionName, controllerName, routeValues, refreshLabel, htmlAttributes);
		}

		private static IHtmlString CaptchaHelper(this HtmlHelper htmlHelper, string name,
			string routeName, string actionName, string controllerName,
			object routeValues, string refreshLabel, object htmlAttributes)
		{
			var container = new TagBuilder("div");
			container.MergeAttribute("class", "captchaContainer");

			var image = new TagBuilder("img");
			image.MergeAttribute("src", UrlHelper.GenerateUrl(routeName, actionName, controllerName, (RouteValueDictionary)routeValues,
				htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext, true));
			image.MergeAttribute("border", "0");

            var refresh = new TagBuilder("a");
            refresh.MergeAttribute("href", "javascript:void(0);");
            refresh.MergeAttribute("class", "newCaptcha");
            refresh.MergeAttribute("title", refreshLabel);
            refresh.InnerHtml = image.ToString(TagRenderMode.SelfClosing);

            container.InnerHtml = refresh.ToString(TagRenderMode.Normal);

            var textBox = htmlHelper.TextBox(name, "", htmlAttributes);

            return new HtmlString(
                textBox + Environment.NewLine +
                container.ToString(TagRenderMode.Normal) + Environment.NewLine);
		}
	}
}