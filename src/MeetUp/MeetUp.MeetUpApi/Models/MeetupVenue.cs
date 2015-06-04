namespace MeetUp.MeetUpApi.Models
{
    public class MeetupVenue
    {
        public long Id { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
    }
}