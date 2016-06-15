using com.dongfangyunwang.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.dongfangyunwang.web.Models
{
    public class InformationModel:Information
    {
        public InformationModel()
        { }
        public InformationModel(Information info)
        {
            this.Id = info.Id;
            this.MemberId = info.MemberId;
            this.Income = info.Income;
            this.Industry = info.Industry;
            this.InserTime = info.InserTime;
            this.IsMarry = info.IsMarry;
            this.Occupation = info.Occupation;
            this.Phone = info.Phone;
            this.QQ = info.QQ;
            this.Sex = info.Sex;
            this.WebCat = info.WebCat;
            this.Hobby = info.Hobby;
            this.HasHouse = info.HasHouse;
            this.HasCar = info.HasCar;
            this.Email = info.Email;
            this.CustomerName = info.CustomerName;
            this.Children = info.Children;
            this.Age = info.Age;
            this.Address = info.Address;
        }
        public string MemberAccount { get; set; }
        public IEnumerable<FollowModel> FollowList { get; set; }
    }
}