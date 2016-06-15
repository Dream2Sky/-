using com.dongfangyunwang.entity;
using com.dongfangyunwang.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.dongfangyunwang.DAL
{
    public class MemberDAL : DataBaseDAL<Member>, IMemberDAL
    {
        /// <summary>
        ///  单纯的根据账号获得member
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Member SelectByAccount(string account)
        {
            try
            {
                return db.Set<Member>().Where(n => n.Account == account).SingleOrDefault();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 先判断身份 再获得Member 主要用于登陆的时候
        /// </summary>
        /// <param name="account"></param>
        /// <param name="isadmin"></param>
        /// <returns></returns>
        public Member SelectByAccount(string account, bool isadmin)
        {
            try
            {
                return db.Set<Member>().Where(n=>n.Account == account).Where(n=>n.IsAdmin == isadmin).SingleOrDefault();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return null;
            }
        }
    }
}
