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
    public class GroupController:Controller
    {
        private IMemberBLL _memberBLL;
        private IInformationBLL _informationBLL;
        private IFollowBLL _followBLL;
        private IFollowRecordBLL _followRecordBLL;
        public GroupController(IMemberBLL memberBLL, 
            IInformationBLL informationBLL, IFollowBLL followBLL,
            IFollowRecordBLL followRecordBLL)
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

        [HttpGet]
        public PartialViewResult Search(string condition)
        {
            Member admin = _memberBLL.GetAdmin();
            ViewData["TableHeaderItem"] = GetFollowHeader();

            List<InformationModel> infoList = GetInformation(condition, admin.Id);
            ViewData["InformationList"] = infoList;

            System.Web.HttpContext.Current.Session["InformationList"] = infoList; // 存储查询结果  当作缓存数据  当导出数据时 可以用

            return PartialView();
        }

        [HttpPost]
        public PartialViewResult Search(AdditionalConditionModel additionalConditionModel)
        {
            ViewData["TableHeaderItem"] = GetFollowHeader();
            Member admin = _memberBLL.GetAdmin();

            List<InformationModel> informationList = GetInformation(additionalConditionModel, admin.Id);
            ViewData["InformationList"] = informationList;

            System.Web.HttpContext.Current.Session["InformationList"] = informationList;

            return PartialView();
        }

        /// <summary>
        /// 组长 审批
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Approval(Guid Id)
        {
            if (Id == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
            // 更新 客户资料的状态
            Information infor = _informationBLL.GetInformationById(Id);
            infor.Approval = true;

            if (_informationBLL.Update(infor))
            {
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 组长获取客户资料
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [NonAction]
        public List<InformationModel> GetInformation(string condition, Guid adminId)
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
                InformationList = _informationBLL.GetInformationByAnythingswithGroupLeader(50, adminId).ToList();
            }
            else
            {
                // 如果condition有值 就按条件查询
                InformationList = _informationBLL.GetInformationByAnythingswithGroupLeader(condition, adminId).ToList();
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
        /// 组长 按条件 查询客户资料
        /// </summary>
        /// <param name="additionalCondition"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [NonAction]
        public List<InformationModel> GetInformation(AdditionalConditionModel additionalCondition, Guid adminId)
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
                InformationList = _informationBLL.GetInformationByAnythingswithGroupLeader(50,adminId).ToList();
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

                InformationList = _informationBLL.GetInformationByAnythingswithGroupLeader(adminId,sex, minage, maxage, ismarried, children, minincome, maxincome, hascar, hashouse, insertTime).ToList();
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
        /// 获取跟进项表头
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