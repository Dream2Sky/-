using com.dongfangyunwang.entity;
using com.dongfangyunwang.IBLL;
using com.dongfangyunwang.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using com.jiechengbao.common;

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

        /// <summary>
        ///  修改密码页 
        ///  这里不加 [IsAdmin] 或 [IsLogin]
        ///  在方法里面已经做了用户角色权限的判断
        /// </summary>
        /// <returns></returns>
        public ActionResult ResetPassword()
        {
            // 判断是否有用户角色权限
            if (System.Web.HttpContext.Current.Session["admin"] == null && System.Web.HttpContext.Current.Session["member"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            
            return View();
        }

        /// <summary>
        ///  修改密码 这里偷懒没有分类讨论失败情况
        ///  失败时统一返回 Json("False",....)
        ///  
        ///  因为此方法想让所有用户角色都可以访问
        ///  所以这里不单独加 [IsAdmin] 或者 [IsLogin]
        ///  
        ///  已经在方法里面实现了角色判断了
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResetPassword(PasswordModel model)
        {

            // 单独判断这个方法是否有权限访问
            string username = (System.Web.HttpContext.Current.Session["admin"] == null) ? (System.Web.HttpContext.Current.Session["member"] == null ? "" : System.Web.HttpContext.Current.Session["member"].ToString()) : System.Web.HttpContext.Current.Session["admin"].ToString();

            if (string.IsNullOrEmpty(username))
            {
                // 没有登陆权限 返回登陆页
                return RedirectToAction("Login", "Account");
            }
            else
            {
                // 根据用户名  获得Member
                Member member = _memberBLL.GetMemberByAccount(username);

                // 判断旧密码是否匹配
                if (member.Password == EncryptManager.SHA1(model.oldpassword))
                {
                    // 匹配成功 则更新
                    member.Password = EncryptManager.SHA1(model.newpassword);

                    // 更新
                    if (_memberBLL.Update(member))
                    {
                        return Json("True", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("False", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    // 匹配失败
                    return Json("False", JsonRequestBehavior.AllowGet);
                }
            }
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            if (System.Web.HttpContext.Current.Session["admin"] != null)
            {
                System.Web.HttpContext.Current.Session["admin"] = null;
            }

            if (System.Web.HttpContext.Current.Session["member"] != null)
            {
                System.Web.HttpContext.Current.Session["member"] = null;
            }

            return RedirectToAction("Login");
        }
    }
}