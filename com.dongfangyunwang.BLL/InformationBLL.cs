using com.dongfangyunwang.IBLL;
using com.dongfangyunwang.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.dongfangyunwang.entity;

namespace com.dongfangyunwang.BLL
{
    public class InformationBLL : IInformationBLL
    {
        private IInformationDAL _informationDAL;
        private IMemberDAL _memberDAL;
        public InformationBLL(IInformationDAL informationDAL, IMemberDAL memberDAL)
        {
            _informationDAL = informationDAL;
            _memberDAL = memberDAL;
        }

        /// <summary>
        /// 添加一条Information
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Add(Information info)
        {
            try
            {
                return _informationDAL.Insert(info);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return false;
            }
        }

        /// <summary>
        /// 模糊查询 返回Information list
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<Information> GetInformationByAnythings(string condition, Guid adminId)
        {
            try
            {
                List<InformationNoEntity> entityList = new List<InformationNoEntity>();
                entityList = _informationDAL.SelectByAnythings(condition, adminId);

                List<Information> InforList = new List<Information>();

                foreach (var item in entityList)
                {
                    Information info = new Information();
                    info.Id = item.Id;
                    info.Address = item.Address;
                    info.Age = item.Age;
                    info.Children = item.Children;
                    info.CustomerName = item.CustomerName;
                    info.Email = item.Email;
                    info.HasCar = item.HasCar;
                    info.HasHouse = item.HasHouse;
                    info.Hobby = item.Hobby;
                    info.Income = item.Income;
                    info.Industry = item.Industry;
                    info.InserTime = item.InserTime;
                    info.IsMarry = item.IsMarry;
                    info.MemberId = item.MemberId;
                    info.Occupation = item.Occupation;
                    info.Phone = item.Phone;
                    info.QQ = item.QQ;
                    info.Sex = item.Sex;
                    info.WebCat = item.WebCat;
                    info.Note1 = item.Note1;
                    info.Note2 = item.Note2;
                    info.Note3 = item.Note3;
                    info.Approval = item.Approval;
                    InforList.Add(info);
                }

                return InforList;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return null;
            }
        }

        public IEnumerable<Information> GetInformationByAnythings(Guid adminId, string sex, string min_age, string max_age, string ismarried, string children, string min_income, string max_income, string hascar, string hashouse, string insertTime)
        {
            try
            {
                List<InformationNoEntity> entityList = new List<InformationNoEntity>();
                entityList = _informationDAL.SelectByAnythings(adminId,sex, min_age, max_age, ismarried, children, min_income, max_income, hascar, hashouse, insertTime);
                List<Information> InforList = new List<Information>();

                foreach (var item in entityList)
                {
                    Information info = new Information();
                    info.Id = item.Id;
                    info.Address = item.Address;
                    info.Age = item.Age;
                    info.Children = item.Children;
                    info.CustomerName = item.CustomerName;
                    info.Email = item.Email;
                    info.HasCar = item.HasCar;
                    info.HasHouse = item.HasHouse;
                    info.Hobby = item.Hobby;
                    info.Income = item.Income;
                    info.Industry = item.Industry;
                    info.InserTime = item.InserTime;
                    info.IsMarry = item.IsMarry;
                    info.MemberId = item.MemberId;
                    info.Occupation = item.Occupation;
                    info.Phone = item.Phone;
                    info.QQ = item.QQ;
                    info.Sex = item.Sex;
                    info.WebCat = item.WebCat;
                    info.Note1 = item.Note1;
                    info.Note2 = item.Note2;
                    info.Note3 = item.Note3;
                    InforList.Add(info);
                }

                return InforList;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return null;
            }
        }

        /// <summary>
        /// 组长 前count条记录
        /// </summary>
        /// <param name="count"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public IEnumerable<Information> GetInformationByAnythingswithGroupLeader(int count, Guid adminId)
        {
            return _informationDAL.SelectByAnythingswithGroupLeader(count, adminId);
        }

