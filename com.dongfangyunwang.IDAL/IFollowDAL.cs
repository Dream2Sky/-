using com.dongfangyunwang.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.dongfangyunwang.IDAL
{
    public interface IFollowDAL:IDataBaseDAL<Follow>
    {
        Follow SelectByName(string name);
    }
}
