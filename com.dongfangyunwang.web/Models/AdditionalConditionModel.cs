using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.dongfangyunwang.web.Models
{
    /// <summary>
    /// 额外的附件条件
    /// </summary>
    public class AdditionalConditionModel
    {
        public string sex { get; set; } // 性别

        public string min_age { get; set; } // 年龄下限
        public string max_age { get; set; } // 年龄上限

        public string ismarried { get; set; } // 婚否

        public string children { get; set; } // 子女

        public string min_income { get; set; } // 收入下限
        public string max_income { get; set; } // 收入上限

        public string hascar { get; set; } // 是否有车

        public string hashouse { get; set; } // 是否有房

        public string insertTime { get; set; } // 收录时间

    }
}