﻿using com.dongfangyunwang.entity;
using com.dongfangyunwang.IBLL;
using com.dongfangyunwang.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.jiechengbao.common;

namespace com.dongfangyunwang.BLL
{
    public class MemberBLL:IMemberBLL
    {
        private IMemberDAL _memberDAL;
        public MemberBLL(IMemberDAL memberDAL)
        {
            _memberDAL = memberDAL;
        }

        public bool Add(Member member)
        {
            return _memberDAL.Insert(member);
        }

        public IEnumerable<Member> GetAllMembers()
        {
            return _memberDAL.SelectAll();
        }

        /// <summary>
        /// 不分管理员和一般用户的按账号名称查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Member GetMemberByAccount(string name)
        {
            return _memberDAL.SelectByAccount(name);
        }

        public Member GetMemberByAccount(string name, bool isadmin)
        {
            return _memberDAL.SelectByAccount(name, isadmin);
        }

        public Member GetMemberById(Guid id)
        {
            return _memberDAL.SelectById(id);
        }

        public bool IsExist(Member member)
        {
            if (_memberDAL.SelectByAccount(member.Account) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 登陆逻辑 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="isadmin"></param>
        /// <returns></returns>
        public bool Login(string account, string password, string isadmin)
        {
            try
            {
                bool _isadmin = (isadmin == "y") ? true : false;
                if (string.IsNullOrEmpty(account))
                {
                    return false;
                }

                Member member = new Member();
                member = _memberDAL.SelectByAccount(account,_isadmin);
                if ( member== null)
                {
                    return false;
                }
                else if(member.Password == EncryptManager.SHA1(password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);
                throw;
            }
        }
    }
}
