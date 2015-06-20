using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using MeetUp.Domain;
using MeetUp.MeetUpApi.Interfaces;
using MeetUp.MeetUpApi.Models;
using MeetUp.Services.Interfaces;

namespace MeetUp.Services
{
    public class MeetUpMemberService : IMeetUpMemberService
    {
        private readonly IMeetUpMemberProxy _memberProxy;
        private readonly IUserAccountFactory _userFactory;
        private readonly IServiceUtils _serviceUtils;
        
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MeetUpMemberService(IMeetUpMemberProxy memberProxy, IUserAccountFactory userFactory, IServiceUtils serviceUtils)
        {
            _memberProxy = memberProxy;
            _userFactory = userFactory;
            _serviceUtils = serviceUtils;
        }

        public void GetMembersFromMeetUp(bool force = false)
        {
            GetMembersFromMeetUp(force, DateTime.Now);
        }

        public void GetMembersFromMeetUp(bool force, DateTime dt)
        {
            if (! _serviceUtils.ShouldUpdate(force, Convert.ToDateTime(dt).AddHours(-4), ApiType.MeetupMembers, null)) return;
            
            // can get a max of 200 members at a time so run in a loop and to reduce memory handle as a set of 200 instead of getting all.
            var i = 0;
            List<MeetupMember> members;
            do
            {
                members = new List<MeetupMember>(); // reset from last loop
                
                try
                {
                    var wrapper = _memberProxy.GetMeetupMembers(i);
                    i++;
                    if (wrapper.IsGood)
                    {
                        members = wrapper.Data.results.ToList();
                        _userFactory.MapUsers(members);
                    }
                    else
                    {
                        Log.Error(wrapper.Error, wrapper.ErrorException);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message, ex);
                }
            } while (members.Count == 200);

            _serviceUtils.UpdateLastRun(ApiType.MeetupMembers, null);
        }

        public void Dispose()
        {
            _userFactory.Dispose();
        }
    }
}
