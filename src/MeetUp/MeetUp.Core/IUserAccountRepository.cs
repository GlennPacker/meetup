using System.Collections.Generic;
using System.Linq;
using MeetUp.Domain;

namespace MeetUp.Core
{
	public interface IUserAccountRepository
	{
        IQueryable<UserAccount> List();
        IQueryable<UserAccount> List(List<long> meetupMemberIds);
        UserAccount Find(int id);
		UserAccount FindByMeetUpId(long meetUpId);
		bool Add(UserAccount item);
		bool Update(UserAccount item);
		bool Delete(int id);
		bool Delete(UserAccount item);
		bool Save();
        void Dispose();
        
	}
}