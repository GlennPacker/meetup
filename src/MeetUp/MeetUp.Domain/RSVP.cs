using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Domain
{
    public class RSVP
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OccasionId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual UserAccount Going { get; set; }

        [ForeignKey("OccasionId")]
        public virtual Occasion Occasion { get; set; }

    }
}