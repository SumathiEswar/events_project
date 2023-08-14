namespace Event.Application.Models
{
    public class EventGetAllResponse : BaseResponse
    {
        public List<EventDetails> TodayEvents { get; set; } = new();
        public List<EventDetails> UpcomingEvents { get; set; } = new();
    }
}

