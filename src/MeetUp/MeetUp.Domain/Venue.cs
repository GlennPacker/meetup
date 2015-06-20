using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Domain
{
    public class Venue
    {
        [Key]
        public int Id { get; set; }
        
        [Index]
        public long MeetUpId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Venue")]
        [StringLength(255, ErrorMessage = "*")]
        public string Name { get; set; }
        
        [Display(Name = "Address")]
        public string Address1 { get; set; }

        [Display(Name = " ")]
        public string Address2 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        public ICollection<Occasion> Occasions { get; set; }
        public virtual ICollection<UserPic> Pics { get; set; }
	    public bool IsDeleted { get; set; }
    }
}