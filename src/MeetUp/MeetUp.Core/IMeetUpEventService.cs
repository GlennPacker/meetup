using System.Linq;
using MeetUp.Domain;

namespace MeetUp.Core
{
    public interface IMeetUpEventService
    {
        void GetEventsFromMeetUp(bool force = false);
        IQueryable<Occasion> GetOccasions();
        void Dispose();
    }
}