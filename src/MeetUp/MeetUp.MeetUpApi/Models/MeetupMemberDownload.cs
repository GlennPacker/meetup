namespace MeetUp.MeetUpApi.Models
{
    public class MeetupMemberDownload
    {
        public string bio { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public long id { get; set; }
        public double joined { get; set; }
        public string lang { get; set; }
        public string lat { get; set; }
        public string link { get; set; }
        public string lon { get; set; }
        public string name { get; set; }
        public dynamic other_services { get; set; }
        public dynamic self { get; set; }
        public string state { get; set; }
        public string status { get; set; }
        public dynamic topics { get; set; }
        public double visited { get; set; }
        public MeetupPhoto Photo { get; set; }
    }
}