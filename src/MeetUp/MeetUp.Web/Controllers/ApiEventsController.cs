using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
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

        public HttpResponseMessage GetEvents()
        {
            return Request.CreateResponse(HttpStatusCode.OK, EventsFromDb());
        }

        
        public HttpResponseMessage GetEventsWithApiUpdate()
        {
            _meetUpEventsService.GetEventsFromMeetUp();
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
