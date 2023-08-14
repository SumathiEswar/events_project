namespace Event.Application.Models
{
    public class EventDetails : CreateEventDetails
    {
        public int Id { get; set; }
        public EventDetails()
        {

        }
        public EventDetails(CreateEventDetails details)
        {
            User = details.User;
            Date = details.Date;
            StartTime = details.StartTime;
            EndTime = details.EndTime;
            Subject = details.Subject;
            Description = details.Description;
        }
    }

    public class CreateEventDetails
    {
        public string? User { get; set; }
        public DateOnly? Date { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
    }
}

