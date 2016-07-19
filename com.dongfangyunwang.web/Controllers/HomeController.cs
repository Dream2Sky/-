using com.dongfangyunwang.entity;
using com.dongfangyunwang.IBLL;
using com.dongfangyunwang.web.Global;
using com.dongfangyunwang.web.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            return View();
        }

        //[HttpPost]
        //public ActionResult Index(string condition)
        //{
        //    ViewData["TableHeaderItem"] = GetFollowHeader();
        //    ViewData["InformationList"] = GetInformation(condition);
        //    return View();
        //}

        /// <summary>
        /// 搜索 string 条件
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult Search(string condition)
        {
            ViewData["TableHeaderItem"] = GetFollowHeader();

            List<InformationModel> informationList = GetInformation(condition);
            ViewData["InformationList"] = informationList;

            System.Web.HttpContext.Current.Session["InformationList"] = informationList; // 存储查询结果  当作缓存数据  当导出数据时 可以用

            return PartialView();
        }

        /// <summary>
        /// 搜索 AdditionalConditionModel 条件
        /// </summary>
        /// <param name="additionalConditionModel"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult Search(AdditionalConditionModel additionalConditionModel)
        {
            ViewData["TableHeaderItem"] = GetFollowHeader();

            List<InformationModel> informationList = GetInformation(additionalConditionModel);
            ViewData["InformationList"] = informationList;

            System.Web.HttpContext.Current.Session["InformationList"] = informationList;
            return PartialView();
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

        /// <summary>
        ///  额外条件查询
        /// </summary>
        /// <param name="additionalCondition"></param>
        /// <returns></returns>
        [NonAction]
        public List<InformationModel> GetInformation(AdditionalConditionModel additionalCondition)
        {
            // InformationModel List 最终提交到页面的数据集
            // InformationModel 继承至 Information
            // 目的是为了包含FollowModel List
            List<InformationModel> InformationModelList = new List<InformationModel>();
            List<Information> InformationList = new List<Information>();

            if (additionalCondition == null)
            {
                // 如果condition没有值
                // 首先获取前50条数据记录到 InformationList
                InformationList = _informationBLL.GetinformationLimitedwithSpecificMember(50, System.Web.HttpContext.Current.Session["member"].ToString()).ToList();
            }
            else
            {
                // 如果condition有值 就按条件查询
                string sex = additionalCondition.sex == null ? "" : additionalCondition.sex;
                string minage = additionalCondition.min_age == null ? "" : additionalCondition.min_age;
                string maxage = additionalCondition.max_age == null ? "" : additionalCondition.max_age;
                string ismarried = additionalCondition.ismarried == null ? "" : additionalCondition.ismarried;
                string children = additionalCondition.children == null ? "" : additionalCondition.children;
                string minincome = additionalCondition.min_income == null ? "" : additionalCondition.min_income;
                string maxincome = additionalCondition.max_income == null ? "" : additionalCondition.max_income;
                string hascar = additionalCondition.hascar == null ? "" : additionalCondition.hascar;
                string hashouse = additionalCondition.hashouse == null ? "" : additionalCondition.hashouse;
                string insertTime = additionalCondition.insertTime == null ? "" : additionalCondition.insertTime;

                InformationList = _informationBLL.GetInformationByAnythingswithSpecificMember(System.Web.HttpContext.Current.Session["member"].ToString(), sex, minage, maxage, ismarried, children, minincome, maxincome, hascar, hashouse, insertTime).ToList();
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

        /// <summary>
        /// 导出数据到Excel表 导出的excel表为 .xls 格式 
        /// </summary>
        /// <returns></returns>
        public FileResult ExportData()
        {
            ExcelManager em = new ExcelManager(_memberBLL, _informationBLL, _followBLL, _followRecordBLL);
            MemoryStream ms = em.DataToExcel(System.Web.HttpContext.Current.Session["InformationList"] as List<InformationModel>);
            return File(ms, "application/vnd.ms-excel", Guid.NewGuid().ToString() + ".xls");
        }
    }
}