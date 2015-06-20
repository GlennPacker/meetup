using System.Linq;
using MeetUp.Domain;

namespace MeetUp.Core
{
	public interface IRSVPRepository
	{
		IQueryable<RSVP> List(int? occasionId);
		RSVP Find(int id);
		bool Add(RSVP item);
		bool Delete(int id);
		bool Delete(RSVP item);
		bool Save();
        void Dispose();
	}
}