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
    public class MemberController:Controller
    {
        private IMemberBLL _memberBLL;
        public MemberController(IMemberBLL memberBLL)
        {
            _memberBLL = memberBLL;
        }

        public ActionResult List()
        {
            ViewData["MemberList"] = _memberBLL.GetAllMembers().OrderBy(n=>n.IsAdmin);
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
    }
}