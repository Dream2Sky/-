using com.dongfangyunwang.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.dongfangyunwang.IDAL
{
    public interface IInformationDAL:IDataBaseDAL<Information>
    {
        List<InformationNoEntity> SelectByAnythings(string conditions, Guid adminId);
        List<InformationNoEntity> SelectByAnythingswithSpecificMember(string conditions, Guid memberId);
        //List<InformationNoEntity> SelectByAnythingswithSpecificMember(string conditions, string memberAccount);
        IEnumerable<Information> SelectPartofSet(int count, Guid adminId);
        IEnumerable<Information> SelectPartofSetwithSpecificMember(int count, Guid memberId);
        List<InformationNoEntity> SelectByAnythings(Guid adminId, string sex, string min_age, string max_age, string ismarried, string children, string min_income, string max_income, string hascar, string hashouse, string insertTime);
        List<InformationNoEntity> SelectByAnythingswithSpecificMember(Guid memberId, string sex, string min_age, string max_age, string ismarried, string children, string min_income, string max_income, string hascar, string hashouse, string insertTime);
        List<InformationNoEntity> SelectByAnythingswithGroupLeader(string condition, Guid adminId);
        List<Information> SelectByAnythingswithGroupLeader(int count, Guid adminId);
        List<InformationNoEntity> SelectByAnythingswithGroupLeader(string sex, string min_age, string max_age, string ismarried, string children, string min_income, string max_income, string hascar, string hashouse, string insertTime, Guid adminId);

    }
}
