using System.Linq;
using MeetUp.Domain;

namespace MeetUp.Core
{
	public interface IOcassionCommentRepository
	{
		IQueryable<OccasionComment> List(int? occasionId);
		OccasionComment Find(int id);
		bool Add(OccasionComment item);
		bool Update(OccasionComment item);
		bool Delete(int id);
		bool Delete(OccasionComment item);
		bool Save();
        void Dispose();
	}
}