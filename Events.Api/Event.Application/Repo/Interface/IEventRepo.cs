using Event.Application.Models;

namespace Event.Application.Interface
{
    public interface IEventRepo
	{
        bool AddEvent(EventDetails request);
        bool UpdateEvent(EventDetails request);
        EventDetails? GetEvent(int id);
        List<EventDetails> GetAllEvents();
        bool DeleteEvent(int eventId);
    }
}

