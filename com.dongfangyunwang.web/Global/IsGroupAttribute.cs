using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.dongfangyunwang.web.Global
{
    public class IsGroupAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (System.Web.HttpContext.Current.Session["group"] == null)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}