namespace HchWebPhr.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFeatureModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        FeatureId = c.Int(nullable: false, identity: true),
                        FeatureName = c.String(),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        RemarkDisplay = c.Boolean(nullable: false),
                        Remark = c.String(storeType: "ntext"),
                        MenuDisplay = c.Boolean(nullable: false),
                        MenuDisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FeatureId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Features");
        }
    }
}
