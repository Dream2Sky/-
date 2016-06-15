namespace com.dongfangyunwang.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Follow_FollowName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Follows", "FollowName", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Follows", "FollowName");
        }
    }
}