        /// <summary>
        /// 组长 任意条件查询
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public IEnumerable<Information> GetInformationByAnythingswithGroupLeader(string condition, Guid adminId)
        {
            try
            {
                List<InformationNoEntity> entityList = new List<InformationNoEntity>();
                // 找出 不是 管理员录入的所有客户资料
                entityList = _informationDAL.SelectByAnythingswithGroupLeader(condition, adminId);

                List<Information> InforList = new List<Information>();

                foreach (var item in entityList)
                {
                    Information info = new Information();
                    info.Id = item.Id;
                    info.Address = item.Address;
                    info.Age = item.Age;
                    info.Children = item.Children;
                    info.CustomerName = item.CustomerName;
                    info.Email = item.Email;
                    info.HasCar = item.HasCar;
                    info.HasHouse = item.HasHouse;
                    info.Hobby = item.Hobby;
                    info.Income = item.Income;
                    info.Industry = item.Industry;
                    info.InserTime = item.InserTime;
                    info.IsMarry = item.IsMarry;
                    info.MemberId = item.MemberId;
                    info.Occupation = item.Occupation;
                    info.Phone = item.Phone;
                    info.QQ = item.QQ;
                    info.Sex = item.Sex;
                    info.WebCat = item.WebCat;
                    info.Note1 = item.Note1;
                    info.Note2 = item.Note2;
                    info.Note3 = item.Note3;
                    info.Approval = item.Approval;
                    InforList.Add(info);
                }

                return InforList;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return null;
            }
        }
       
        /// <summary>
        /// 组长 按条件查询
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="sex"></param>
        /// <param name="min_age"></param>
        /// <param name="max_age"></param>
        /// <param name="ismarried"></param>
        /// <param name="children"></param>
        /// <param name="min_income"></param>
        /// <param name="max_income"></param>
        /// <param name="hascar"></param>
        /// <param name="hashouse"></param>
        /// <param name="insertTime"></param>
        /// <returns></returns>
        public IEnumerable<Information> GetInformationByAnythingswithGroupLeader(Guid adminId, string sex, string min_age, string max_age, string ismarried, string children, string min_income, string max_income, string hascar, string hashouse, string insertTime)
        {
            try
            {
                List<InformationNoEntity> entityList = new List<InformationNoEntity>();
                entityList = _informationDAL.SelectByAnythingswithGroupLeader(sex, min_age, max_age, ismarried, children, min_income, max_income, hascar, hashouse, insertTime, adminId);

                List<Information> informationList = new List<Information>();
                foreach (var item in entityList)
                {
                    Information info = new Information();
                    info.Id = item.Id;
                    info.Address = item.Address;
                    info.Age = item.Age;
                    info.Children = item.Children;
                    info.CustomerName = item.CustomerName;
                    info.Email = item.Email;
                    info.HasCar = item.HasCar;
                    info.HasHouse = item.HasHouse;
                    info.Hobby = item.Hobby;
                    info.Income = item.Income;
                    info.Industry = item.Industry;
                    info.InserTime = item.InserTime;
                    info.IsMarry = item.IsMarry;
                    info.MemberId = item.MemberId;
                    info.Occupation = item.Occupation;
                    info.Phone = item.Phone;
                    info.QQ = item.QQ;
                    info.Sex = item.Sex;
                    info.WebCat = item.WebCat;
                    info.Note1 = item.Note1;
                    info.Note2 = item.Note2;
                    info.Note3 = item.Note3;
                    info.Approval = item.Approval;

                    informationList.Add(info);
                }
                return informationList;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                return null;
            }

        }

        /// <summary>
        /// 根据指定的memberId按条件查询信息
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public IEnumerable<Information> GetInformationByAnythingswithSpecificMember(string condition, Guid memberId)
        {
            try
            {
                List<InformationNoEntity> entityList = new List<InformationNoEntity>();
                entityList = _informationDAL.SelectByAnythingswithSpecificMember(condition, memberId);

                List<Information> InforList = new List<Information>();

                foreach (var item in entityList)
                {
                    Information info = new Information();
                    info.Id = item.Id;
                    info.Address = item.Address;
                    info.Age = item.Age;
                    info.Children = item.Children;
                    info.CustomerName = item.CustomerName;
                    info.Email = item.Email;
                    info.HasCar = item.HasCar;
                    info.HasHouse = item.HasHouse;
                    info.Hobby = item.Hobby;
                    info.Income = item.Income;
                    info.Industry = item.Industry;
                    info.InserTime = item.InserTime;
                    info.IsMarry = item.IsMarry;
                    info.MemberId = item.MemberId;
                    info.Occupation = item.Occupation;
                    info.Phone = item.Phone;
                    info.QQ = item.QQ;
                    info.Sex = item.Sex;
                    info.WebCat = item.WebCat;

                    // 修改需求 information 新添加 note1 note2 note3三列
                    info.Note1 = item.Note1;
                    info.Note2 = item.Note2;
                    info.Note3 = item.Note3;

                    InforList.Add(info);
                }

                return InforList;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return null;
            }
        }

