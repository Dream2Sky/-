using com.dongfangyunwang.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.dongfangyunwang.IBLL
{
    public interface IMemberBLL
    {
        bool Login(string account, string password, string isadmin);
        Member GetMemberById(Guid id);
        Member GetMemberByAccount(string name,bool isadmin);
        Member GetMemberByAccount(string name);
        IEnumerable<Member> GetAllMembers();
        bool IsExist(Member member);
        bool Add(Member member);
    }
}
