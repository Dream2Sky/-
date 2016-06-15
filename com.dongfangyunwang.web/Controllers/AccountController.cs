using com.dongfangyunwang.IBLL;
using com.dongfangyunwang.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.dongfangyunwang.web.Controllers
{
    public class AccountController : Controller
    {
        private IMemberBLL _memberBLL;
        public AccountController(IMemberBLL memberBLL)
        {
            _memberBLL = memberBLL;
        }
        // GET: Account
        public ActionResult Login(string msg)
        {
            ViewBag.msg = msg;
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountModel model)
        {
            // error msg 
            string msg = string.Empty;
            try
            {             
                if (model == null)
                {
                    msg = "提交的数据为空";
                    return RedirectToAction("Login", "Account", new { msg = msg });
                }

                if (model.isadmin == "y")
                {
                    if (_memberBLL.Login(model.account, model.password, model.isadmin))
                    {
                        System.Web.HttpContext.Current.Session["admin"] = model.account;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        msg = "账号或密码错误";
                        return RedirectToAction("Login", "Account", new { msg = msg });
                    }
                }
                else
                {
                    if (_memberBLL.Login(model.account, model.password, model.isadmin))
                    {
                        System.Web.HttpContext.Current.Session["member"] = model.account;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        msg = "账号或密码错误";
                        return RedirectToAction("Login", "Account", new { msg = msg });
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                msg = "系统错误";
                return RedirectToAction("Login", "Account", new { msg = msg });
            }
        }
    }
}