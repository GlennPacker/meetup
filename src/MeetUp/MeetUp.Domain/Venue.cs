using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeetUp.Domain
{
    public class Venue
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Venue")]
        [StringLength(50, ErrorMessage = "Name may not be longer than 50 characters")]
        public string Name { get; set; }
        
        [Display(Name = "Address")]
        public string Address1 { get; set; }

        [Display(Name = " ")]
        public string Address2 { get; set; }

        [Display(Name = "Town")]
        public string Town { get; set; }

        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        public ICollection<Occasion> Occasions { get; set; }
        public virtual ICollection<OccasionImage> OccasionImages { get; set; }
	    public bool IsDeleted { get; set; }
    }
}