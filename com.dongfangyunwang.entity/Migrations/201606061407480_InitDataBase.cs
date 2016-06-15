namespace com.dongfangyunwang.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FollowRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InforId = c.Guid(nullable: false),
                        FollowId = c.Guid(nullable: false),
                        FollowValue = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FollowItem = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Information",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InserTime = c.String(unicode: false),
                        CustomerName = c.String(unicode: false),
                        Sex = c.String(unicode: false),
                        Age = c.String(unicode: false),
                        IsMarry = c.String(unicode: false),
                        Children = c.String(unicode: false),
                        Phone = c.String(unicode: false),
                        QQ = c.String(unicode: false),
                        WebCat = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        Industry = c.String(unicode: false),
                        Occupation = c.String(unicode: false),
                        Income = c.String(unicode: false),
                        Hobby = c.String(unicode: false),
                        HasCar = c.String(unicode: false),
                        HasHouse = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Account = c.String(unicode: false),
                        Password = c.String(unicode: false),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Members");
            DropTable("dbo.Information");
            DropTable("dbo.Follows");
            DropTable("dbo.FollowRecords");
        }
    }
}
