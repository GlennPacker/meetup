using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MeetUp.Core;
using MeetUp.Domain;
using MeetUp.Services.Interfaces;

namespace MeetUp.Web.Controllers
{
    public class ApiEventsController : ApiController
    {
        private readonly IMeetUpEventService _meetUpEventsService;
        private readonly IMeetUpMemberService _memberService;
        
        public ApiEventsController(IMeetUpEventService meetUpEventsService, IMeetUpMemberService memberService)
        {
            _meetUpEventsService = meetUpEventsService;
            _memberService = memberService;
        }


        // initial call to events which will only get local data
        [Route("api/v1/ApiEvents")]
        public HttpResponseMessage GetEvents()
        {
            return Request.CreateResponse(HttpStatusCode.OK, EventsFromDb());
        }

        // second call which will update the data and then return the events. 
        // This process can make the data seem most up to date if we see changes happen on screen. 
        // this also means if meetup goes down user is still ok and we get a faster site.
        [Route("api/v1/Events/Update")]
        public HttpResponseMessage GetEventsWithApiUpdate(bool force = false)
        {
            _meetUpEventsService.GetEventsFromMeetUp(force);
            return Request.CreateResponse(HttpStatusCode.OK, EventsFromDb());
        }

        public List<ApiOccasionInfo> EventsFromDb()
        {
            var fromDate = DateTime.Now.AddDays(-2);
            var data = _meetUpEventsService.GetOccasions().Where(r => r.Date >= fromDate).ToList();  // its good to have a past event at the top for photos and to see event you just gone.
            return Mapper.Map<List<ApiOccasionInfo>>(data);
        }


        protected override void Dispose(bool disposing)
        {
            _memberService.Dispose();
            _meetUpEventsService.Dispose();
            base.Dispose(disposing);
        }
    }
}
