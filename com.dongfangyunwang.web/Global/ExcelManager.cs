using com.dongfangyunwang.entity;
using com.dongfangyunwang.IBLL;
using com.dongfangyunwang.web.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace com.dongfangyunwang.web.Global
{
    public class ExcelManager
    {
        private IWorkbook _workbook { get; set; }
        private string _filename { get; set; }
        private ISheet _currentSheet { get; set; }

        private IMemberBLL _memberBLL { get; set; }
        private IInformationBLL _informationBLL { get; set; }
        private IFollowBLL _followBLL { get; set; }
        private IFollowRecordBLL _followRecordBLL { get; set; }

        /// <summary>
        /// 不需要提供完整的文件路径
        /// 默认文件路劲为 AppDomain.CurrentDomain.BaseDirectory + File/
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="memberBLL"></param>
        /// <param name="informationBLL"></param>
        /// <param name="followBLL"></param>
        /// <param name="followRecordBLL"></param>
        public ExcelManager(string filename, IMemberBLL memberBLL, IInformationBLL informationBLL, IFollowBLL followBLL, IFollowRecordBLL followRecordBLL)
        {
            _filename = AppDomain.CurrentDomain.BaseDirectory + "/File/" + filename;
            _memberBLL = memberBLL;
            _followBLL = followBLL;
            _followRecordBLL = followRecordBLL;
            _informationBLL = informationBLL;
        }

        /// <summary>
        /// 需要提供完整的文件路径
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="path"></param>
        /// <param name="memberBLL"></param>
        /// <param name="informationBLL"></param>
        /// <param name="followBLL"></param>
        /// <param name="followRecordBLL"></param>
        public ExcelManager(string filename, string path, IMemberBLL memberBLL, IInformationBLL informationBLL, IFollowBLL followBLL, IFollowRecordBLL followRecordBLL)
        {
            _filename = path + filename;
            _memberBLL = memberBLL;
            _informationBLL = informationBLL;
            _followBLL = followBLL;
            _followRecordBLL = followRecordBLL;
        }

        /// <summary>
        /// 不提供文件名 仅仅是实例化 各个BLL
        /// </summary>
        /// <param name="memberBLL"></param>
        /// <param name="informationBLL"></param>
        /// <param name="followBLL"></param>
        /// <param name="followRecordBLL"></param>
        public ExcelManager(IMemberBLL memberBLL, IInformationBLL informationBLL, IFollowBLL followBLL, IFollowRecordBLL followRecordBLL)
        {
            _memberBLL = memberBLL;
            _informationBLL = informationBLL;
            _followBLL = followBLL;
            _followRecordBLL = followRecordBLL;
        }

        /// <summary>
        /// 读取Excel到数据库 耦合度极高 
        /// </summary>
        /// <returns></returns>
        public bool ExcelToDataBase()
        {
            FileStream fs = new FileStream(_filename, FileMode.Open, FileAccess.Read);
            if (_filename.IndexOf(".xlsx") > 0)
            {
                _workbook = new XSSFWorkbook(fs);
            }
            else if (_filename.IndexOf(".xls") > 0)
            {
                _workbook = new HSSFWorkbook(fs);
            }

            // 遍历所有worksheets
            #region 遍历所有worksheets    
            for (int i = 0; i < _workbook.NumberOfSheets; i++)
            {
                _currentSheet = _workbook.GetSheetAt(i);

                // 处理表头
                // 获取表头各项 先导入前18项 
                // 后面的跟进项要跟数据库的Follow表对比 数据库有则直接入库 没有则新建Follow项 再入库

                // 表头
                IRow firstRow = _currentSheet.GetRow(0);

                // 判断数据表的第一行数据是不是为空

                // 如果为空 则跳出循环

                // 如果不为空 则加载数据
                if (firstRow == null)
                {
                    break;
                }


                // 总列数
                int columnCount = firstRow.LastCellNum;

                // 获取当前的跟进项列表
                List<Follow> currentFollowList = _followBLL.GetAllFollow().ToList();

                for (int k = 21; k < columnCount; k++)
                {
                    if (currentFollowList.SingleOrDefault(n => n.FollowItem == firstRow.GetCell(k).StringCellValue) == null)
                    {
                        Follow follow = new Follow();
                        follow.Id = Guid.NewGuid();
                        follow.FollowItem = firstRow.GetCell(k).StringCellValue;

                        _followBLL.Add(follow);
                    }
                }

                // 刷新当前跟进项的纪录
                currentFollowList = _followBLL.GetAllFollow().ToList();

                // 先处理前18列  前18列是固定列
                // 时间 收集员 客户姓名 性别 年龄 婚否 子女 电话 qq 微信 邮箱 地址省市 所有行业 职业 年收入 爱好 是否有车 是否有房
                for (int j = 1; j < _currentSheet.LastRowNum + 1; j++)
                {

                    Information infor = new Information();
                    IRow _row = _currentSheet.GetRow(j);
                    if (string.IsNullOrEmpty(_row.GetCell(1).StringCellValue.ToString()))
                    {
                        break;
                    }
                    // 构造Information
                    #region 构造Information
                    infor.Id = Guid.NewGuid();
                    try
                    {
                        infor.InserTime = DateTime.Parse(_row.GetCell(0).DateCellValue.ToString()).ToString("yyyy-mm-dd");
                    }
                    catch (Exception)
                    {
                        infor.InserTime = DateTime.Parse(_row.GetCell(0).StringCellValue.ToString()).ToString("yyyy-mm-dd");
                    }
                    
                    
                    Member member = _memberBLL.GetMemberByAccount(_row.GetCell(1).StringCellValue);
                    if (member == null)
                    {
                        return false;
                    }
                    else
                    {
                        infor.MemberId = Guid.Parse(member.Id.ToString());
                    }
                    infor.CustomerName = _row.GetCell(2).StringCellValue;
                    infor.Sex = _row.GetCell(3).StringCellValue;
                    try
                    {
                        infor.Age = _row.GetCell(4).NumericCellValue.ToString();
                    }
                    catch (Exception)
                    {
                        infor.Age = _row.GetCell(4).StringCellValue.ToString();
                    }
                    
                    infor.IsMarry = _row.GetCell(5).StringCellValue;
                    infor.Children = _row.GetCell(6).StringCellValue;
                    infor.Phone = _row.GetCell(7).StringCellValue;
                    try
                    {
                        infor.QQ = _row.GetCell(8).NumericCellValue.ToString();
                    }
                    catch (Exception)
                    {
                        infor.QQ = _row.GetCell(8).StringCellValue.ToString();
                    }
                    
                    infor.WebCat = _row.GetCell(9).StringCellValue;
                    infor.Email = _row.GetCell(10).StringCellValue;
                    infor.Address = _row.GetCell(11).StringCellValue;
                    infor.Industry = _row.GetCell(12).StringCellValue;
                    infor.Occupation = _row.GetCell(13).StringCellValue;

                    try
                    {
                        infor.Income = _row.GetCell(14).NumericCellValue.ToString();
                    }
                    catch (Exception)
                    {
                        infor.Income = _row.GetCell(14).StringCellValue.ToString();
                    }
                    
                    infor.Hobby = _row.GetCell(15).StringCellValue;
                    infor.HasCar = _row.GetCell(16).StringCellValue;
                    infor.HasHouse = _row.GetCell(17).StringCellValue;
                    infor.Note1 = _row.GetCell(18).StringCellValue;
                    infor.Note2 = _row.GetCell(19).StringCellValue;
                    infor.Note3 = _row.GetCell(20).StringCellValue;
                    #endregion

                    // 添加Information
                    #region 添加Information

                    if (_informationBLL.Add(infor))
                    {
                        // 添加成功
                        for (int k = 21; k < columnCount; k++)
                        {
                            // 循环剩下的跟进项列
                            // 构造每个FollowRecord
                            FollowRecord fr = new FollowRecord();
                            fr.Id = Guid.NewGuid();
                            fr.InforId = infor.Id;
                            fr.FollowId = _followBLL.GetFollow(firstRow.GetCell(k).StringCellValue).Id;
                            fr.FollowValue = _row.GetCell(k).StringCellValue;

                            try
                            {
                                // 添加新的FollowRecord
                                _followRecordBLL.Add(fr);
                            }
                            catch (Exception ex)
                            {
                                LogHelper.Log.Write(ex.Message);
                                LogHelper.Log.Write(ex.StackTrace);

                                //释放内存
                                fs.Dispose();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        // 添加Information 失败
                        #region 失败后的代码

                        // 释放内存
                        fs.Dispose();

                        return false;
                        #endregion
                    }
                    #endregion 
                }
            }
            #endregion

            // 如果什么都正常 则返回True
            return true;
        }

        /// <summary>
        /// 把内存数据导出到Excel表
        /// </summary>
        /// <param name="informationList"></param>
        /// <returns></returns>
        public MemoryStream DataToExcel(List<InformationModel> informationList)
        {
            _workbook = new HSSFWorkbook();
            _currentSheet = _workbook.CreateSheet("Sheet1");
            IRow firstRow = _currentSheet.CreateRow(0);

            // 构造表头
            #region 构造表头
            firstRow.CreateCell(0).SetCellValue("录入时间");
            firstRow.CreateCell(1).SetCellValue("收集员");
            firstRow.CreateCell(2).SetCellValue("客户姓名");
            firstRow.CreateCell(3).SetCellValue("性别");
            firstRow.CreateCell(4).SetCellValue("年龄");
            firstRow.CreateCell(5).SetCellValue("婚否");
            firstRow.CreateCell(6).SetCellValue("子女");
            firstRow.CreateCell(7).SetCellValue("电话");
            firstRow.CreateCell(8).SetCellValue("QQ");
            firstRow.CreateCell(9).SetCellValue("微信");
            firstRow.CreateCell(10).SetCellValue("邮箱");
            firstRow.CreateCell(11).SetCellValue("地址省市");
            firstRow.CreateCell(12).SetCellValue("所属行业");
            firstRow.CreateCell(13).SetCellValue("职位");
            firstRow.CreateCell(14).SetCellValue("年收入");
            firstRow.CreateCell(15).SetCellValue("爱好");
            firstRow.CreateCell(16).SetCellValue("是否有车");
            firstRow.CreateCell(17).SetCellValue("是否有房");
            firstRow.CreateCell(18).SetCellValue("备注1");
            firstRow.CreateCell(19).SetCellValue("备注2");
            firstRow.CreateCell(20).SetCellValue("备注3");

            List<Follow> followList = _followBLL.GetAllFollow().ToList();

            for (int i = 0; i < followList.Count; i++)
            {
                firstRow.CreateCell(21 + i).SetCellValue(followList[i].FollowItem);
            }
            #endregion

            // 遍历数据列表
            #region 填表

            for (int i = 0; i < informationList.Count; i++)
            {
                IRow row = _currentSheet.CreateRow(i + 1);

                row.CreateCell(0).SetCellValue(informationList[i].InserTime.ToString());
                row.CreateCell(1).SetCellValue(informationList[i].MemberAccount);
                row.CreateCell(2).SetCellValue(informationList[i].CustomerName);
                row.CreateCell(3).SetCellValue(informationList[i].Sex);
                row.CreateCell(4).SetCellValue(informationList[i].Age);
                row.CreateCell(5).SetCellValue(informationList[i].IsMarry);
                row.CreateCell(6).SetCellValue(informationList[i].Children);
                row.CreateCell(7).SetCellValue(informationList[i].Phone);
                row.CreateCell(8).SetCellValue(informationList[i].QQ);
                row.CreateCell(9).SetCellValue(informationList[i].WebCat);
                row.CreateCell(10).SetCellValue(informationList[i].Email);
                row.CreateCell(11).SetCellValue(informationList[i].Address);
                row.CreateCell(12).SetCellValue(informationList[i].Industry);
                row.CreateCell(13).SetCellValue(informationList[i].Occupation);
                row.CreateCell(14).SetCellValue(informationList[i].Income);
                row.CreateCell(15).SetCellValue(informationList[i].Hobby);
                row.CreateCell(16).SetCellValue(informationList[i].HasCar);
                row.CreateCell(17).SetCellValue(informationList[i].HasHouse);
                row.CreateCell(18).SetCellValue(informationList[i].Hobby);
                row.CreateCell(19).SetCellValue(informationList[i].HasCar);
                row.CreateCell(20).SetCellValue(informationList[i].HasHouse);

                for (int j = 0; j < followList.Count; j++)
                {
                    FollowModel fm = informationList[i].FollowList.Where(n => n.FollowName == followList[j].FollowItem).SingleOrDefault();
                    if (fm == null)
                    {
                        row.CreateCell(21 + j).SetCellValue("");
                    }
                    else
                    {
                        row.CreateCell(21 + j).SetCellValue(fm.FollowValue);
                    }
                }
            }

            #endregion

            MemoryStream ms = new MemoryStream();

            try
            {
                // 把 workbook 写到内存流里面
                _workbook.Write(ms);

                // 设置内存流的读取偏移量
                ms.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }

            return ms;
        }
    }
}