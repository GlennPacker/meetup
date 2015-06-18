namespace MeetUp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Message = c.String(),
                        ToUserId = c.Int(nullable: false),
                        FromUserId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        UserAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserAccounts", t => t.UserAccount_Id)
                .ForeignKey("dbo.UserAccounts", t => t.FromUserId)
                .ForeignKey("dbo.UserAccounts", t => t.ToUserId)
                .Index(t => t.ToUserId)
                .Index(t => t.FromUserId)
                .Index(t => t.UserAccount_Id);
            
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeetupMemberId = c.Long(nullable: false),
                        Name = c.String(),
                        Bio = c.String(),
                        EmailAddress = c.String(),
                        Tel = c.String(),
                        AuthUserId = c.Int(),
                        IsAdmin = c.Boolean(nullable: false),
                        ProfileThmb = c.String(),
                        ProfilePic = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.MeetupMemberId);
            
            CreateTable(
                "dbo.OccasionComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OcassionId = c.Int(nullable: false),
                        Comment = c.String(),
                        UserId = c.Int(nullable: false),
                        Added = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Occasions", t => t.OcassionId)
                .ForeignKey("dbo.UserAccounts", t => t.UserId)
                .Index(t => t.OcassionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Occasions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        Created = c.DateTime(nullable: false),
                        MeetupEventId = c.Long(),
                        MeetupLastUpdated = c.DateTime(),
                        Hour = c.Int(nullable: false),
                        Min = c.Int(nullable: false),
                        HostId = c.Int(nullable: false),
                        VenueId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserAccounts", t => t.HostId)
                .ForeignKey("dbo.Venues", t => t.VenueId)
                .Index(t => t.MeetupEventId)
                .Index(t => t.HostId)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.RSVPs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        OccasionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserAccounts", t => t.UserId)
                .ForeignKey("dbo.Occasions", t => t.OccasionId)
                .Index(t => t.UserId)
                .Index(t => t.OccasionId);
            
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeetUpId = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        PostCode = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.MeetUpId);
            
            CreateTable(
                "dbo.UserPics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        ContentType = c.String(),
                        UserId = c.Int(nullable: false),
                        VenueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserAccounts", t => t.UserId)
                .ForeignKey("dbo.Venues", t => t.VenueId)
                .Index(t => t.UserId)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.OccasionImages",
                c => new
                    {
                        EventImageId = c.Int(nullable: false, identity: true),
                        ImgStream = c.Binary(),
                        Img = c.String(),
                        ImgStreamThb = c.Binary(),
                        ImgThb = c.String(),
                        MeetUpPath = c.String(),
                        OccasionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        VenueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventImageId)
                .ForeignKey("dbo.Occasions", t => t.OccasionId)
                .ForeignKey("dbo.UserAccounts", t => t.UserId)
                .ForeignKey("dbo.Venues", t => t.VenueId)
                .Index(t => t.OccasionId)
                .Index(t => t.UserId)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.Runners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastRun = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emails", "ToUserId", "dbo.UserAccounts");
            DropForeignKey("dbo.Emails", "FromUserId", "dbo.UserAccounts");
            DropForeignKey("dbo.OccasionImages", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.OccasionImages", "UserId", "dbo.UserAccounts");
            DropForeignKey("dbo.OccasionImages", "OccasionId", "dbo.Occasions");
            DropForeignKey("dbo.Emails", "UserAccount_Id", "dbo.UserAccounts");
            DropForeignKey("dbo.OccasionComments", "UserId", "dbo.UserAccounts");
            DropForeignKey("dbo.UserPics", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.UserPics", "UserId", "dbo.UserAccounts");
            DropForeignKey("dbo.Occasions", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.RSVPs", "OccasionId", "dbo.Occasions");
            DropForeignKey("dbo.RSVPs", "UserId", "dbo.UserAccounts");
            DropForeignKey("dbo.Occasions", "HostId", "dbo.UserAccounts");
            DropForeignKey("dbo.OccasionComments", "OcassionId", "dbo.Occasions");
            DropIndex("dbo.OccasionImages", new[] { "VenueId" });
            DropIndex("dbo.OccasionImages", new[] { "UserId" });
            DropIndex("dbo.OccasionImages", new[] { "OccasionId" });
            DropIndex("dbo.UserPics", new[] { "VenueId" });
            DropIndex("dbo.UserPics", new[] { "UserId" });
            DropIndex("dbo.Venues", new[] { "MeetUpId" });
            DropIndex("dbo.RSVPs", new[] { "OccasionId" });
            DropIndex("dbo.RSVPs", new[] { "UserId" });
            DropIndex("dbo.Occasions", new[] { "VenueId" });
            DropIndex("dbo.Occasions", new[] { "HostId" });
            DropIndex("dbo.Occasions", new[] { "MeetupEventId" });
            DropIndex("dbo.OccasionComments", new[] { "UserId" });
            DropIndex("dbo.OccasionComments", new[] { "OcassionId" });
            DropIndex("dbo.UserAccounts", new[] { "MeetupMemberId" });
            DropIndex("dbo.Emails", new[] { "UserAccount_Id" });
            DropIndex("dbo.Emails", new[] { "FromUserId" });
            DropIndex("dbo.Emails", new[] { "ToUserId" });
            DropTable("dbo.Runners");
            DropTable("dbo.OccasionImages");
            DropTable("dbo.UserPics");
            DropTable("dbo.Venues");
            DropTable("dbo.RSVPs");
            DropTable("dbo.Occasions");
            DropTable("dbo.OccasionComments");
            DropTable("dbo.UserAccounts");
            DropTable("dbo.Emails");
        }
    }
}
