using System.Collections.Generic;

namespace MeetUp.MeetUpApi.Models
{
    public class MeetUpMembers
    {
        public Meta meta { get; set; }
        public List<MeetupMember> results { get; set; } 
    }
}