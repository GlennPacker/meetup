using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Domain
{
    public class OccasionImage
    {
        [Key]
        public int EventImageId { get; set; }
        public byte[] ImgStream { get; set; }
        public string Img { get; set; }
        public byte[] ImgStreamThb { get; set; }
        public string ImgThb { get; set; }
        public string MeetUpPath { get; set; }
        
        public int OccasionId { get; set; }
        [ForeignKey("OccasionId")]
        public virtual Occasion Occasion { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserAccount User { get; set; }

        public int VenueId { get; set; }
        [ForeignKey("VenueId")]
        public virtual Venue Venue { get; set; }
    }
}