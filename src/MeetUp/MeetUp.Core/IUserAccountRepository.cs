using MeetUp.Domain;

namespace MeetUp.Core
{
	public interface IUserAccountRepository
	{
		UserAccount Find(int id);
		UserAccount FindByMeetUp(double meetUpEventId);
		bool Add(UserAccount item);
		bool Update(UserAccount item);
		bool Delete(int id);
		bool Delete(UserAccount item);
		bool Save();
	}
}