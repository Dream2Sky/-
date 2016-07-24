namespace com.dongfangyunwang.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn_Member_IsAdmin : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "Account", c => c.String(nullable: false, maxLength: 24, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Members", "Password", c => c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Members", "IsAdmin", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "IsAdmin", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Members", "Password", c => c.String(unicode: false));
            AlterColumn("dbo.Members", "Account", c => c.String(unicode: false));
        }
    }
}
