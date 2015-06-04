using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Domain
{
    public class Occasion
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "About the night out / event")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Date Added")]
        public DateTime Created { get; set; }

        public int? MeetupEventId { get; set; }
        public DateTime? MeetupLastUpdated { get; set; }

        [UIHint("HourDropDown")]
        [Display(Name = "Time")]
        [Required(ErrorMessage = "Hour is required")]
        public Int32 EventHour { get; set; }

        [UIHint("MinuteDropDown")]
        [Required(ErrorMessage = "Minute is required")]
        [Display(Name = ":")]
        public Int32 EventMin { get; set; }

        [Display(Name = "Event Date")]
        [Required(ErrorMessage = "Date is required")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime EventDate { get; set; }
        
        public int HostId { get; set; }
        [ForeignKey("HostId")]
        public UserAccount Host { get; set; }

        public int VenueId { get; set; }
        [ForeignKey("VenueId")]
        public Venue Venue { get; set; }
	    
        public bool IsDeleted{get;set;}

        public virtual ICollection<OcassionComment> Comments { get; set; }
        public virtual ICollection<RSVP> RSVP { get; set; }
    }
}