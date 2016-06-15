using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.dongfangyunwang.entity
{
    public class FollowRecord:Entity
    {
        [Required]
        public Guid InforId { get; set; }

        [Required]
        public Guid FollowId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FollowValue { get; set; }
    }
}
