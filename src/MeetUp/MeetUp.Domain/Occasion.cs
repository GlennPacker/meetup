using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Domain
{
    public class Occasion
    {
        public Occasion()
        {
            Comments = new List<OccasionComment>();
            RSVP = new List<RSVP>();
            Created = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Event Date")]
        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Display(Name = "About the night out / event")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Date Added")]
        public DateTime Created { get; set; }

        [Index]
        public long? MeetupEventId { get; set; }
        public DateTime? MeetupLastUpdated { get; set; }

        [UIHint("HourDropDown")]
        [Display(Name = "Time")]
        [Required(ErrorMessage = "Hour is required")]
        public Int32 Hour { get; set; }

        [UIHint("MinuteDropDown")]
        [Required(ErrorMessage = "Minute is required")]
        [Display(Name = ":")]
        public Int32 Min { get; set; }
        
        public int HostId { get; set; }
        [ForeignKey("HostId")]
        public virtual UserAccount Host { get; set; }

        public int VenueId { get; set; }
        [ForeignKey("VenueId")]
        public Venue Venue { get; set; }
	    
        public bool IsDeleted{get;set;}

        public virtual List<OccasionComment> Comments { get; set; }
        public virtual List<RSVP> RSVP { get; set; }

        // while it is easier to deal with data entry as sperated date it does not always so
        public DateTime? OccasionDateTime
        {
            get
            {
                return new DateTime(Date.Year, Date.Month, Date.Day, Hour, Min, 0);
            }
        }
    }
}