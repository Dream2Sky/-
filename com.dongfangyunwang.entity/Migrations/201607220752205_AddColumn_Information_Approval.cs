namespace com.dongfangyunwang.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Information_Approval : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Information", "Approval", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Information", "Approval");
        }
    }
}
