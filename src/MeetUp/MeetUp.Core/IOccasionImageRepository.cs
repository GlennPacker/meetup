using System.Linq;
using MeetUp.Domain;

namespace MeetUp.Core
{
	public interface IOccasionImageRepository
	{
		IQueryable<OccasionImage> List(int? occasionId);
		OccasionImage Find(int id);
		bool Add(OccasionImage item);
		bool Update(OccasionImage item);
		bool Delete(int id);
		bool Delete(OccasionImage item);
		bool Save();
        void Dispose();
	}
}