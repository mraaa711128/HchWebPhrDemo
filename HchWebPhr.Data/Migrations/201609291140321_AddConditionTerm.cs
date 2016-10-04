namespace HchWebPhr.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConditionTerm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConditionTerms",
                c => new
                    {
                        ConditionTermId = c.Int(nullable: false, identity: true),
                        EffectiveDateTime = c.DateTime(nullable: false),
                        TermContent = c.String(),
                        LastModifyDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ConditionTermId);
            
            AddColumn("dbo.Users", "AgreeDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "AgreeDateTime");
            DropTable("dbo.ConditionTerms");
        }
    }
}
