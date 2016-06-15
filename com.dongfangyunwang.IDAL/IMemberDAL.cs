using com.dongfangyunwang.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.dongfangyunwang.IDAL
{
    public interface IMemberDAL:IDataBaseDAL<Member>
    {
        Member SelectByAccount(string account,bool isadmin);
        Member SelectByAccount(string account);
    }
}
