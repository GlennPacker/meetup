using System.Linq;
using MeetUp.Domain;

namespace MeetUp.Core
{
	public interface IEmailRepository
	{
		IQueryable<Email> List(bool isDeleted = false);
		Email Find(int id);
		bool Add(Email item);
		bool Update(Email item);
		bool Delete(int id);
		bool Delete(Email item);
		bool Save();
	}
}