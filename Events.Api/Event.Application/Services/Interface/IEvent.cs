using System;
using Event.Application.Models;

namespace Event.Application.Interface
{
	public interface IEvent
	{
        BaseResponse CreateEvent(EventCreateRequest request);
        BaseResponse UpdateEvent(EventDetails request);
        EventGetResponse GetEvent(int eventId);
        EventGetAllResponse GetAllEvents();
        BaseResponse DeleteEvent(int eventId);
    }
}

