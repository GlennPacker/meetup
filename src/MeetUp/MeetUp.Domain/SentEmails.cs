using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Domain
{
    public class SentEmails
    {
        [Key]
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string EmailAddress { get; set; }
        public string MessageType { get; set; }


        [ForeignKey("ToUserId")]
        public virtual UserAccount ToUser { get; set; }

        [ForeignKey("FromUserId")]
        public virtual UserAccount FromUser { get; set; }
    }
}