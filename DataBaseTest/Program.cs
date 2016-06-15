using com.dongfangyunwang.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.common;
using com.dongfangyunwang.DAL;

namespace DataBaseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Now inserting some data...");
            #region  添加用户账号  和 Follow


            //Member member = new Member();
            //member.Account = "account1";
            //member.Password = EncryptManager.SHA1("asdf1234");
            //member.IsAdmin = false;

            //Member m1 = new Member();
            //m1.Account = "account2";
            //m1.Password = EncryptManager.SHA1("asdf1234");
            //m1.IsAdmin = false;


            //Follow follow = new Follow();
            //follow.FollowItem = "跟进1";

            //Follow f1 = new Follow();
            //f1.FollowItem = "跟进2";

            //try
            //{
            //    Console.WriteLine("tring...");
            //    MemberDAL dal = new MemberDAL();
            //    dal.Insert(member);
            //    dal.Insert(m1);

            //    FollowDAL fdal = new FollowDAL();
            //    fdal.Insert(f1);
            //    fdal.Insert(follow);


            //    Console.WriteLine("Ok");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Insert data failed,something wrong");
            //    Console.WriteLine(ex.Message);
            //    Console.WriteLine(ex.StackTrace);
            //}
            #endregion

            #region 添加Information

            //InformationDAL infodal = new InformationDAL();
            //try
            //{
            //    Information info = new Information();
            //    info.Address = "广东省";
            //    info.Age = "10";
            //    info.Children = "没有";
            //    info.CustomerName = "呵呵";
            //    info.Email = "12121@qq.com";
            //    info.HasCar = "没有";
            //    info.HasHouse = "没有";
            //    info.Hobby = "电影";
            //    info.Income = "1233";
            //    info.Industry = "IT";
            //    info.InserTime = DateTime.Now.ToString();
            //    info.IsMarry = "没有";
            //    info.MemberId = Guid.Parse("e0e89436-b98b-494e-8044-c752f45475b8");
            //    info.Occupation = "程序员";
            //    info.Phone = "12346485";
            //    info.QQ = "7895546";
            //    info.Sex = "男";
            //    info.WebCat = "heh";

            //    infodal.Insert(info);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Something wrong");
            //    Console.WriteLine(ex.Message);
            //    Console.WriteLine(ex.StackTrace);
            //    throw;
            //}
            #endregion

            #region check for InformationList
            //try
            //{
            //    InformationDAL dal = new InformationDAL();
            //    IEnumerable<Information> infolist = dal.SelectPartofSet(50);
            //    foreach (Information item in infolist)
            //    {
            //        Console.WriteLine(item.CustomerName);
            //    }

            //    Console.WriteLine("OK");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error");
            //    Console.WriteLine(ex.Message);
            //    Console.WriteLine(ex.StackTrace);
            //    throw;
            //}
            #endregion

            //FollowRecordDAL dal = new FollowRecordDAL();
            //IEnumerable<FollowRecord> frList = dal.SelectByInformationId(Guid.Parse("64e69948-951c-4035-bc4b-e53560972865"));

            //Console.Write("followRecord.count: " + frList.Count());
            #region 注释


            //FollowRecordDAL dal = new FollowRecordDAL();

            //try
            //{
            //    FollowRecord Fr = new FollowRecord();
            //    Fr.FollowId = Guid.Parse("064ae90b-005f-4262-aa94-7b13d00d780d");
            //    Fr.FollowValue = "hehe";
            //    Fr.InforId = Guid.Parse("64e69948-951c-4035-bc4b-e53560972865");
            //    dal.Insert(Fr);

            //    FollowRecord fr2 = new FollowRecord();
            //    fr2.FollowId = Guid.Parse("9c9b2c8b-f11d-4e1d-8c0b-0f34e6bb39d4");
            //    fr2.FollowValue = "hahaha";
            //    fr2.InforId = Guid.Parse("64e69948-951c-4035-bc4b-e53560972865");
            //    dal.Insert(fr2);

            //    Console.WriteLine("OK");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    Console.WriteLine(ex.StackTrace);
            //    throw;
            //}

            #endregion


            //InformationDAL inforDal = new InformationDAL();
            //FollowRecordDAL followDal = new FollowRecordDAL();

            //try
            //{
            //    followDal.Clear();
            //    inforDal.Clear();
            //    Console.WriteLine("Deleted data successfully");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    Console.WriteLine(ex.StackTrace);
            //    Console.WriteLine("Deleted data failed");
            //}
            //Console.Read();


            InformationDAL dal = new InformationDAL();
            List<Information> inforList = dal.Test();

            Console.WriteLine("Start");

            foreach (var item in inforList)
            {
                Console.WriteLine(item.Sex);
            }

            Console.WriteLine("Finish");
            Console.Read();
        }
    }
}
