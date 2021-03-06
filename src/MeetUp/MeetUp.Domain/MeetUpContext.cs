﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MeetUp.Domain
{
    public class MeetUpContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<GreatNightOutContext.Web.Models.GreatNightOutContext>());

        public MeetUpContext(): base("name=DefaultConnection")
        {
        }

        //public DbSet<Dinner> Dinners { get; set; }
		public DbSet<Email> Emails { get; set; }
        public DbSet<Occasion> Occasions { get; set; }
        public DbSet<OccasionComment> OcassionComments { get; set; }
        public DbSet<OccasionImage> OccasionImages { get; set; }
        public DbSet<RSVP> RSVPs { get; set; }
        public DbSet<Runner> Runners { get; set; }
		public DbSet<UserAccount> UserAccounts { get; set; }
		public DbSet<UserPic> UserPics { get; set; }
		public DbSet<Venue> Venues { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }


    }
}
