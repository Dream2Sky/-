using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.dongfangyunwang.web.Global
{
    public class IsAdminAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (System.Web.HttpContext.Current.Session["admin"] == null)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}