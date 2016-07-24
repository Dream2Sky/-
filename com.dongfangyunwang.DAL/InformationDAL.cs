using com.dongfangyunwang.entity;
using com.dongfangyunwang.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.dongfangyunwang.DAL
{
    public class InformationDAL : DataBaseDAL<Information>, IInformationDAL
    {
        /// <summary>
        /// 模糊查询 但仅包括 客户名 爱好 地址 邮箱 行业 职业 微信号 和跟进项
        /// 其他条件 包括年龄 收入 是否有车 有房 qq 电话 等等并不适合做模糊查询
        /// 所以会有其他方法去检索他们
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public List<InformationNoEntity> SelectByAnythings(string conditions,Guid adminId)
        {
            try
            {
                // information followrecord follows 三表联合查询  
                var result = (from info in db.InformationSet
                              join fr in db.FollowRecords
                              on info.Id equals fr.InforId into joinedInfor
                              from fr in joinedInfor.DefaultIfEmpty()
                              where info.CustomerName.Contains(conditions) || info.Hobby.Contains(conditions)
                                    || info.Address.Contains(conditions) || info.Email.Contains(conditions)
                                    || info.Industry.Contains(conditions) || info.Occupation.Contains(conditions)
                                    || info.WebCat.Contains(conditions) || info.Note1.Contains(conditions) || info.Note2.Contains(conditions) || info.Note3.Contains(conditions)
                                    || fr.FollowValue.Contains(conditions) && (info.Approval == true || info.MemberId ==adminId)
                              select new InformationNoEntity
                              {
                                  Id = info.Id,
                                  Address = info.Address,
                                  Age = info.Age,
                                  Children = info.Children,
                                  CustomerName = info.CustomerName,
                                  Email = info.Email,
                                  HasCar = info.HasCar,
                                  HasHouse = info.HasHouse,
                                  Hobby = info.Hobby,
                                  Income = info.Income,
                                  Industry = info.Industry,
                                  InserTime = info.InserTime,
                                  IsMarry = info.IsMarry,
                                  MemberId = info.MemberId,
                                  Occupation = info.Occupation,
                                  Phone = info.Phone,
                                  QQ = info.QQ,
                                  Sex = info.Sex,
                                  WebCat = info.WebCat,
                                  Note1 = info.Note1,
                                  Note2 = info.Note2,
                                  Note3 = info.Note3,
                                  Approval = info.Approval
                              }).Distinct();

                return result.ToList();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return null;
            }
        }

        /// <summary>
        /// 额外条件查询
        /// </summary>
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
        public List<InformationNoEntity> SelectByAnythings(Guid adminId, string sex, string min_age, string max_age, string ismarried, string children, string min_income, string max_income, string hascar, string hashouse, string insertTime)
        {
            for (int i = 0; i < max_age.Length - min_age.Length; i++)
            {
                min_age = "0" + min_age;
            }
            for (int i = 0; i < max_income.Length - min_income.Length; i++)
            {
                min_income = "0" + max_income;
            }
            try
            {

                var result = from info in db.InformationSet
                             where info.Sex.Contains(sex) && info.IsMarry.Contains(ismarried) && info.Children.Contains(children) && info.HasCar.Contains(hascar) && info.HasHouse.Contains(hashouse) && info.InserTime.Contains(insertTime) && (info.Approval == true || info.MemberId == adminId)
                             select new InformationNoEntity
                             {
                                 Id = info.Id,
                                 Address = info.Address,
                                 Age = info.Age,
                                 Children = info.Children,
                                 CustomerName = info.CustomerName,
                                 Email = info.Email,
                                 HasCar = info.HasCar,
                                 HasHouse = info.HasHouse,
                                 Hobby = info.Hobby,
                                 Income = info.Income,
                                 Industry = info.Industry,
                                 InserTime = info.InserTime,
                                 IsMarry = info.IsMarry,
                                 MemberId = info.MemberId,
                                 Occupation = info.Occupation,
                                 Phone = info.Phone,
                                 QQ = info.QQ,
                                 Sex = info.Sex,
                                 WebCat = info.WebCat,
                                 Note1 = info.Note1,
                                 Note2 = info.Note2,
                                 Note3 = info.Note3,
                                 Approval = info.Approval
                             };

                if (!string.IsNullOrEmpty(min_age))
                {
                    result = from info in result
                             where string.Compare(info.Age, min_age) >= 0
                             select new InformationNoEntity
                             {
                                 Id = info.Id,
                                 Address = info.Address,
                                 Age = info.Age,
                                 Children = info.Children,
                                 CustomerName = info.CustomerName,
                                 Email = info.Email,
                                 HasCar = info.HasCar,
                                 HasHouse = info.HasHouse,
                                 Hobby = info.Hobby,
                                 Income = info.Income,
                                 Industry = info.Industry,
                                 InserTime = info.InserTime,
                                 IsMarry = info.IsMarry,
                                 MemberId = info.MemberId,
                                 Occupation = info.Occupation,
                                 Phone = info.Phone,
                                 QQ = info.QQ,
                                 Sex = info.Sex,
                                 WebCat = info.WebCat,
                                 Note1 = info.Note1,
                                 Note2 = info.Note2,
                                 Note3 = info.Note3,
                                 Approval = info.Approval
                             };
                }
                if (!string.IsNullOrEmpty(max_age))
                {
                    result = from info in result
                             where string.Compare(info.Age, max_age) <= 0
                             select new InformationNoEntity
                             {
                                 Id = info.Id,
                                 Address = info.Address,
                                 Age = info.Age,
                                 Children = info.Children,
                                 CustomerName = info.CustomerName,
                                 Email = info.Email,
                                 HasCar = info.HasCar,
                                 HasHouse = info.HasHouse,
                                 Hobby = info.Hobby,
                                 Income = info.Income,
                                 Industry = info.Industry,
                                 InserTime = info.InserTime,
                                 IsMarry = info.IsMarry,
                                 MemberId = info.MemberId,
                                 Occupation = info.Occupation,
                                 Phone = info.Phone,
                                 QQ = info.QQ,
                                 Sex = info.Sex,
                                 WebCat = info.WebCat,
                                 Note1 = info.Note1,
                                 Note2 = info.Note2,
                                 Note3 = info.Note3,
                                 Approval = info.Approval
                             };
                }
                if (!string.IsNullOrEmpty(min_income))
                {
                    result = from info in result
                             where string.Compare(info.Income, min_income) >= 0
                             select new InformationNoEntity
                             {
                                 Id = info.Id,
                                 Address = info.Address,
                                 Age = info.Age,
                                 Children = info.Children,
                                 CustomerName = info.CustomerName,
                                 Email = info.Email,
                                 HasCar = info.HasCar,
                                 HasHouse = info.HasHouse,
                                 Hobby = info.Hobby,
                                 Income = info.Income,
                                 Industry = info.Industry,
                                 InserTime = info.InserTime,
                                 IsMarry = info.IsMarry,
                                 MemberId = info.MemberId,
                                 Occupation = info.Occupation,
                                 Phone = info.Phone,
                                 QQ = info.QQ,
                                 Sex = info.Sex,
                                 WebCat = info.WebCat,
                                 Note1 = info.Note1,
                                 Note2 = info.Note2,
                                 Note3 = info.Note3,
                                 Approval = info.Approval
                             };
                }
                if (!string.IsNullOrEmpty(max_income))
                {
                    result = from info in result
                             where string.Compare(info.Income, max_income) <= 0
                             select new InformationNoEntity
                             {
                                 Id = info.Id,
                                 Address = info.Address,
                                 Age = info.Age,
                                 Children = info.Children,
                                 CustomerName = info.CustomerName,
                                 Email = info.Email,
                                 HasCar = info.HasCar,
                                 HasHouse = info.HasHouse,
                                 Hobby = info.Hobby,
                                 Income = info.Income,
                                 Industry = info.Industry,
                                 InserTime = info.InserTime,
                                 IsMarry = info.IsMarry,
                                 MemberId = info.MemberId,
                                 Occupation = info.Occupation,
                                 Phone = info.Phone,
                                 QQ = info.QQ,
                                 Sex = info.Sex,
                                 WebCat = info.WebCat,
                                 Note1 = info.Note1,
                                 Note2 = info.Note2,
                                 Note3 = info.Note3,
                                 Approval = info.Approval
                             };
                }

                return result.ToList();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                return null;
            }
        }

        public List<Information> SelectByAnythingswithGroupLeader(int count, Guid adminId)
        {
            try
            {
                return db.Set<Information>().Where(n => n.MemberId != adminId).OrderBy(n => n.Approval).Take(count).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }


        /// <summary>
        /// 组长 任意条件搜索
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public List<InformationNoEntity> SelectByAnythingswithGroupLeader(string conditions, Guid adminId)
        {
            try
            {
                // information followrecord 
                var result = (from info in db.InformationSet
                              join fr in db.FollowRecords
                              on info.Id equals fr.InforId into joinedInfor
                              from fr in joinedInfor.DefaultIfEmpty()
                              where info.CustomerName.Contains(conditions) || info.Hobby.Contains(conditions)
                                    || info.Address.Contains(conditions) || info.Email.Contains(conditions)
                                    || info.Industry.Contains(conditions) || info.Occupation.Contains(conditions)
                                    || info.WebCat.Contains(conditions) || info.Note1.Contains(conditions) || info.Note2.Contains(conditions) || info.Note3.Contains(conditions)
                                    || fr.FollowValue.Contains(conditions) && info.MemberId != adminId
                              select new InformationNoEntity
                              {
                                  Id = info.Id,
                                  Address = info.Address,
                                  Age = info.Age,
                                  Children = info.Children,
                                  CustomerName = info.CustomerName,
                                  Email = info.Email,
                                  HasCar = info.HasCar,
                                  HasHouse = info.HasHouse,
                                  Hobby = info.Hobby,
                                  Income = info.Income,
                                  Industry = info.Industry,
                                  InserTime = info.InserTime,
                                  IsMarry = info.IsMarry,
                                  MemberId = info.MemberId,
                                  Occupation = info.Occupation,
                                  Phone = info.Phone,
                                  QQ = info.QQ,
                                  Sex = info.Sex,
                                  WebCat = info.WebCat,
                                  Note1 = info.Note1,
                                  Note2 = info.Note2,
                                  Note3 = info.Note3,
                                  Approval = info.Approval
                              }).Distinct();

                return result.ToList();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return null;
            }
        }

        public List<InformationNoEntity> SelectByAnythingswithGroupLeader(string sex, string min_age, string max_age, string ismarried, string children, string min_income, string max_income, string hascar, string hashouse, string insertTime, Guid adminId)
        {
            for (int i = 0; i < max_age.Length - min_age.Length; i++)
            {
                min_age = "0" + min_age;
            }
            for (int i = 0; i < max_income.Length - min_income.Length; i++)
            {
                min_income = "0" + max_income;
            }
            try
            {

                var result = from info in db.InformationSet
                             where info.Sex.Contains(sex) && info.IsMarry.Contains(ismarried) && info.Children.Contains(children) && info.HasCar.Contains(hascar) && info.HasHouse.Contains(hashouse) && info.InserTime.Contains(insertTime) && info.MemberId != adminId
                             select new InformationNoEntity
                             {
                                 Id = info.Id,
                                 Address = info.Address,
                                 Age = info.Age,
                                 Children = info.Children,
                                 CustomerName = info.CustomerName,
                                 Email = info.Email,
                                 HasCar = info.HasCar,
                                 HasHouse = info.HasHouse,
                                 Hobby = info.Hobby,
                                 Income = info.Income,
                                 Industry = info.Industry,
                                 InserTime = info.InserTime,
                                 IsMarry = info.IsMarry,
                                 MemberId = info.MemberId,
                                 Occupation = info.Occupation,
                                 Phone = info.Phone,
                                 QQ = info.QQ,
                                 Sex = info.Sex,
                                 WebCat = info.WebCat,
                                 Note1 = info.Note1,
                                 Note2 = info.Note2,
                                 Note3 = info.Note3,
                                 Approval = info.Approval
                             };

                if (!string.IsNullOrEmpty(min_age))
                {
                    result = from info in result
                             where string.Compare(info.Age, min_age) >= 0
                             select new InformationNoEntity
                             {
                                 Id = info.Id,
                                 Address = info.Address,
                                 Age = info.Age,
                                 Children = info.Children,
                                 CustomerName = info.CustomerName,
                                 Email = info.Email,
                                 HasCar = info.HasCar,
                                 HasHouse = info.HasHouse,
                                 Hobby = info.Hobby,
                                 Income = info.Income,
                                 Industry = info.Industry,
                                 InserTime = info.InserTime,
                                 IsMarry = info.IsMarry,
                                 MemberId = info.MemberId,
                                 Occupation = info.Occupation,
                                 Phone = info.Phone,
                                 QQ = info.QQ,
                                 Sex = info.Sex,
                                 WebCat = info.WebCat,
                                 Note1 = info.Note1,
                                 Note2 = info.Note2,
                                 Note3 = info.Note3,
                                 Approval = info.Approval
                             };
                }
                if (!string.IsNullOrEmpty(max_age))
                {
                    result = from info in result
                             where string.Compare(info.Age, max_age) <= 0
                             select new InformationNoEntity
                             {
                                 Id = info.Id,
                                 Address = info.Address,
                                 Age = info.Age,
                                 Children = info.Children,
                                 CustomerName = info.CustomerName,
                                 Email = info.Email,
                                 HasCar = info.HasCar,
                                 HasHouse = info.HasHouse,
                                 Hobby = info.Hobby,
                                 Income = info.Income,
                                 Industry = info.Industry,
                                 InserTime = info.InserTime,
                                 IsMarry = info.IsMarry,
                                 MemberId = info.MemberId,
                                 Occupation = info.Occupation,
                                 Phone = info.Phone,
                                 QQ = info.QQ,
                                 Sex = info.Sex,
                                 WebCat = info.WebCat,
                                 Note1 = info.Note1,
                                 Note2 = info.Note2,
                                 Note3 = info.Note3,
                                 Approval = info.Approval
                             };
                }
                if (!string.IsNullOrEmpty(min_income))
                {
                    result = from info in result
                             where string.Compare(info.Income, min_income) >= 0
                             select new InformationNoEntity
                             {
                                 Id = info.Id,
                                 Address = info.Address,
                                 Age = info.Age,
                                 Children = info.Children,
                                 CustomerName = info.CustomerName,
                                 Email = info.Email,
                                 HasCar = info.HasCar,
                                 HasHouse = info.HasHouse,
                                 Hobby = info.Hobby,
                                 Income = info.Income,
                                 Industry = info.Industry,
                                 InserTime = info.InserTime,
                                 IsMarry = info.IsMarry,
                                 MemberId = info.MemberId,
                                 Occupation = info.Occupation,
                                 Phone = info.Phone,
                                 QQ = info.QQ,
                                 Sex = info.Sex,
                                 WebCat = info.WebCat,
                                 Note1 = info.Note1,
                                 Note2 = info.Note2,
                                 Note3 = info.Note3,
                                 Approval = info.Approval
                             };
                }
                if (!string.IsNullOrEmpty(max_income))
                {
                    result = from info in result
                             where string.Compare(info.Income, max_income) <= 0
                             select new InformationNoEntity
                             {
                                 Id = info.Id,
                                 Address = info.Address,
                                 Age = info.Age,
                                 Children = info.Children,
                                 CustomerName = info.CustomerName,
                                 Email = info.Email,
                                 HasCar = info.HasCar,
                                 HasHouse = info.HasHouse,
                                 Hobby = info.Hobby,
                                 Income = info.Income,
                                 Industry = info.Industry,
                                 InserTime = info.InserTime,
                                 IsMarry = info.IsMarry,
                                 MemberId = info.MemberId,
                                 Occupation = info.Occupation,
                                 Phone = info.Phone,
                                 QQ = info.QQ,
                                 Sex = info.Sex,
                                 WebCat = info.WebCat,
                                 Note1 = info.Note1,
                                 Note2 = info.Note2,
                                 Note3 = info.Note3,
                                 Approval = info.Approval
                             };
                }

                return result.ToList();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                return null;
            }

        }

        #region 注释掉 SelectByAnythingswithSpecificMember(string ,string) 这个重载
        ///// <summary>
        ///// 根据指定用户的用户名按条件查询信息
        ///// 这个方法调用了 SelectByAnythingswithSpecificMember(string, guid) 这个重载
        ///// </summary>
        ///// <param name="conditions"></param>
        ///// <param name="memberAccount"></param>
        ///// <returns></returns>
        //public List<InformationNoEntity> SelectByAnythingswithSpecificMember(string conditions, string memberAccount)
        //{
        //    Member member = new Member();
        //    MemberDAL dal = new MemberDAL();
        //    member = dal.SelectByAccount(memberAccount, false);

        //    try
        //    {
        //        return SelectByAnythingswithSpecificMember(conditions, member.Id);
        //    }
        //    catch (Exception ex)
        //    {

        //        LogHelper.Log.Write(ex.Message);
        //        LogHelper.Log.Write(ex.StackTrace);

        //        return null;
        //    }
        //}
        #endregion

        /// <summary>
        /// 根据指定用户GUID按条件查询信息
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public List<InformationNoEntity> SelectByAnythingswithSpecificMember(string conditions, Guid memberId)
        {
            try
            {
                // information followrecord follows 三表联合查询  
                var result = (from info in db.InformationSet
                              join fr in db.FollowRecords
                              on info.Id equals fr.InforId into joinedInfor
                              from fr in joinedInfor.DefaultIfEmpty()
                              where info.MemberId == memberId
                              where info.CustomerName.Contains(conditions) || info.Hobby.Contains(conditions)
                                    || info.Address.Contains(conditions) || info.Email.Contains(conditions)
                                    || info.Industry.Contains(conditions) || info.Occupation.Contains(conditions)
                                    || info.WebCat.Contains(conditions) || info.Note1.Contains(conditions) || info.Note2.Contains(conditions) || info.Note3.Contains(conditions)
                                    || fr.FollowValue.Contains(conditions)
                              select new InformationNoEntity
                              {
                                  Id = info.Id,
                                  Address = info.Address,
                                  Age = info.Age,
                                  Children = info.Children,
                                  CustomerName = info.CustomerName,
                                  Email = info.Email
                              ,
                                  HasCar = info.HasCar,
                                  HasHouse = info.HasHouse,
                                  Hobby = info.Hobby,
                                  Income = info.Income,
                                  Industry = info.Industry,
                                  InserTime = info.InserTime,
                                  IsMarry = info.IsMarry,
                                  MemberId = info.MemberId,
                                  Occupation = info.Occupation,
                                  Phone = info.Phone,
                                  QQ = info.QQ,
                                  Sex = info.Sex,
                                  WebCat = info.WebCat,
                                  Note1 = info.Note1,
                                  Note2 = info.Note2,
                                  Note3 = info.Note3
                              }).Distinct();

                return result.ToList();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return null;
            }
        }

        /// <summary>
        /// 带memberId验证的额外条件查询
        /// </summary>
        /// <param name="memberId"></param>
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
        public List<InformationNoEntity> SelectByAnythingswithSpecificMember(Guid memberId, string sex, string min_age, string max_age, string ismarried, string children, string min_income, string max_income, string hascar, string hashouse, string insertTime)
        {
            for (int i = 0; i < max_age.Length - min_age.Length; i++)
            {
                min_age = "0" + min_age;
            }
            for (int i = 0; i < max_income.Length - min_income.Length; i++)
            {
                min_income = "0" + max_income;
            }
            try
            {

                var result = from info in db.InformationSet
                             where info.Sex.Contains(sex) && info.IsMarry.Contains(ismarried) && info.Children.Contains(children) && info.HasCar.Contains(hascar) && info.HasHouse.Contains(hashouse) && info.InserTime.Contains(insertTime)
                             where info.MemberId == memberId
                             select new InformationNoEntity
                             {
                                 Id = info.Id,
                                 Address = info.Address,
                                 Age = info.Age,
                                 Children = info.Children,
                                 CustomerName = info.CustomerName,
                                 Email = info.Email,
                                 HasCar = info.HasCar,
                                 HasHouse = info.HasHouse,
                                 Hobby = info.Hobby,
                                 Income = info.Income,
                                 Industry = info.Industry,
                                 InserTime = info.InserTime,
                                 IsMarry = info.IsMarry,
                                 MemberId = info.MemberId,
                                 Occupation = info.Occupation,
                                 Phone = info.Phone,
                                 QQ = info.QQ,
                                 Sex = info.Sex,
                                 WebCat = info.WebCat,
                                 Note1 = info.Note1,
                                 Note2 = info.Note2,
                                 Note3 = info.Note3
                             };

                if (!string.IsNullOrEmpty(min_age))
                {
                    result = from info in result
                             where string.Compare(info.Age, min_age) >= 0
                             select new InformationNoEntity
                             {
                                 Id = info.Id,
                                 Address = info.Address,
                                 Age = info.Age,
                                 Children = info.Children,
                                 CustomerName = info.CustomerName,
                                 Email = info.Email,
                                 HasCar = info.HasCar,
                                 HasHouse = info.HasHouse,
                                 Hobby = info.Hobby,
                                 Income = info.Income,
                                 Industry = info.Industry,
                                 InserTime = info.InserTime,
                                 IsMarry = info.IsMarry,
                                 MemberId = info.MemberId,
                                 Occupation = info.Occupation,
                                 Phone = info.Phone,
                                 QQ = info.QQ,
                                 Sex = info.Sex,
                                 WebCat = info.WebCat,
                                 Note1 = info.Note1,
                                 Note2 = info.Note2,
                                 Note3 = info.Note3
                             };
                }
                if (!string.IsNullOrEmpty(max_age))
                {
                    result = from info in result
                             where string.Compare(info.Age, max_age) <= 0
                             select new InformationNoEntity
                             {
                                 Id = info.Id,
                                 Address = info.Address,
                                 Age = info.Age,
                                 Children = info.Children,
                                 CustomerName = info.CustomerName,
                                 Email = info.Email,
                                 HasCar = info.HasCar,
                                 HasHouse = info.HasHouse,
                                 Hobby = info.Hobby,
                                 Income = info.Income,
                                 Industry = info.Industry,
                                 InserTime = info.InserTime,
                                 IsMarry = info.IsMarry,
                                 MemberId = info.MemberId,
                                 Occupation = info.Occupation,
                                 Phone = info.Phone,
                                 QQ = info.QQ,
                                 Sex = info.Sex,
                                 WebCat = info.WebCat,
                                 Note1 = info.Note1,
                                 Note2 = info.Note2,
                                 Note3 = info.Note3
                             };
                }
                if (!string.IsNullOrEmpty(min_income))
                {
                    result = from info in result
                             where string.Compare(info.Income, min_income) >= 0
                             select new InformationNoEntity
                             {
                                 Id = info.Id,
                                 Address = info.Address,
                                 Age = info.Age,
                                 Children = info.Children,
                                 CustomerName = info.CustomerName,
                                 Email = info.Email,
                                 HasCar = info.HasCar,
                                 HasHouse = info.HasHouse,
                                 Hobby = info.Hobby,
                                 Income = info.Income,
                                 Industry = info.Industry,
                                 InserTime = info.InserTime,
                                 IsMarry = info.IsMarry,
                                 MemberId = info.MemberId,
                                 Occupation = info.Occupation,
                                 Phone = info.Phone,
                                 QQ = info.QQ,
                                 Sex = info.Sex,
                                 WebCat = info.WebCat,
                                 Note1 = info.Note1,
                                 Note2 = info.Note2,
                                 Note3 = info.Note3
                             };
                }
                if (!string.IsNullOrEmpty(max_income))
                {
                    result = from info in result
                             where string.Compare(info.Income, max_income) <= 0
                             select new InformationNoEntity
                             {
                                 Id = info.Id,
                                 Address = info.Address,
                                 Age = info.Age,
                                 Children = info.Children,
                                 CustomerName = info.CustomerName,
                                 Email = info.Email,
                                 HasCar = info.HasCar,
                                 HasHouse = info.HasHouse,
                                 Hobby = info.Hobby,
                                 Income = info.Income,
                                 Industry = info.Industry,
                                 InserTime = info.InserTime,
                                 IsMarry = info.IsMarry,
                                 MemberId = info.MemberId,
                                 Occupation = info.Occupation,
                                 Phone = info.Phone,
                                 QQ = info.QQ,
                                 Sex = info.Sex,
                                 WebCat = info.WebCat,
                                 Note1 = info.Note1,
                                 Note2 = info.Note2,
                                 Note3 = info.Note3
                             };
                }

                return result.ToList();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 返回information 集合的前count项
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<Information> SelectPartofSet(int count, Guid adminId)
        {
            try
            {
                return db.Set<Information>().Where(n=>n.Approval == true || n.MemberId == adminId).Take(count);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return null;
            }
        }

        /// <summary>
        /// 返回指定memberId的信息集的前count项
        /// </summary>
        /// <param name="count"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public IEnumerable<Information> SelectPartofSetwithSpecificMember(int count, Guid memberId)
        {
            try
            {
                return db.Set<Information>().Where(m => m.MemberId == memberId).Take(count);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return null;
            }
        }

        public List<Information> Test()
        {
            return db.Set<Information>().Where(n => n.Sex.Contains(null)).ToList();
        }
    }
}
