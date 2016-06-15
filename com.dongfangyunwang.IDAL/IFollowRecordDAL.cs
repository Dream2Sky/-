using com.dongfangyunwang.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.dongfangyunwang.IDAL
{
    public interface IFollowRecordDAL : IDataBaseDAL<FollowRecord>
    {
        IEnumerable<FollowRecord> SelectByInformationId(Guid infoId);
    }
}
