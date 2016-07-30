namespace HchWebPhr.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActivateDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ActivateDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ActivateDateTime");
        }
    }
}
