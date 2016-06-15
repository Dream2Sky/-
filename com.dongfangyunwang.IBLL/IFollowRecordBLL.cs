using com.dongfangyunwang.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.dongfangyunwang.IBLL
{
    public interface IFollowRecordBLL
    {
        IEnumerable<FollowRecord> GetFollowRecordByInformationId(Guid infoId);
        bool Add(FollowRecord follow);
    }
}
