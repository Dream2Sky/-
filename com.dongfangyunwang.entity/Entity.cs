using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.dongfangyunwang.entity
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
