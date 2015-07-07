namespace MeetUp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RSVPs", "Guests", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RSVPs", "Guests");
        }
    }
}
