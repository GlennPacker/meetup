namespace MeetUp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Runners", "ApiType", c => c.Int(nullable: false));
            AddColumn("dbo.Runners", "RefId", c => c.Int());
            DropColumn("dbo.Runners", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Runners", "Name", c => c.String());
            DropColumn("dbo.Runners", "RefId");
            DropColumn("dbo.Runners", "ApiType");
        }
    }
}
