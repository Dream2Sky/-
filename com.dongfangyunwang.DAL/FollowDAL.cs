using com.dongfangyunwang.entity;
using com.dongfangyunwang.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.dongfangyunwang.DAL
{
    public class FollowDAL : DataBaseDAL<Follow>, IFollowDAL
    {
        public Follow SelectByName(string name)
        {
            try
            {
                return db.Set<Follow>().SingleOrDefault(n => n.FollowItem == name);
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
