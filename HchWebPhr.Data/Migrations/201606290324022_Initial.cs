namespace HchWebPhr.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        UserInfoId = c.Int(nullable: false),
                        Name = c.String(),
                        ChartNo = c.String(),
                        IdNo = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        PhoneNo = c.String(),
                        Address = c.String(),
                        Sex = c.String(),
                        BloodType = c.String(),
                    })
                .PrimaryKey(t => t.UserInfoId)
                .ForeignKey("dbo.Users", t => t.UserInfoId)
                .Index(t => t.UserInfoId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        ActiveToken = c.String(),
                        ForgetPasswordToken = c.String(),
                        LastLoginTime = c.DateTime(nullable: false),
                        LastLoginIp = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInfoes", "UserInfoId", "dbo.Users");
            DropIndex("dbo.UserInfoes", new[] { "UserInfoId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserInfoes");
        }
    }
}
