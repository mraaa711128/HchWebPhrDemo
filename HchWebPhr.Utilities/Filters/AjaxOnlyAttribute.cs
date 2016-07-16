using System;
using System.Reflection;
using System.Web.Mvc;

namespace HchWebPhr.Utilities.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext context, MethodInfo methodInfo)
        {
            return context.RequestContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}