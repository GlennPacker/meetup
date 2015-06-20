using MeetUp.Domain;

namespace MeetUp.Core
{
	public interface IUserPicRepository
	{
		UserPic Find(int id);
		bool Add(UserPic item);
		bool Update(UserPic item);
		bool Delete(int id);
		bool Delete(UserPic item);
		bool Save();
        void Dispose();
	}
}