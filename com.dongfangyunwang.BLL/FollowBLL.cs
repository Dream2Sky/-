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
    public class FollowBLL:IFollowBLL
    {
        private IFollowDAL _followDAL;
        public FollowBLL(IFollowDAL followDAL)
        {
            _followDAL = followDAL;
        }

        public bool Add(Follow follow)
        {
            return _followDAL.Insert(follow);
        }

        public IEnumerable<Follow> GetAllFollow()
        {
            return _followDAL.SelectAll();
        }

        public Follow GetFollow(string name)
        {
            return _followDAL.SelectByName(name);
        }

        public Follow GetFollow(Guid id)
        {
            return _followDAL.SelectById(id);
        }

        /// <summary>
        /// 判断是否有同名跟进项
        /// </summary>
        /// <param name="follow"></param>
        /// <returns></returns>
        public bool IsExist(Follow follow)
        {
            if (_followDAL.SelectByName(follow.FollowItem) == null)
            {
                return false;
            }
            return true;
        }
    }
}
