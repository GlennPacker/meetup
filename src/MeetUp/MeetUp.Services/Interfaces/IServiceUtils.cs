using System;
using MeetUp.Domain;

namespace MeetUp.Services.Interfaces
{
    public interface IServiceUtils
    {
        bool ShouldUpdate(bool force, DateTime updateIfOlderThan, ApiType apiType, long? refId);
        DateTime? GetLastRun(ApiType apiType, long? refId);
        void UpdateLastRun(ApiType meetUpEvents, long? refId);
        void Dispose();
        
    }
}