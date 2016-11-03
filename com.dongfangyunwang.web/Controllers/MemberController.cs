using com.dongfangyunwang.entity;
using com.dongfangyunwang.IBLL;
using com.dongfangyunwang.web.Global;
using com.jiechengbao.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.dongfangyunwang.web.Controllers
{
    [IsAdmin]
    public class MemberController : Controller
    {
        private IMemberBLL _memberBLL;
        public MemberController(IMemberBLL memberBLL)
        {
            _memberBLL = memberBLL;
        }

        public ActionResult List()
        {
            ViewData["MemberList"] = _memberBLL.GetAllMembers().Where(n=>n.IsAdmin != 1).OrderBy(n => n.IsAdmin);
            return View();
        }

        [HttpPost]
        public ActionResult Add(string memberName, string isadmin)
        {
            Member member = new Member();
            member.Id = Guid.NewGuid();
            member.Account = memberName;
            member.Password = EncryptManager.SHA1("123456");
            if (isadmin == "g")
            {
                member.IsAdmin = 2;
            }
            else
            {
                member.IsAdmin = 0;
            }
            if (_memberBLL.IsExist(member))
            {
                return View();
            }
            else
            {
                _memberBLL.Add(member);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Update(string memberId, string memberName, string isAdmin)
        {
            bool res = false;

            Member member = new Member();
            member = _memberBLL.GetMemberById(Guid.Parse(memberId));

            member.Account = memberName;
            member.IsAdmin = isAdmin == "g" ? 2 : 0;

            using (DFYW_DbContext db = new DFYW_DbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Set<Member>().Attach(member);
                        db.Entry(member).State = System.Data.Entity.EntityState.Modified;

                        db.SaveChanges();

                        trans.Commit();

                        res = true;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Log.Write(ex.Message);
                        LogHelper.Log.Write(ex.StackTrace);

                        trans.Rollback();

                        res = false;
                    }
                }
            }

            var obj = new
            {
                res = res,
                member = new
                {
                    memberId = memberId,
                    memberName = memberName,
                    isadmin = isAdmin
                }
            };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(string memberId)
        {
            bool res = false;
            Member member = new Member();
            member = _memberBLL.GetMemberById(Guid.Parse(memberId));
            
            using (DFYW_DbContext db = new DFYW_DbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Set<Member>().Attach(member);
                        db.Entry(member).State = System.Data.Entity.EntityState.Deleted;

                        db.SaveChanges();

                        trans.Commit();
                        res = true;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Log.Write(ex.Message);
                        LogHelper.Log.Write(ex.StackTrace);

                        trans.Rollback();

                        res = false;
                    }
                }
            }

            var obj = new
            {
                res = res
            };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}