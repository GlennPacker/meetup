using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Domain
{
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }
        [Index]
        public long MeetupMemberId { get; set; }

        [Display(Name = "Screen / Display Name")]
        public string Name { get; set; }
        
        [Display(Name = "About Me")]
        [DataType(DataType.MultilineText)]
        public string Bio { get; set; }
        public string EmailAddress { get; set; } 
        
        //public string DefaultPicPath { get; set; }
        
        [Display(Name = "Contact Tel.")]
        public string Tel { get; set; }
        public int? AuthUserId { get; set; }
        public bool IsAdmin { get; set; }
        
        public string ProfileThmb { get; set; }
        public string ProfilePic { get; set; }

        public virtual ICollection<RSVP> RSVP { get; set; }
        public virtual ICollection<OccasionComment> Comments { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<UserPic> Pics { get; set; }
        public virtual ICollection<OccasionImage> EventImages { get; set; }
	    
		public bool IsDeleted { get; set; }
    }
}