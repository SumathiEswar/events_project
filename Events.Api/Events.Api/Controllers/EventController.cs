using Event.Application.Interface;
using Event.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Events.Api.Controllers.EventController
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        public IEvent _eventService;
        public EventController(IEvent eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("CreateNewEvent")]
        public BaseResponse CreateEvent(EventCreateRequest request)
        {
            return _eventService.CreateEvent(request);
        }

        [HttpPut("UpdateEvent")]
        public BaseResponse UpdateEvent(EventDetails request)
        {
            return _eventService.UpdateEvent(request);
        }

        [HttpGet("GetEventById")]
        public EventGetResponse GetEventById(int eventId)
        {
            return _eventService.GetEvent(eventId);
        }

        [HttpGet("GetAllEvents")]
        public EventGetAllResponse GetAllEvents()
        {
            return _eventService.GetAllEvents();
        }

        [HttpDelete("DeleteEvent")]
        public BaseResponse DeleteEvent(int eventId)
        {
            return _eventService.DeleteEvent(eventId);
        }
    }

}

