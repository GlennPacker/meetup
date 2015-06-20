using System;
using MeetUp.Domain;

namespace MeetUp.Services.Interfaces
{
    public interface IServiceUtils
    {
        bool ShouldUpdate(bool force, DateTime updateIfOlderThan, ApiType apiType, int? refId);
        void UpdateLastRun(ApiType meetUpEvents, int? refId);
        void Dispose();
        
    }
}