using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.dongfangyunwang.entity
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class DFYW_DbContext:DbContext
    {
        public DbSet<Follow> Follows { get; set; }
        public DbSet<FollowRecord> FollowRecords { get; set; }
        public DbSet<Information> InformationSet { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}
