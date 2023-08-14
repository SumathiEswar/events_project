namespace Event.Application.Models
{
    public class EventGetResponse : BaseResponse 
    {
        public EventDetails EventDetails { get; set; } = new();
    }
}

