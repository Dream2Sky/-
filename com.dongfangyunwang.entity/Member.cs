using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.dongfangyunwang.entity
{
    public class Member:Entity
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
