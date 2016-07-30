namespace HchWebPhr.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccountLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountLogs",
                c => new
                    {
                        AccountLogId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        LoginIpAddress = c.String(),
                        LoginDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AccountLogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountLogs");
        }
    }
}
