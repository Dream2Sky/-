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
    public class FollowRecordBLL:IFollowRecordBLL
    {
        private IFollowRecordDAL _followRecordDAL;
        public FollowRecordBLL(IFollowRecordDAL followRecordDAL)
        {
            _followRecordDAL = followRecordDAL;
        }

        public bool Add(FollowRecord follow)
        {
            return _followRecordDAL.Insert(follow);
        }

        public IEnumerable<FollowRecord> GetFollowRecordByInformationId(Guid infoId)
        {
            return _followRecordDAL.SelectByInformationId(infoId);
        }
    }
}
