using System.Collections.Generic;

namespace MeetUp.MeetUpApi.Models
{
    public class MemberDowload
    {
        public Meta meta { get; set; }
        public List<MeetupMemberDownload> results { get; set; } 
    }
}