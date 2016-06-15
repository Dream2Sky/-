﻿using com.dongfangyunwang.entity;
using com.dongfangyunwang.IBLL;
using com.dongfangyunwang.web.Global;
using com.dongfangyunwang.web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Coffee.ImportData2.Web.Models;
using System.Data;
using com.jiechengbao.common;

namespace com.dongfangyunwang.web.Controllers
{
    public class InformationController : Controller
    {
        private IMemberBLL _memberBLL;
        private IFollowBLL _followBLL;
        private IFollowRecordBLL _followRecordBLL;
        private IInformationBLL _informationBLL;
        public InformationController(IMemberBLL memberBLL, IFollowBLL followBLL, IFollowRecordBLL followRecordBLL, IInformationBLL informationBLL)
        {
            _memberBLL = memberBLL;
            _followBLL = followBLL;
            _followRecordBLL = followRecordBLL;
            _informationBLL = informationBLL;
        }

        [IsAdmin]
        public ActionResult ImportInformation()
        {
            List<Follow> FollowList = _followBLL.GetAllFollow().ToList();
            ViewData["FollowItems"] = FollowList;
            ViewBag.Count = FollowList.Count;
            return View();
        }

        [HttpPost]
        [IsAdmin]
        public ActionResult ImportData()
        {
            if (Request.IsAjaxRequest())
            {
                var stream = HttpContext.Request.InputStream;
                string json = new StreamReader(stream).ReadToEnd();

                try
                {
                    //JArray jarray = (JArray)JsonConvert.DeserializeObject(json);
                    List<KeyValuePair<string, string>> kvList = JsonToList(json);
                    Information infor = new Information();

                    #region 给information赋值

                    infor.Address = kvList.Where(n => n.Key == "address").SingleOrDefault().Value;
                    infor.Age = kvList.SingleOrDefault(n => n.Key == "age").Value;
                    infor.Children = kvList.SingleOrDefault(n => n.Key == "children").Value;
                    infor.CustomerName = kvList.SingleOrDefault(n => n.Key == "customerName").Value;
                    infor.Email = kvList.SingleOrDefault(n => n.Key == "email").Value;
                    infor.HasCar = kvList.SingleOrDefault(n => n.Key == "hascar").Value;
                    infor.HasHouse = kvList.SingleOrDefault(n => n.Key == "hashouse").Value;
                    infor.Hobby = kvList.SingleOrDefault(n => n.Key == "hobby").Value;
                    infor.Id = Guid.NewGuid();
                    infor.Income = kvList.SingleOrDefault(n => n.Key == "income").Value;
                    infor.Industry = kvList.SingleOrDefault(n => n.Key == "industry").Value;
                    infor.InserTime = DateTime.Now.ToString("yyyy-mm-dd");
                    infor.IsMarry = kvList.SingleOrDefault(n => n.Key == "ismarry").Value;
                    infor.MemberId = _memberBLL.GetMemberByAccount(System.Web.HttpContext.Current.Session["Admin"].ToString(), true).Id;
                    infor.Occupation = kvList.SingleOrDefault(n => n.Key == "occupation").Value;
                    infor.Phone = kvList.SingleOrDefault(n => n.Key == "phone").Value;
                    infor.QQ = kvList.SingleOrDefault(n => n.Key == "qq").Value;
                    infor.Sex = kvList.SingleOrDefault(n => n.Key == "sex").Value;
                    infor.WebCat = kvList.SingleOrDefault(n => n.Key == "webcat").Value;

                    #endregion

                    #region 添加FollowRecord
                    if (_informationBLL.Add(infor))
                    {
                        List<Follow> followList = _followBLL.GetAllFollow().ToList();
                        foreach (var item in followList)
                        {
                            FollowRecord fr = new FollowRecord();
                            fr.FollowId = _followBLL.GetFollow(item.FollowItem).Id;
                            fr.InforId = infor.Id;
                            fr.Id = Guid.NewGuid();
                            fr.FollowValue = kvList.SingleOrDefault(n => n.Key == item.FollowItem).Value;

                            _followRecordBLL.Add(fr);
                        }

                        return Json("True", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("False", JsonRequestBehavior.AllowGet);

                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    LogHelper.Log.Write(ex.Message);
                    LogHelper.Log.Write(ex.StackTrace);

                    return Json("False", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            } 
        }

        #region 注释
        //[HttpPost]
        //[IsAdmin]
        //public ActionResult ImportData()
        //{
        //    List<Follow> FollowList = _followBLL.GetAllFollow().ToList();
        //    List<KeyValuePair<string, string>> followSubmit = new List<KeyValuePair<string, string>>();
        //    ViewBag.Count = FollowList.Count;
        //    #region 获取提交的字段
        //    var customerName = Request.QueryString["customerName"].ToString();
        //    var sex = Request.QueryString["sex"].ToString();
        //    var age = Request.QueryString["age"].ToString();
        //    var ismarry = Request.QueryString["ismarry"].ToString();
        //    var children = Request.QueryString["children"].ToString();
        //    var address = Request.QueryString["address"].ToString();
        //    var industry = Request.QueryString["industry"].ToString();
        //    var occupation = Request.QueryString["occupation"].ToString();
        //    var income = Request.QueryString["income"].ToString();
        //    var hobby = Request.QueryString["hobby"].ToString();
        //    var phone = Request.QueryString["phone"].ToString();
        //    var webcat = Request.QueryString["webcat"].ToString();
        //    var email = Request.QueryString["email"].ToString();
        //    var hascar = Request.QueryString["hascar"].ToString();
        //    var hashouse = Request.QueryString["hashouse"].ToString();
        //    #endregion

        //    foreach (var item in FollowList)
        //    {
        //        KeyValuePair<string, string> k = new KeyValuePair<string, string>(item.FollowItem, Request.QueryString[item.FollowItem]);
        //        followSubmit.Add(k);
        //    }

        //    #region 构造information
        //    Information info = new Information();
        //    info.Id = Guid.NewGuid();
        //    info.Address = address;
        //    info.Age = age;
        //    info.Children = children;
        //    info.CustomerName = customerName;
        //    info.Email = email;
        //    info.HasCar = hascar;
        //    info.HasHouse = hashouse;
        //    info.Hobby = hobby;
        //    info.Income = income;
        //    info.Industry = industry;
        //    info.InserTime = DateTime.Now.ToString();
        //    info.WebCat = webcat;
        //    info.MemberId = _memberBLL.GetMemberByAccount(System.Web.HttpContext.Current.Session["admin"].ToString(), true).Id;
        //    #endregion


        //    // 添加一条information 纪录
        //    if (_informationBLL.Add(info))
        //    {
        //        // 如果添加成功
        //        // 添加相应的 followrecord
        //        foreach (var item in followSubmit)
        //        {
        //            FollowRecord fr = new FollowRecord();

        //            // informationId
        //            fr.InforId = info.Id;

        //            // 获取followId
        //            fr.FollowId = _followBLL.GetFollow(item.Key).Id;

        //            fr.FollowValue = item.Value;

        //            _followRecordBLL.Add(fr);
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("")
        //    }


        //    return View();
        //}
        #endregion
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="fileCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase[] fileCollection)
        {
            if (fileCollection.First() == null)
            {
                return Json("False", JsonRequestBehavior.AllowGet);
            }

            List<string> fileList = new List<string>();

            #region 保存文件
            try
            {
                foreach (HttpPostedFileBase file in fileCollection)
                {
                    fileList.Add(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/File"), System.IO.Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write("Failed to save files");
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return Json("Fale", JsonRequestBehavior.AllowGet);
            }
            #endregion

            // 传入文件名列表 filelist
            if (GetDataFromExcel(fileList))
            {
                DeleteFile(fileList);
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            else
            {
                DeleteFile(fileList);
                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 一般用户的数据导入
        /// </summary>
        /// <returns></returns>
        [IsLogin]
        public ActionResult ImportInformationwithSpecificMember()
        {
            return View();
        }

        /// <summary>
        /// 把json字符串转换成List<keyvaluepair<>>
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        [NonAction]
        protected List<KeyValuePair<string, string>> JsonToList(string jsonStr)
        {
            try
            {
                List<KeyValuePair<string, string>> kvList = new List<KeyValuePair<string, string>>();
                jsonStr = jsonStr.Replace("{", "").Replace("}", "").Replace("\"", "");
                string[] keyvalueArray = jsonStr.Split(',');
                foreach (var item in keyvalueArray)
                {
                    string[] keyvalue = item.Split(':');
                    KeyValuePair<string, string> kv = new KeyValuePair<string, string>(keyvalue[0], keyvalue[1]);

                    kvList.Add(kv);
                }

                return kvList;

            }
            catch (Exception ex)
            {
                LogHelper.Log.Write("Failed to change type List<KeyValuePair<string,string>> from type string");
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 获取 AppDomain.CurrentDomain.BaseDirectory 下的File文件夹里的所有文件
        /// </summary>
        /// <returns></returns>
        [NonAction]
        protected List<FileInfo> GetFiles()
        {
            return new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "/File").GetFiles().ToList();
        }

        /// <summary>
        /// 删除AppDomain.CurrentDomain.BaseDirectory目录下的File文件夹下的指定文件
        /// </summary>
        /// <param name="fileName"></param>
        [NonAction]
        protected void DeleteFile(string fileName)
        {
            try
            {
                FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "/File/" + fileName);
                fi.Delete();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }

        }

        /// <summary>
        /// 删除指定文件列表内的所有文件
        /// </summary>
        /// <param name="fileList"></param>
        [NonAction]
        protected void DeleteFile(List<string> fileList)
        {
            try
            {
                foreach (var item in fileList)
                {
                    DeleteFile(item);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write("删除文件失败");
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }

        }

        /// <summary>
        ///  从多个excel表里导出数据到数据库
        /// </summary>
        /// <param name="fileList"></param>
        /// <returns></returns>
        [NonAction]
        protected bool GetDataFromExcel(List<string> fileList)
        {
            bool result = false;
            foreach (string item in fileList)
            {
                ExcelManager em = new ExcelManager(item, _memberBLL, _informationBLL, _followBLL, _followRecordBLL);
                if (em.ExcelToDataBase())
                {
                    result = true;
                }
                else
                {
                    return result;
                }
            }

            return result;
        }


    }
}