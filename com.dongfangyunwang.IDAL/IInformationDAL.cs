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
        List<InformationNoEntity> SelectByAnythings(string conditions);
        List<InformationNoEntity> SelectByAnythingswithSpecificMember(string conditions, Guid memberId);
        //List<InformationNoEntity> SelectByAnythingswithSpecificMember(string conditions, string memberAccount);
        IEnumerable<Information> SelectPartofSet(int count);
        IEnumerable<Information> SelectPartofSetwithSpecificMember(int count, Guid memberId);
        List<InformationNoEntity> SelectByAnythings(string sex, string min_age, string max_age, string ismarried, string children, string min_income, string max_income, string hascar, string hashouse, string insertTime);
    }
}
