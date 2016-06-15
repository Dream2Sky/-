namespace com.dongfangyunwang.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumn_Follow_FollowName : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Follows", "FollowName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Follows", "FollowName", c => c.String(nullable: false, unicode: false));
        }
    }
}
