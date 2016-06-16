using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.dongfangyunwang.IDAL
{
    public class InformationNoEntity
    {
        public Guid Id { get; set; }
        public string InserTime { get; set; }
        public Guid MemberId { get; set; }
        public string CustomerName { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }
        public string IsMarry { get; set; }
        public string Children { get; set; }
        public string Phone { get; set; }
        public string QQ { get; set; }
        public string WebCat { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        /// <summary>
        /// 所属行业
        /// </summary>
        public string Industry { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        public string Occupation { get; set; }
        /// <summary>
        /// 年收入
        /// </summary>
        public string Income { get; set; }
        /// <summary>
        /// 爱好
        /// </summary>
        public string Hobby { get; set; }
        /// <summary>
        /// 是否有车
        /// </summary>
        public string HasCar { get; set; }
        /// <summary>
        /// 是否有房
        /// </summary>
        public string HasHouse { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note1 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note2 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note3 { get; set; }

    }
}
