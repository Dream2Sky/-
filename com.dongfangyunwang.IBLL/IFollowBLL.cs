using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.dongfangyunwang.entity;

namespace com.dongfangyunwang.IBLL
{
    public interface IFollowBLL
    {
        IEnumerable<Follow> GetAllFollow();
        Follow GetFollow(Guid id);
        Follow GetFollow(string name);
        bool Add(Follow follow);
        bool IsExist(Follow follow);
    }
}
