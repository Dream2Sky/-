namespace com.dongfangyunwang.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTable_FollowRecord : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FollowRecords", "FollowValue", c => c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FollowRecords", "FollowValue", c => c.String(nullable: false, unicode: false));
        }
    }
}
