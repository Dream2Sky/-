using com.dongfangyunwang.entity;
using com.dongfangyunwang.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.dongfangyunwang.DAL
{
    public class FollowRecordDAL : DataBaseDAL<FollowRecord>, IFollowRecordDAL
    {
        /// <summary>
        /// 通过informationID 获取 FollowRecord List
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        public IEnumerable<FollowRecord> SelectByInformationId(Guid infoId)
        {
            try
            {
                return db.Set<FollowRecord>().Where(n => n.InforId == infoId);
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
