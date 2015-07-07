using System.Collections.Generic;
using MeetUp.Domain;
using MeetUp.MeetUpApi.Models;

namespace MeetUp.Services.Interfaces
{
    public interface IRsvpFactory
    {
        void MapRsvps(List<MeetupRSVP> data, Occasion occasion);
    }
}