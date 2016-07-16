namespace HchWebPhr.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfileImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "ProfileImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInfoes", "ProfileImage");
        }
    }
}
