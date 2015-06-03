using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Domain
{
    public class Email
    {
        [Key]
		public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

		public int ToUserId { get; set; }
        [ForeignKey("ToUserId")]
        public virtual UserAccount ToUser { get; set; }

		public int FromUserId { get; set; }
        [ForeignKey("FromUserId")]
        public virtual UserAccount FromUser { get; set; }

	    public bool IsDeleted { get; set; }
    }
}