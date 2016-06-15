namespace com.dongfangyunwang.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTable_Information : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Information", "InserTime", c => c.String(maxLength: 30, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "CustomerName", c => c.String(maxLength: 20, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "Sex", c => c.String(maxLength: 4, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "Age", c => c.String(maxLength: 3, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "IsMarry", c => c.String(maxLength: 4, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "Children", c => c.String(maxLength: 4, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "Phone", c => c.String(maxLength: 20, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "QQ", c => c.String(maxLength: 20, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "WebCat", c => c.String(maxLength: 30, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "Email", c => c.String(maxLength: 50, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "Address", c => c.String(maxLength: 255, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "Industry", c => c.String(maxLength: 30, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "Occupation", c => c.String(maxLength: 30, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "Income", c => c.String(maxLength: 20, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "Hobby", c => c.String(maxLength: 30, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "HasCar", c => c.String(maxLength: 4, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.Information", "HasHouse", c => c.String(maxLength: 4, unicode: false, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Information", "HasHouse", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "HasCar", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "Hobby", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "Income", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "Occupation", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "Industry", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "Address", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "Email", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "WebCat", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "QQ", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "Phone", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "Children", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "IsMarry", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "Age", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "Sex", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "CustomerName", c => c.String(unicode: false));
            AlterColumn("dbo.Information", "InserTime", c => c.String(unicode: false));
        }
    }
}
