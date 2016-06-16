namespace com.dongfangyunwang.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumns_Information : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Information", "Note1", c => c.String(maxLength: 255, unicode: false, storeType: "nvarchar"));
            AddColumn("dbo.Information", "Note2", c => c.String(maxLength: 255, unicode: false, storeType: "nvarchar"));
            AddColumn("dbo.Information", "Note3", c => c.String(maxLength: 255, unicode: false, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Information", "Note3");
            DropColumn("dbo.Information", "Note2");
            DropColumn("dbo.Information", "Note1");
        }
    }
}
