using System.Linq;
using MeetUp.Domain;

namespace MeetUp.Core
{
	public interface IRunnerRepository
	{
		IQueryable<Runner> List();
		Runner Find(int id);
		bool Add(Runner item);
		bool Delete(int id);
		bool Delete(Runner item);
		bool Save();
	}
}