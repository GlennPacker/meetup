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

        public string FullAddress
        {
            get
            {
                var result = Name ?? "";
                result = result + (Address1 ?? "");
                result = result + (Address2 ?? "");
                result = result + (City ?? "");         //Todo: address needs better parsing including checking city for postcode meetup data is bad.
                result = result + (PostCode ?? "");
                return result;
            }
            
        }
        public virtual ICollection<ApiUserPic> Pics { get; set; }
    }
}