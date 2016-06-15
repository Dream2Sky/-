namespace com.dongfangyunwang.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Information_MemberId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Information", "MemberId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Information", "MemberId");
        }
    }
}