        /// <summary>
        /// 根据指定的用户名 memberAccount 按条件查询信息
        /// 该方法 调用了 GetInformationByAnythingswithSpecificMember(string,guid) 这个重载
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="memberAccount"></param>
        /// <returns></returns>
        public IEnumerable<Information> GetInformationByAnythingswithSpecificMember(string condition, string memberAccount)
        {
            Member member = new Member();
            member = _memberDAL.SelectByAccount(memberAccount,0);

            return this.GetInformationByAnythingswithSpecificMember(condition, member.Id);
        }

        /// <summary>
        /// 带用户判断的额外条件查询
        /// </summary>
        /// <param name="memberAccount"></param>
        /// <param name="sex"></param>
        /// <param name="min_age"></param>
        /// <param name="max_age"></param>
        /// <param name="ismarried"></param>
        /// <param name="children"></param>
        /// <param name="min_income"></param>
        /// <param name="max_income"></param>
        /// <param name="hascar"></param>
        /// <param name="hashouse"></param>
        /// <param name="insertTime"></param>
        /// <returns></returns>
        public IEnumerable<Information> GetInformationByAnythingswithSpecificMember(string memberAccount, string sex, string min_age, string max_age, string ismarried, string children, string min_income, string max_income, string hascar, string hashouse, string insertTime)
        {
            Member member = new Member();
            member = _memberDAL.SelectByAccount(memberAccount, 0);

            List<InformationNoEntity> entityList = new List<InformationNoEntity>();
            entityList = _informationDAL.SelectByAnythingswithSpecificMember(member.Id, sex, min_age, max_age, ismarried, children, min_income, max_income, hascar, hashouse, insertTime);
            List<Information> informationList = new List<Information>();

            try
            {
                foreach (var item in entityList)
                {
                    Information info = new Information();
                    info.Id = item.Id;
                    info.Address = item.Address;
                    info.Age = item.Age;
                    info.Children = item.Children;
                    info.CustomerName = item.CustomerName;
                    info.Email = item.Email;
                    info.HasCar = item.HasCar;
                    info.HasHouse = item.HasHouse;
                    info.Hobby = item.Hobby;
                    info.Income = item.Income;
                    info.Industry = item.Industry;
                    info.InserTime = item.InserTime;
                    info.IsMarry = item.IsMarry;
                    info.MemberId = item.MemberId;
                    info.Occupation = item.Occupation;
                    info.Phone = item.Phone;
                    info.QQ = item.QQ;
                    info.Sex = item.Sex;
                    info.WebCat = item.WebCat;
                    info.Note1 = item.Note1;
                    info.Note2 = item.Note2;
                    info.Note3 = item.Note3;

                    informationList.Add(info);
                }
                return informationList;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return null;
            }
        }

        public Information GetInformationById(Guid id)
        {
            return _informationDAL.SelectById(id);
        }

        /// <summary>
        /// 返回information 前count项
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<Information> GetInformationLimited(int count,Guid adminId)
        {
            return _informationDAL.SelectPartofSet(count, adminId);
        }

        /// <summary>
        /// 根据指定的用户Id  memberId 获取该用户下的前count项Information信息集
        /// </summary>
        /// <param name="count"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public IEnumerable<Information> GetinformationLimitedwithSpecificMember(int count, Guid memberId)
        {
            return _informationDAL.SelectPartofSetwithSpecificMember(count, memberId);
        }

        /// <summary>
        /// 根据指定的用户名  memberAccount 获取该用户下的前count项Information信息集
        /// 该方法调用了GetinformationLimitedwithSpecificMember(int, string)这个重载
        /// </summary>
        /// <param name="count"></param>
        /// <param name="memberAccount"></param>
        /// <returns></returns>
        public IEnumerable<Information> GetinformationLimitedwithSpecificMember(int count, string memberAccount)
        {
            Member member = new Member();
            member = _memberDAL.SelectByAccount(memberAccount, 0);

            return _informationDAL.SelectPartofSetwithSpecificMember(count, member.Id);
        }

        public bool Update(Information info)
        {
            return _informationDAL.Update(info);
        }
    }
}
