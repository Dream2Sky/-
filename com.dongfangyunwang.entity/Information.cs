using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.dongfangyunwang.entity
{
    public class Information:Entity
    {
        [MaxLength(30)]
        public string InserTime { get; set; }
        [Required]
        public Guid MemberId { get; set; }
        [MaxLength(20)]
        public string CustomerName { get; set; }
        [MaxLength(4)]
        public string Sex { get; set; }
        [MaxLength(3)]
        public string Age { get; set; }
        [MaxLength(4)]
        public string IsMarry { get; set; }
        [MaxLength(4)]
        public string Children { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(20)]
        public string QQ { get; set; }
        [MaxLength(30)]
        public string WebCat { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        /// <summary>
        /// 所属行业
        /// </summary>
        [MaxLength(30)]
        public string Industry { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        [MaxLength(30)]
        public string Occupation { get; set; }
        /// <summary>
        /// 年收入
        /// </summary>
        [MaxLength(20)]
        public string Income { get; set; }
        /// <summary>
        /// 爱好
        /// </summary>
        [MaxLength(30)]
        public string Hobby { get; set; }
        /// <summary>
        /// 是否有车
        /// </summary>
        [MaxLength(4)]
        public string HasCar { get; set; }
        /// <summary>
        /// 是否有房
        /// </summary>
        [MaxLength(4)]
        public string HasHouse { get; set; }
    }

}
