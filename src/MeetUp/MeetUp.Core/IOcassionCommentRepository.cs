using System.Linq;
using MeetUp.Domain;

namespace MeetUp.Core
{
	public interface IOcassionCommentRepository
	{
		IQueryable<OcassionComment> List(int? occasionId);
		OcassionComment Find(int id);
		bool Add(OcassionComment item);
		bool Update(OcassionComment item);
		bool Delete(int id);
		bool Delete(OcassionComment item);
		bool Save();
	}
}