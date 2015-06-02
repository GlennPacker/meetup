using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Domain
{
    public class OcassionComment
    {
        [Key]       
        public int Id { get; set; }

        public virtual int OcassionId { get; set; }
        [ForeignKey("OcassionId")]
        public virtual Occasion Ocassion { get; set; }

        [Display(Name = "Comment")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserAccount User { get; set; }

        public DateTime Added { get; set; }
    }
}