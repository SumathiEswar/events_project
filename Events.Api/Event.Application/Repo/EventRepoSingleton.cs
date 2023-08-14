using Event.Application.Interface;
using Event.Application.Models;

namespace Event.Application.Repo
{
    public class EventRepoSingleton : IEventRepo
    {
        private static EventRepoSingleton? instance = null;
        public static EventRepoSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventRepoSingleton();
                }
                return instance;
            }
        }
        public  List<EventDetails> _eventsList = new();

        public bool AddEvent(EventDetails eventDetails)
        {
            eventDetails.Id = _eventsList.Any() ? _eventsList.Max(a => a.Id) + 1 : 1;
            _eventsList.Add(eventDetails);
            return true;
        }

        public bool UpdateEvent(EventDetails request)
        {
            var eventDetails = GetEvent(request.Id);
            if (eventDetails != null)
            {
                _eventsList.Remove(eventDetails);
                _eventsList.Add(request);
            }
            return true;
        }
        public List<EventDetails> GetAllEvents()
        {
            return _eventsList.OrderBy(a=> a.Date).ToList();
        }
        public EventDetails? GetEvent(int id)
        {
            return _eventsList.Find(s => s.Id == id);
        }

        public bool DeleteEvent(int eventId)
        {
            var eventDetails = GetEvent(eventId);
            if(eventDetails != null)
            {
                _eventsList.Remove(eventDetails);
                return true;
            }
            return false;
        }

    }
}

