using System;
using MeetUp.Domain;

namespace MeetUp.Core
{
	public interface IRunnerRepository
	{
        bool Update(ApiType apiType, long? id);
	    DateTime? GetLastRun(ApiType apiType, long? id);
        void StartUpdate(ApiType meetUpEvents, long? refId);

        // basic repo tasks
        void Dispose();

	    
	}
}