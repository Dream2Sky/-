using com.dongfangyunwang.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.dongfangyunwang.IBLL
{
    public interface IInformationBLL
    {
        Information GetInformationById(Guid id);
        IEnumerable<Information> GetInformationByAnythings(string condition, Guid adminId);
        IEnumerable<Information> GetInformationByAnythingswithSpecificMember(string condition, string memberAccount);
        IEnumerable<Information> GetInformationByAnythingswithSpecificMember(string condition, Guid memberId);
        IEnumerable<Information> GetInformationByAnythingswithGroupLeader(string condition, Guid adminId);
        IEnumerable<Information> GetInformationLimited(int count, Guid adminId);
        IEnumerable<Information> GetinformationLimitedwithSpecificMember(int count, string memberAccount);
        IEnumerable<Information> GetinformationLimitedwithSpecificMember(int count, Guid memberId);
        IEnumerable<Information> GetInformationByAnythingswithGroupLeader(int count, Guid adminId);
        bool Add(Information info);
        bool Update(Information info);

        IEnumerable<Information> GetInformationByAnythings(Guid adminId,string sex, string min_age, string max_age, string ismarried ,string children, string min_income, string max_income, string hascar, string hashouse, string insertTime);

        IEnumerable<Information> GetInformationByAnythingswithSpecificMember(string memberAccount, string sex, string min_age, string max_age, string ismarried, string children, string min_income, string max_income, string hascar, string hashouse, string insertTime);
        IEnumerable<Information> GetInformationByAnythingswithGroupLeader(Guid adminId, string sex, string min_age, string max_age, string ismarried, string children, string min_income, string max_income, string hascar, string hashouse, string insertTime);


    }
}