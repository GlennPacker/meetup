using System.Linq;
using MeetUp.Domain;

namespace MeetUp.Core
{
	public interface IVenueRepository
	{
		IQueryable<Venue> List();
		Venue Find(int id);
        Venue FindByMeetUpId(long id);
		bool Add(Venue item);
		bool Update(Venue item);
		bool Delete(int id);
		bool Delete(Venue item);
		bool Save();
        void Dispose();
        
	}
}