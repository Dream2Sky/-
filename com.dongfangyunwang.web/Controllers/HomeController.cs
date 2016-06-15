using com.dongfangyunwang.entity;
using com.dongfangyunwang.IBLL;
using com.dongfangyunwang.web.Global;
using com.dongfangyunwang.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.dongfangyunwang.web.Controllers
{
    [IsLogin]
    public class HomeController:Controller
    {
        private IMemberBLL _memberBLL;
        private IInformationBLL _informationBLL;
        private IFollowBLL _followBLL;
        private IFollowRecordBLL _followRecordBLL;

        public HomeController(IMemberBLL memberBLL, IInformationBLL informationBLL, IFollowBLL followBLL, IFollowRecordBLL followRecordBLL)
        {
            _memberBLL = memberBLL;
            _informationBLL = informationBLL;
            _followBLL = followBLL;
            _followRecordBLL = followRecordBLL;
        }

        public ActionResult Index()
        {
            ViewData["TableHeaderItem"] = GetFollowHeader();
            ViewData["InformationList"] = GetInformation("");
            return View();
        }

        [HttpPost]
        public ActionResult Index(string condition)
        {
            ViewData["TableHeaderItem"] = GetFollowHeader();
            ViewData["InformationList"] = GetInformation(condition);
            return View();
        }

        /// <summary>
        /// 构造跟进项表头
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public List<string> GetFollowHeader()
        {
            // 构造表头 即添加跟进项
            List<string> baseTableItem = new List<string>();

            IEnumerable<Follow> followItems = _followBLL.GetAllFollow();
            foreach (Follow item in followItems)
            {
                baseTableItem.Add(item.FollowItem);
            }

            return baseTableItem;
        }

        /// <summary>
        /// 获取Information
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [NonAction]
        public List<InformationModel> GetInformation(string condition)
        {
            // InformationModel List 最终提交到页面的数据集
            // InformationModel 继承至 Information
            // 目的是为了包含FollowModel List
            List<InformationModel> InformationModelList = new List<InformationModel>();
            List<Information> InformationList = new List<Information>();

            if (string.IsNullOrEmpty(condition))
            {
                // 如果condition没有值
                // 首先获取前50条数据记录到 InformationList
                InformationList = _informationBLL.GetinformationLimitedwithSpecificMember(50, System.Web.HttpContext.Current.Session["member"].ToString()).ToList();
            }
            else
            {
                // 如果condition有值 就按条件查询
                InformationList = _informationBLL.GetInformationByAnythingswithSpecificMember(condition,System.Web.HttpContext.Current.Session["member"].ToString()).ToList();
            }

            List<Information> InfoList = new List<Information>();
            InfoList = InformationList.ToList();

            // 然后循环InformationList
            // 构造 InformationModelList
            foreach (Information item in InformationList)
            {
                InformationModel info = new InformationModel(item);

                List<FollowModel> followModelList = new List<FollowModel>();

                List<FollowRecord> followRecordList = new List<FollowRecord>();
                followRecordList = _followRecordBLL.GetFollowRecordByInformationId(info.Id).ToList();

                foreach (FollowRecord fr in followRecordList)
                {
                    FollowModel fm = new FollowModel();
                    fm.FollowName = _followBLL.GetFollow(fr.FollowId).FollowItem;
                    fm.FollowValue = fr.FollowValue;

                    followModelList.Add(fm);
                }

                info.FollowList = followModelList.AsEnumerable();
                // 获取收集员的账号
                info.MemberAccount = _memberBLL.GetMemberById(info.MemberId).Account;
                InformationModelList.Add(info);
            }

            return InformationModelList;
        }
    }
}