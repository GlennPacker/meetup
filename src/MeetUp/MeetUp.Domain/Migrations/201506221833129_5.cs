namespace MeetUp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Runners", new[] { "RefId" });
            AddColumn("dbo.RSVPs", "MeetUpId", c => c.Long());
            AlterColumn("dbo.Runners", "RefId", c => c.Long());
            CreateIndex("dbo.Runners", "RefId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Runners", new[] { "RefId" });
            AlterColumn("dbo.Runners", "RefId", c => c.Int());
            DropColumn("dbo.RSVPs", "MeetUpId");
            CreateIndex("dbo.Runners", "RefId");
        }
    }
}
