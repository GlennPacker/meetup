using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Domain
{
    public class UserPic
    {
        [Key]
        public int Id { get; set; }
        public bool IsDefault { get; set; }
        public string MeetupImgPath { get; set; }
        public string ContentType { get; set; }
        public byte[] Img { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserAccount User { get; set; }
    }
}