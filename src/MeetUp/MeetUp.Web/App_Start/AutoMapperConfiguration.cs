using AutoMapper;
using MeetUp.Domain;
using MeetUp.Services.ApiModels;

namespace MeetUp.Web
{
    public static class AutoMapperConfiguration
    {
            
        public static void Configure()
        {
            ConfigureApiMapping();
        }

        private static void ConfigureApiMapping()
        {
            Mapper.CreateMap<Occasion, ApiOccasionInfo>();
            Mapper.CreateMap<OccasionComment, ApiOccasionComment>();
            Mapper.CreateMap<RSVP, ApiRSVP>();
            Mapper.CreateMap<UserAccount, ApiUserAccount>();
            Mapper.CreateMap<UserPic, ApiUserPic>();
            Mapper.CreateMap<Venue, ApiVenue>();
        }
    }
}