using System;
using MeetUp.Domain;

namespace MeetUp.Core
{
	public interface IRunnerRepository
	{
		bool Update(ApiType apiType, int? id);
	    DateTime? GetLastRun(ApiType apiType, int? id);
        void StartUpdate(ApiType meetUpEvents, int? refId);

        // basic repo tasks
        void Dispose();

	    
	}
}