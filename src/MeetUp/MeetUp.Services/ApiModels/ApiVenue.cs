using System.Collections.Generic;

namespace MeetUp.Services.ApiModels
{
    public class ApiVenue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public virtual ICollection<ApiUserPic> Pics { get; set; }
    }
}