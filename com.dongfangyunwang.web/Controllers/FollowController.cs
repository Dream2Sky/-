using com.dongfangyunwang.entity;
using com.dongfangyunwang.IBLL;
using com.dongfangyunwang.web.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.dongfangyunwang.web.Controllers
{
    [IsAdmin]
    public class FollowController : Controller
    {
        private IFollowBLL _followBLL;
        public FollowController(IFollowBLL followBLL)
        {
            _followBLL = followBLL;
        }

        public ActionResult Items()
        {
            ViewData["Follow"] = _followBLL.GetAllFollow();
            return View();
        }

        [HttpPost]
        public ActionResult Add(string followitem)
        {
            Follow follow = new Follow();
            follow.Id = Guid.NewGuid();
            follow.FollowItem = followitem;

            try
            {
                if (!_followBLL.IsExist(follow))
                {
                    _followBLL.Add(follow);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }

            return RedirectToAction("Items");
        }

        [HttpPost]
        public ActionResult Update(string followId, string followName)
        {
            Follow follow = new Follow();
            follow = _followBLL.GetFollow(Guid.Parse(followId));
            follow.FollowItem = followName;

            bool res = false;

            using (DFYW_DbContext db = new DFYW_DbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Set<Follow>().Attach(follow);
                        db.Entry<Follow>(follow).State = System.Data.Entity.EntityState.Modified;

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
                follow = new
                {
                    Id = followId,
                    Name = followName
                }
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string followId)
        {
            bool res = false;
            Follow follow = _followBLL.GetFollow(Guid.Parse(followId));

            using (DFYW_DbContext db =new DFYW_DbContext ())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Set<Follow>().Attach(follow);
                        db.Entry<Follow>(follow).State = System.Data.Entity.EntityState.Deleted;

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
            };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

    }
}