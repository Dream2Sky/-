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
    public class FollowController:Controller
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
    }
}