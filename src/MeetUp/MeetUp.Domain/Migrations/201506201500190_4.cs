namespace MeetUp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Runners", "Started", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Runners", "ApiType");
            CreateIndex("dbo.Runners", "RefId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Runners", new[] { "RefId" });
            DropIndex("dbo.Runners", new[] { "ApiType" });
            DropColumn("dbo.Runners", "Started");
        }
    }
}
