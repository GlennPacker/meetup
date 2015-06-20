using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Domain
{
    public class UserPic
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserAccount User { get; set; }

        public int VenueId { get; set; }
        [ForeignKey("VenueId")]
        public virtual Venue Venue{get;set;}
    }
}